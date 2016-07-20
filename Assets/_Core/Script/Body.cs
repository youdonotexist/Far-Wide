using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Body : NetworkBehaviour {
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
