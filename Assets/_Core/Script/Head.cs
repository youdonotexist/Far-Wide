using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Head : NetworkBehaviour {

    [SyncVar] private Vector3 position;

    public void SetPosition(Vector3 pos) {
        position = pos;
    }

    void Start() {
        position = transform.position;
    }

    void Update() {
        transform.position = position;
    }
}
