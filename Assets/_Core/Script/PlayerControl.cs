using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerControl : NetworkBehaviour {

    /*server only*/ private Head head;
    /*server only*/ private Body body;

    // Update is called once per frame
    void Update () {
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.W)) {
                CmdMoveBody(new Vector2(0, 1));
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                CmdMoveBody(new Vector2(0, -1));
            }
            else if (Input.GetKeyDown(KeyCode.A)) {
                CmdMoveBody(new Vector2(-1, 0));
            }
            else if (Input.GetKeyDown(KeyCode.D)) {
                CmdMoveBody(new Vector2(1, 0));
            }
        }
    }

    [Command] void CmdMoveBody(Vector2 amount) {
        ServerGridBuilder grid = GameObject.Find("[S]Main").GetComponent<ServerGridBuilder>();

        Cell cell = grid.GetCell(body.transform.position, amount);
        if (cell != null) {
            body.SetPosition(cell.Center);
        }
        else {
            Debug.LogError("CELL IS NULL");
        }
    }

    public void BindToObjects(Head h, Body b) {
        head = h;
        body = b;
    }
}
