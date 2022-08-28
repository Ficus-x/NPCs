using System.Linq;
using Exiled.API.Features;
using NPCs.API.Features;
using UnityEngine;

namespace NPCs.API
{
    public static class Extensions
    {
        public static bool TryGetNpcOnSight(this Player player, float maxDistance, out Npc npc)
        {
            npc = null;

            if (!Physics.Raycast(new Ray(player.ReferenceHub.PlayerCameraReference.position + player.GameObject.transform.forward * 0.3f, player.ReferenceHub.PlayerCameraReference.forward), out RaycastHit hit, maxDistance))
                return false;

            npc = Npc.SpawnedNpc.FirstOrDefault((n => n.GameObject == hit.transform.gameObject));

            return npc != null;
        }

        public static bool IsNpc(this Player player) => Npc.SpawnedNpc.Contains(player);
    }
}