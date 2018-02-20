using UnityEngine;
using System.Collections;

public class GameMessages : MonoBehaviour {
    public static short MSG_LOGIN_RESPONSE = 1000;
    public static short MSG_SCORE = 1005;

    struct Turn {
        int player;
        int numMoves;
    }
}


