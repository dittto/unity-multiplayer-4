// Player/SyncedData/PlayerDataForClients.cs

using UnityEngine;
using UnityEngine.Networking;

namespace Player.SyncedData {
    public class PlayerDataForClients : NetworkBehaviour {

        public delegate void ColourUpdated (Color newColour);
        public event ColourUpdated OnColourUpdated;
        public delegate void IsServerFlagUpdated (bool isServer);
        public event IsServerFlagUpdated OnIsServerFlagUpdated;

        [SyncVar(hook = "UpdateColour")]
        private Color colour;
        [SyncVar(hook = "UpdateIsServerFlag")]
        private bool isServerFlag;

        // use this for re-triggering the hooks on scene load
        public override void OnStartClient()
        {
            // don't update for local player as handled by LocalPlayerOptionsManager
            // don't update for server as only the clients need this
            if (!isLocalPlayer && !isServer) {
                UpdateColour(colour);
                UpdateIsServerFlag(isServerFlag);
            }
        }
        
        [Client]
        public void SetColour (Color newColour)
        {
            CmdSetColour(newColour);
        }

        [Command]
        public void CmdSetColour (Color newColour)
        {
            colour = newColour;
        }

        [Client]
        public void UpdateColour (Color newColour)
        {
            colour = newColour;
            GetComponentInChildren<MeshRenderer>().material.color = newColour;

            if (this.OnColourUpdated != null) {
                this.OnColourUpdated(newColour);
            }
        }

        [Client]
        public void SetIsServer (bool newIsServer)
        {
            CmdSetIsServer(newIsServer);
        }

        [Command]
        public void CmdSetIsServer (bool newIsServer)
        {
            isServerFlag = newIsServer;
        }

        [Client]
        public void UpdateIsServerFlag (bool newIsServer)
        {
            isServerFlag = newIsServer;

            if (this.OnIsServerFlagUpdated != null) {
                this.OnIsServerFlagUpdated(newIsServer);
            }
        }
    }
}