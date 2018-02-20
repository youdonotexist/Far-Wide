using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerVotePhase : NetworkBehaviour, IPhase {

    // Grab the canvas, show the voting UI
    // Wait for both player to vote

    //public void Vote(

    public bool Player1CanMove() {
        return false;
    }

    public bool Player2CanMove() {
        return false;
    }

        public enum Vote {
            MOVE,
            BLOCK
        };
	
}
