using System;
using System.IO;
using Exiled.API.Features;
using HarmonyLib;
using NPCs.API.Handlers.Internal;
using NPCs.Resources;
using Map = Exiled.Events.Handlers.Map;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;
using Warhead = Exiled.Events.Handlers.Warhead;

namespace NPCs
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Author => "Ficus-x";

        public override string Name => "NPCs";

        public override string Prefix => Name;

        public override Version Version { get; } = new(2, 0, 0);

        public static Plugin Instance { get; private set; }

        private EventHandlers _handlers;

        private Harmony _harmony;
        
        public override void OnEnabled()
        {
            Instance = this;
            
            RegisterEvents();
            
            PatchAll();
            
            CheckFolders();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            
            UnregisterEvents();
            
            UnpatchAll();
            
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _handlers = new EventHandlers();

            Map.Generated += _handlers.OnGenerated;
            Server.RoundStarted += _handlers.OnRoundStarted;
            Warhead.Detonated += _handlers.OnDetonated;
            Player.ItemAdded += _handlers.OnItemAdded;
        }

        private void UnregisterEvents()
        {
            Map.Generated -= _handlers.OnGenerated;
            Server.RoundStarted -= _handlers.OnRoundStarted;
            Warhead.Detonated -= _handlers.OnDetonated;
            Player.ItemAdded -= _handlers.OnItemAdded;
            
            _handlers = null;
        }

        private void PatchAll()
        {
            _harmony = new Harmony($"NPCs.{DateTime.UtcNow.Ticks}");
            _harmony.PatchAll();
        }

        private void UnpatchAll()
        {
            _harmony.UnpatchAll();
            _harmony = null;
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