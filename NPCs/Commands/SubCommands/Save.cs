using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Permissions.Extensions;
using NPCs.API;
using NPCs.API.Features;

namespace NPCs.Commands.SubCommands
{
    public class Save : ICommand
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
                response = "Usage: npc save [Map name]";
                return false;
            }
            
            MapUtils.SaveMap(arguments.ElementAt(0));
            
            response = "Map has been successfully saved!";
            return true;
        }
    }
}