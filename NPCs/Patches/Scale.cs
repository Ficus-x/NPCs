using Exiled.API.Features;
using HarmonyLib;

namespace NPCs.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.Scale), MethodType.Setter)]
    internal static class Scale
    {
        private static bool Prefix() => false;
    }
}