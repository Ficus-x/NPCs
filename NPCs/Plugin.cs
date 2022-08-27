using System;
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
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
        }
    }
}