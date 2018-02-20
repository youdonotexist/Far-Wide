using UnityEngine;
using System.Collections;
using Gamelogic.Grids;
using UnityEngine.Networking;

public class Cell : NetworkBehaviour, IGLScriptableObject {

    [SerializeField]
    private Sprite normalSprite;

    [SerializeField]
    private Sprite magicSprite;

    [SerializeField]
    private Sprite phantomSwitchSprite;

    [SyncVar(hook="SetColor")][SerializeField] 
    private Color color = Color.white;

    [SyncVar(hook="SetTileType")] 
    private TileType tileType;

    [SerializeField]
    private Vector3 centerOffset = Vector3.zero;

    private SpriteRenderer spriteRenderer;
    /**
        The visual center of a cell.
        
        This is useful for cells that do not have their actual pivot 
        at the "expected" place, such as mesh tile cells in polar grids.            
    */
    public Vector3 Center
    {
        get {return transform.TransformPoint(__CenterOffset); }
    }

    /**
        Should be set by the grid creator to a value that can serve as the 
        center of the cell (if the cell is at the origin).

        @usedbyeditor
    **/

    public Vector3 __CenterOffset
    {
        get { return centerOffset; }
        set { centerOffset = value; }
    }

    protected SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null) {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
                
            if (spriteRenderer == null) {
                Debug.LogError("The cell needs a child with a SpriteRenderer component attached");
            }

            return spriteRenderer;
        }
    }

    public Color Color
    {
        get { return color; }

        set
        {
            color = value;
            __UpdatePresentation(true);
        }
    }

    public Vector2 Dimensions
    {
        get { return SpriteRenderer.sprite.bounds.size; }
    }

    void OnStart() {
        __UpdatePresentation(true);
    }

    public void __UpdatePresentation(bool forceUpdate)
    {
        switch(tileType) {
        case TileType.BASIC: 
            SpriteRenderer.color = Color.white;
            SpriteRenderer.sprite = normalSprite;
            break;
        case TileType.MAGIC:
            SpriteRenderer.color = Color.white;
            SpriteRenderer.sprite = magicSprite;
            break;
        case TileType.PHANTOM:
            SpriteRenderer.color = Color.white; //TODO colorize for currently targeted player
            SpriteRenderer.sprite = phantomSwitchSprite;
            break;
        }   
    }

    private void SetColor(Color c) {
        Color = c;
    }

    public void SetTileType(TileType type) {
        tileType = type;
        __UpdatePresentation(true);
    }

    public TileType GetTileType() {
        return tileType;
    }

    public enum TileType {
        BASIC,
        MAGIC,
        PHANTOM
    };
}
