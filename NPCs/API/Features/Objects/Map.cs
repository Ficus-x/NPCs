using System.Collections.Generic;

namespace NPCs.API.Features.Objects
{
    public class Map
    {
        public Map(string name)
        {
            Name = name;
        }
        
        public Map() {}
        
        public string Name { get; set; }
        
        public HashSet<SerializableNpc> Npcs { get; set; }
    }
}