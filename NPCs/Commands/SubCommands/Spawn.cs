using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using NPCs.API.Features;
using UnityEngine;

namespace NPCs.Commands.SubCommands
{
    public class Spawn : ICommand
    {
        public string Command => "spawn";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description => "Spawns a NPC";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            
            if (!player.CheckPermission($"npc.{Command}"))
            {
                response = $"You do not have permission to use this command! Required permission: npc.{Command}";
                return false;
            }

            if (arguments.Count != 2 || !Enum.TryParse(arguments.ElementAt(1), true, out RoleType roleType))
            {
                response = "Usage: npc spawn [nickname] [RoleType]";
                return false;
            }

            _ = Npc.Spawn(arguments.ElementAt(0), roleType, player.CurrentRoom.Type, player.Position, Vector3.one, Vector2.up);

            response = "NPC has been successfully spawned!";
            return true;
        }
    }
}