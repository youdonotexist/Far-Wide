using UnityEngine;
using System.Collections;
using Gamelogic.Grids;
using UnityEngine.Networking;

public class Startup : NetworkBehaviour {

    [SerializeField]
    private ServerGridBuilder gridBuilder;

    [SerializeField]
    private Gameplay gameplay;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject headPrefab;

    public void StartGame() {
        gridBuilder.BuildGrid();

        Cell[] corners = gridBuilder.getCorners();

        Cell[] magicTiles = gridBuilder.GetMagicTiles();

        gameplay.setPlayerObjects(
            (GameObject) GameObject.Instantiate(playerPrefab, corners[0].Center, Quaternion.identity), 
            (GameObject) GameObject.Instantiate(headPrefab, corners[2].Center, Quaternion.identity),
            (GameObject) GameObject.Instantiate(playerPrefab, corners[1].Center, Quaternion.identity),
            (GameObject) GameObject.Instantiate(headPrefab, corners[3].Center, Quaternion.identity));

        foreach (Cell cell in magicTiles) {
            cell.SetIsMagic(true);
        }
    }
	
}
