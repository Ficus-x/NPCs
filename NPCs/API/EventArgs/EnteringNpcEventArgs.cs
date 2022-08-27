using Exiled.API.Features;

namespace NPCs.API.EventArgs
{
    public class EnteringNpcEventArgs : System.EventArgs
    {
        public EnteringNpcEventArgs(Player player, Npc npc)
        {
            Player = player;
            Npc = npc;
        }

        public Player Player { get; }

        public Npc Npc { get; }
    }
}