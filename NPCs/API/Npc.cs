using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using InventorySystem;
using InventorySystem.Items;
using MEC;
using Mirror;
using NPCs.API.Components;
using RemoteAdmin;
using UnityEngine;

namespace NPCs.API
{
    public class Npc
    {
        public static readonly HashSet<Npc> SpawnedNpc = new();

        public string Nickname
        {
            get => ReferenceHub.nicknameSync.MyNick;
            set => ReferenceHub.nicknameSync.MyNick = value;
        }

        public ReferenceHub ReferenceHub { get; private set; }

        public GameObject GameObject { get; private set; }

        public RoleType Role
        {
            get => ReferenceHub.characterClassManager.CurClass;
            set => ReferenceHub.characterClassManager.CurClass = value;
        }

        public Room CurrentRoom => Map.FindParentRoom(GameObject);

        public Item CurrentItem
        {
            get => Item.Get(ReferenceHub.inventory._curInstance);
            set
            {
                if (value == null || value.Type == ItemType.None)
                {
                    ReferenceHub.inventory.ServerSelectItem(0);
                }
                else
                {
                    if (!ReferenceHub.inventory.UserInventory.Items.TryGetValue(value.Serial, out ItemBase _))
                        ReferenceHub.inventory.ServerAddItem(value.Type);

                    Timing.CallDelayed(0.5f, () => ReferenceHub.inventory.ServerSelectItem(value.Serial));
                }
            }
        }

        public Vector3 Scale
        {
            get => ReferenceHub.transform.localScale;
            set => ReferenceHub.transform.localScale = value;
        }

        public Vector2 Rotation
        {
            get => ReferenceHub.playerMovementSync.RotationSync;
            set
            {
                ReferenceHub.playerMovementSync.RotationSync = value;
                ReferenceHub.playerMovementSync.ForceRotation(new PlayerMovementSync.PlayerRotation(value.x, value.y));
            }
        }

        public Vector3 Position
        {
            get => ReferenceHub.playerMovementSync.GetRealPosition();
            set => ReferenceHub.playerMovementSync.ForcePosition(value);
        }

        private Npc(string nickname, RoleType role, Vector3 position, Vector3 scale, Vector2 rotation)
        {
            GameObject = Object.Instantiate(NetworkManager.singleton.playerPrefab);
            ReferenceHub = ReferenceHub.GetHub(GameObject);

            ReferenceHub.queryProcessor._ipAddress = Server.IpAddress;
            ReferenceHub.queryProcessor.NetworkPlayerId = QueryProcessor._idIterator++;

            ReferenceHub.nicknameSync.MyNick = nickname;
            ReferenceHub.transform.localScale = scale;
            ReferenceHub.transform.position = position;
            ReferenceHub.playerMovementSync.RotationSync = rotation;
            ReferenceHub.characterClassManager.CurClass = role;

            ReferenceHub.playerMovementSync.NetworkGrounded = true;
            ReferenceHub.characterClassManager.GodMode = true;

            NetworkServer.Spawn(GameObject);
            GameObject.AddComponent<Touching>();

            SpawnedNpc.Add(this);
        }

        public static Npc Spawn(string nickname, RoleType role, Vector3 position, Vector3 scale, Vector2 rotation) => new Npc(nickname, role, position, scale, rotation);

        public void LookAtPlayer(Player player)
        {
            Quaternion quaternion = Quaternion.LookRotation((player.Position - Position).normalized);
            Rotation = new Vector2(quaternion.eulerAngles.x, quaternion.eulerAngles.y);
        }
    }
}