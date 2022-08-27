using Exiled.Events;
using Exiled.Events.Extensions;
using NPCs.API.EventArgs;

namespace NPCs.API.Handlers
{
    public static class Npc
    {
        public static event Events.CustomEventHandler<EnteringNpcEventArgs> EnteringNpc;

        public static event Events.CustomEventHandler<SpawningNpcEventArgs> SpawningNpc; 

        public static void OnEnteringNpc(EnteringNpcEventArgs ev) => EnteringNpc?.InvokeSafely(ev);

        public static void OnSpawningNpc(SpawningNpcEventArgs ev) => SpawningNpc?.InvokeSafely(ev);
    }
}