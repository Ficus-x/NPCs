using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using NPCs.API;

namespace NPCs.Commands.SubCommands
{
    public class Destroy : ICommand
    {
        public string Command => "destroy";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description => "Destroys a NPC which you look at";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.CheckPermission($"npc.{Command}"))
            {
                response = $"You do not have permission to use this command! Required permission: npc.{Command}";
                return false;
            }

            if (!player.TryGetNpcOnSight(12f, out Npc npc))
            {
                response = "You need to look at NPC!";
                return false;
            }
            
            npc.Destroy();

            response = "NPC has been successfully destroyed.";
            return true;
        }
    }
}