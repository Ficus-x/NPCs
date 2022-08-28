using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Permissions.Extensions;
using NPCs.API;
using NPCs.API.Features;
using Map = NPCs.API.Features.Objects.Map;

namespace NPCs.Commands.SubCommands
{
    public class Load : ICommand
    {
        public string Command => "save";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description => "Save the map with NPCs";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.CheckPermission($"npc.{Command}"))
            {
                response = $"You do not have permission to use this command! Required permission: npc.{Command}";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Usage: npc load [Map name]";
                return false;
            }

            Map map = MapUtils.GetMapByName(arguments.ElementAt(0));

            if (map is null)
            {
                response = "The map with this name does not exist";
                return false;
            }

            MapUtils.LoadMap(map);

            response = "Map has been successfully loaded!";
            return true;
        }
    }
}