using System.IO;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Loader;
using NPCs.API.Features.Objects;
using UnityEngine;
using Map = NPCs.API.Features.Objects.Map;

namespace NPCs.API.Features
{
    public static class MapUtils
    {
        public static void SaveMap(string name)
        {
            Map map = new Map(name)
            {
                Npcs = Npc.SpawnedNpc.Select(n => new SerializableNpc() {Nickname = n.Nickname, Scale = n.ReferenceHub.transform.localScale, Role = n.Role, Position = GetRelativePosition(n.Position, n.Room),
                    Room = n.Room, Rotation = n.Rotation, CurrentItem = n.CurrentItem}).Where(n => Npc.LoadedMaps.All(m => !m.Npcs.Contains(n))).ToHashSet()
            };

            string path = Path.Combine(Path.Combine(Paths.Configs, "NPCs", "Maps"), $"{map.Name}.yml");
            File.WriteAllText(path, Loader.Serializer.Serialize(map));
        }

        public static void LoadMap(string mapName) => LoadMap(GetMapByName(mapName));

        public static void LoadMap(Map map)
        {
            foreach (SerializableNpc npc in map.Npcs)
            {
                Npc.Spawn(npc);
            }

            Npc.LoadedMaps.Add(map);
        }
        
        public static Map GetMapByName(string name)
        {
            string path = Path.Combine(Path.Combine(Paths.Configs, "NPCs", "Maps"), $"{name}.yml");

            return !File.Exists(path) ? null : Loader.Deserializer.Deserialize<Map>(File.ReadAllText(path));
        }
        
        public static Vector3 GetRelativePosition(Vector3 position, RoomType room) => room == RoomType.Surface ? position : Room.Get(room).Transform.TransformPoint(position);
    }
}