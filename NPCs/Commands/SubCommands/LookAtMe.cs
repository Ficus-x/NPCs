using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using NPCs.API;
using Npc = NPCs.API.Features.Npc;

namespace NPCs.Commands.SubCommands
{
    public class LookAtMe : ICommand
    {
        public string Command => "lookAtMe";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description => "Forces a NPC to look at you";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!player.CheckPermission("npc.modify"))
            {
                response = "You do not have permission to use this command! Required permission: npc.modify";
                return false;
            }

            if (!player.TryGetNpcOnSight(12f, out Npc npc))
            {
                response = "You need to look at NPC!";
                return false;
            }

            npc.LookAtPosition(player.Position);
            npc.Update();

            response = "NPC has successfully looked at you!";
            return true;
        }
    }
}