using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FWNetworkManager : NetworkManager  {

    [SerializeField]
    private Startup startup;

    private PlayerInfo[] players = new PlayerInfo[2];

    private int playerCount = 0; //Bug in Unity causes first client to double connect

    struct PlayerInfo {
        public int playerNumber;
        public int connectionId;
        public PlayerControl playerControl;

        public PlayerInfo(int pNum, int connId, PlayerControl control) {
            playerNumber = pNum;
            connectionId = connId;
            playerControl = control;
        }
    }

    public PlayerControl PlayerForConnectionId(int connId) {
        if (players[0].connectionId == connId) {
            return players[0].playerControl;
        }
        else if (players[1].connectionId == connId) {
            return players[1].playerControl;
        }

        return null;
    }

    public PlayerControl PlayerForIndex(int index) {
        return players[index].playerControl;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (playerCount < 2) {
            var player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            players[playerCount] = new PlayerInfo(playerCount, conn.connectionId, player.GetComponent<PlayerControl>());

            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

            if (playerCount == 1) {
                startup.StartGame();
            }

            playerCount++;
        }
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController controller) {

    }
}
