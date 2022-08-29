using Exiled.API.Features;
using HarmonyLib;
using NPCs.API;
using UnityEngine;

namespace NPCs.Patches
{
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.AddPlayer))]
    internal static class Online
    {
        private static void Postfix(GameObject player, int maxPlayers)
        {
            Player loadedPlayer = Player.Get(player);
            
            if (loadedPlayer is null || !loadedPlayer.IsNpc())
                return;

            PlayerManager.players.Remove(player);
            ServerConsole.PlayersAmount = PlayerManager.players.Count;
            ServerConsole.PlayersListChanged = false;
        }
    }
}