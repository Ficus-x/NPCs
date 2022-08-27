using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using NPCs.API;

namespace NPCs.Commands.SubCommands
{
    public class List : ICommand
    {
        public string Command => "list";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description => "Shows the list of spawned NPC";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.CheckPermission($"npc.{Command}"))
            {
                response = $"You do not have permission to use this command! Required permission: npc.{Command}";
                return false;
            }

            response = Npc.SpawnedNpc.Aggregate("NPC list:", (current, npc) => current + $"{npc.Nickname} - Role: {npc.RoleType}, Position: {npc.Position}");
            return true;
        }
    }
}