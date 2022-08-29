using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using NPCs.API.Features;
using UnityEngine;
using Map = NPCs.API.Features.Objects.Map;

namespace NPCs.API.Handlers.Internal
{
    public class EventHandlers
    {
        public void OnItemAdded(ItemAddedEventArgs ev)
        {
            if (ev.Player is not Features.Npc npc || ev.Item.Type is not ItemType.Ammo762x39 and ItemType.Ammo9x19)
                return;

            Vector3 sizeToEdit = new Vector3(0.1f, 0.1f, 0.1f);

            if (ev.Item.Type is ItemType.Ammo762x39)
                npc.Scale += sizeToEdit;
            else
                npc.Scale -= sizeToEdit;

            npc.Update();
        }
        
        public void OnGenerated() => LoadMap(Plugin.Instance.Config.MapsToLoad.OnGenerated);

        public void OnDetonated() => LoadMap(Plugin.Instance.Config.MapsToLoad.OnWarheadDetonated);

        public void OnRoundStarted() => LoadMap(Plugin.Instance.Config.MapsToLoad.OnRoundStarted);

        private void LoadMap(List<string> names)
        {
            foreach (string name in names)
            {
                Map map = MapUtils.GetMapByName(name);

                if (map is null)
                {
                    Log.Warn($"There are no maps with this name! Map name: {name}");
                    continue;
                }

                MapUtils.LoadMap(map);
            }
        }
    }
}