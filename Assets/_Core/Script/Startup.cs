using UnityEngine;
using System.Collections;
using Gamelogic.Grids;

public class Startup : MonoBehaviour {

    [SerializeField]
    private FarAndWide.FWGrid fwGrid;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject headPrefab;

	// Use this for initialization
	void Start () {
        SpriteCell[] corners = fwGrid.getCorners();

        GameObject.Instantiate(playerPrefab, corners[0].Center, Quaternion.identity);
        GameObject.Instantiate(playerPrefab, corners[1].Center, Quaternion.identity);
        GameObject.Instantiate(headPrefab, corners[2].Center, Quaternion.identity);
        GameObject.Instantiate(headPrefab, corners[3].Center, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
