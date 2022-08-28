using Exiled.API.Enums;
using Exiled.API.Features.Items;
using UnityEngine;

namespace NPCs.API.Features.Objects
{
    public class SerializableNpc
    {
        public string Nickname { get; set; }
        
        public RoomType Room { get; set; }
        
        public Vector3 Position { get; set; }
        
        public Vector3 Scale { get; set; }
        
        public RoleType Role { get; set; }
        
        public Vector2 Rotation { get; set; }
        
        public Item CurrentItem { get; set; }
    }
}