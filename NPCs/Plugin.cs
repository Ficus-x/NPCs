using System;
using System.IO;
using Exiled.API.Features;
using NPCs.Resources;

namespace NPCs
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Author => "Ficus-x (original idea by gamehunt)";

        public override string Name => "NPCs";

        public override string Prefix => Name;

        public override Version Version { get; } = new(1, 0, 0);

        public override void OnEnabled()
        {
            CheckFolders();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
        }

        private void CheckFolders()
        {
            string npcPath = Path.Combine(Paths.Configs, "NPCs");
            
            if (!Directory.Exists(npcPath))
                Directory.CreateDirectory(npcPath);
            
            if (!Directory.Exists(Path.Combine(npcPath, "Maps")))
                Directory.CreateDirectory(Path.Combine(npcPath, "Maps"));
        }
    }
}