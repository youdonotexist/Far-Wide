using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Gameplay : NetworkBehaviour {

    [SerializeField]
    private ServerGridBuilder gridBuilder;

    [SerializeField] 
    private FWNetworkManager networkManager;

    private GameObject player1body;
    private GameObject player1head;

    private GameObject player2body;
    private GameObject player2head;

    public void setPlayerObjects (GameObject p1Body, GameObject p1Head, GameObject p2Body, GameObject p2Head) {
        player1body = p1Body;
        player1head = p1Head;

        player2body = p2Body;
        player2head = p2Head;

        networkManager.PlayerForIndex(0).BindToObjects(player1head.GetComponent<Head>(), player1body.GetComponent<Body>());
        networkManager.PlayerForIndex(1).BindToObjects(player2head.GetComponent<Head>(), player2body.GetComponent<Body>());

        NetworkServer.Spawn(player1body);
        NetworkServer.Spawn(player1head);

        NetworkServer.Spawn(player2body);
        NetworkServer.Spawn(player2head);
    }

    public void StartPhase() {
        NetworkServer.SendToAll
    }
}
