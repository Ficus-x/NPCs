using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Mirror;
using NPCs.API.Components;
using RemoteAdmin;
using UnityEngine;

namespace NPCs.API
{
    public class Npc : Player
    {
        public static readonly HashSet<Npc> SpawnedNpc = new();

        private Npc(ReferenceHub referenceHub, string nickname, RoleType role, Vector3 position, Vector3 scale, Vector2 rotation) : base(referenceHub)
        {
            ReferenceHub.queryProcessor._ipAddress = Server.IpAddress;
            ReferenceHub.queryProcessor.NetworkPlayerId = QueryProcessor._idIterator++;

            ReferenceHub.characterClassManager.Start();
            ReferenceHub.playerMovementSync.Start();

            ReferenceHub.nicknameSync.MyNick = nickname;
            Scale = scale;
            Position = position;
            Rotation = rotation;
            ReferenceHub.characterClassManager.CurClass = role;

            ReferenceHub.playerMovementSync.NetworkGrounded = true;
            ReferenceHub.characterClassManager.GodMode = true;

            NetworkServer.Spawn(GameObject);
            GameObject.AddComponent<Touching>();

            SpawnedNpc.Add(this);
        }
        
        public static Npc Spawn(string nickname, RoleType role, Vector3 position, Vector3 scale, Vector2 rotation)
            => new(ReferenceHub.GetHub(Object.Instantiate(NetworkManager.singleton.playerPrefab)), nickname, role, position, scale, rotation);

        public void LookAtPosition(Vector3 position)
        {
            Quaternion quaternion = Quaternion.LookRotation((position - Position).normalized);
            Rotation = new Vector2(quaternion.eulerAngles.x, quaternion.eulerAngles.y);
        }

        public void Update()
        {
            Destroy();
            Spawn(Nickname, ReferenceHub.characterClassManager.CurClass, Position, Scale, Rotation);
        }
        
        public void Destroy()
        {
            SpawnedNpc.Remove(this);
            Object.Destroy(GameObject);
        }
    }
}