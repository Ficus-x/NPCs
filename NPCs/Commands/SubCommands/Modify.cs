using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using NPCs.API;

namespace NPCs.Commands.SubCommands
{
    public class Modify : ICommand
    {
        public string Command => "modify";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description => "Modifies a NPC which you are looking at";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (!player.CheckPermission("npc." + Command))
            {
                response = "You do not have permission to use this command! Required permission: npc." + Command;
                return false;
            }

            if (!player.TryGetNpcOnSight(15f, out Npc npc))
            {
                response = "You need to look at NPC!";
                return false;
            }

            object instance = npc;
            List<PropertyInfo> list = instance.GetType().GetProperties().Where(x => Type.GetTypeCode(x.PropertyType) != TypeCode.Object).ToList();

            if (arguments.Count == 0)
            {
                response = "\nObject properties:\n\n";
                foreach (PropertyInfo propertyInfo in list)
                {
                    if (propertyInfo.PropertyType == typeof(bool))
                        response = response + propertyInfo.Name + ": " + ((bool) propertyInfo.GetValue(instance) ? "<color=green><b>TRUE</b></color>" : "<color=red><b>FALSE</b></color>") + "\n";
                    else
                        response += string.Format("{0}: <color=yellow><b>{1}</b></color>\n", propertyInfo.Name, propertyInfo.GetValue(instance) ?? "NULL");
                }

                return true;
            }

            PropertyInfo propertyInfo1 = list.FirstOrDefault(x => x.Name.ToLower().Contains(arguments.At(0).ToLower()));

            if (propertyInfo1 == null)
            {
                response = "There isn't any object property that contains \"" + arguments.At(0) + "\" in it's name!";
                return false;
            }

            try
            {
                if (propertyInfo1.PropertyType != typeof(string))
                {
                    object value;

                    try
                    {
                        value = TypeDescriptor.GetConverter(propertyInfo1.PropertyType).ConvertFromInvariantString(arguments.At(1));
                    }
                    catch (Exception)
                    {
                        if (!arguments.At(1).ToLower().Contains("null") || !(propertyInfo1.PropertyType == typeof(float?)))
                            throw new Exception();
                        
                        value = null;
                    }

                    propertyInfo1.SetValue(instance, value);
                }
                else
                {
                    string text = arguments.At(1);
                    for (int index = 1; index < arguments.Count - 1; ++index)
                        text = text + " " + arguments.At(1 + index);

                    propertyInfo1.SetValue(instance, TypeDescriptor.GetConverter(propertyInfo1.PropertyType).ConvertFromInvariantString(text));
                }
            }
            catch (Exception)
            {
                response = $"\"{arguments.At(1)}\" is not a valid argument! The value should be a {propertyInfo1.PropertyType} type.";
                return false;
            }

            response = "You have successfully modified the object!";
            return false;
        }
    }
}