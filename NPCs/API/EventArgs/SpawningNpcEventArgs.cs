using NPCs.API.Features;

namespace NPCs.API.EventArgs
{
    public class SpawningNpcEventArgs : System.EventArgs
    {
        public SpawningNpcEventArgs(Npc npc, bool isAllowed = true)
        {
            Npc = npc;
            IsAllowed = isAllowed;
        }
        
        public Npc Npc { get; }
        
        public bool IsAllowed { get; set; }
    }
}