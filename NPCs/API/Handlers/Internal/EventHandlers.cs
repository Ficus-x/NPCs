using System.Collections.Generic;
using Exiled.API.Features;
using NPCs.API.Features;
using Map = NPCs.API.Features.Objects.Map;

namespace NPCs.API.Handlers.Internal
{
    public class EventHandlers
    {
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