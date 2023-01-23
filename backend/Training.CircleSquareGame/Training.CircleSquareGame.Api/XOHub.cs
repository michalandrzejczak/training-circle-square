using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR;

namespace Training.CircleSquareGame.Api;
public class XOHub : Hub
{
    private static string currentFieldValue = FieldValue.X;
    public static Dictionary<string, string> xoBoard = new Dictionary<string, string>();

    public async Task GetField(string fieldId)
    {
        await Clients.All.SendAsync("CurrentFieldValue", fieldId, "", xoBoard);
    }
    
    public async Task SetField(string fieldId)
    {
        if (xoBoard.ContainsKey(fieldId))
        {
            return;
        }

        xoBoard.Add(fieldId, currentFieldValue);
        currentFieldValue = currentFieldValue == FieldValue.X ? FieldValue.O : FieldValue.X;
        await Clients.All.SendAsync("CurrentFieldValue", fieldId, currentFieldValue, xoBoard);
    }

    public async Task StartNewGame(string fieldValue)
    {
        currentFieldValue = fieldValue;
        xoBoard.Clear();
        await Clients.All.SendAsync("StartNewGame", currentFieldValue);
    }
}