using System.Collections.Generic;
using Exiled.API.Interfaces;

namespace NPCs.Resources
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public MapToLoad MapsToLoad { get; set; } = new();
    }

    public class MapToLoad
    {
        public List<string> OnGenerated { get; private set; } = new ();

        public List<string> OnRoundStarted { get; private set; } = new ();

        public List<string> OnWarheadDetonated { get; private set; } = new ();
    }
}