using Exiled.API.Features;
using HarmonyLib;
using NPCs.API;

namespace NPCs.Patches
{
    [HarmonyPatch(typeof(PlayerMovementSync), nameof(PlayerMovementSync.OverridePosition))]
    internal static class OverridePosition
    {
        private static void Postfix(PlayerMovementSync __instance)
        {
            Player player = Player.Get(__instance.gameObject);

            if (player is null || !player.IsNpc())
                return;
            
            __instance.ReceivePosition(__instance.RealModelPosition, false);
        }
    }
}