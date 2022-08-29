using Exiled.API.Features;
using HarmonyLib;
using NPCs.API;

namespace NPCs.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.Scale), MethodType.Setter)]
    internal static class Scale
    {
        private static bool Prefix(Player __instance) => !__instance.IsNpc();
    }
}