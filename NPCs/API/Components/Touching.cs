using System.Linq;
using Exiled.API.Features;
using NPCs.API.EventArgs;
using NPCs.API.Features;
using UnityEngine;

namespace NPCs.API.Components
{
    internal sealed class Touching : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            Handlers.Npc.OnEnteringNpc(new EnteringNpcEventArgs(Player.Get(collider.transform.root.gameObject), Npc.SpawnedNpc.FirstOrDefault(n => n.GameObject == gameObject)));
        }
    }
}