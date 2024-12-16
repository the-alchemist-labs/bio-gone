using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Serializable]
public static class MatchmakingApi
{
    public static async Task RemovePlayerFromLobby()
    {
        await Http.Delete<Task>($"{Consts.ServerURI}/lobby/remove/{PlayerProfile.Instance.Id}");
    }
}