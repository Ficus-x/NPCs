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
    public class AddItem : ICommand
    {
        public string Command => "addItem";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description => "Adds an item to a NPC";

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

            if (arguments.Count != 1 || !Enum.TryParse(arguments.ElementAt(0), true, out ItemType itemType))
            {
                response = "Usage: npc additem [ItemType]";
                return false;
            }
            
            npc.CurrentItem = Item.Create(itemType);
            npc.Update();
            
            response = $"Item {itemType} has been successfully added to the NPC!";
            return true;
        }
    }
}