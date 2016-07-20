using UnityEngine;
using System.Collections;
using Gamelogic.Grids;
using UnityEngine.Networking;

public class Cell : NetworkBehaviour, IGLScriptableObject {

    [SerializeField]
    private Sprite normalSprite;

    [SerializeField]
    private Sprite magicSprite;

    [SyncVar(hook="SetHighlight")][SerializeField] 
    private bool highlightOn;

    [SyncVar(hook="SetColor")][SerializeField] 
    private Color color;

    [SyncVar] 
    private bool isMagic;

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
                
            if (spriteRenderer == null)
            {
                Debug.LogError("The cell needs a child with a SpriteRenderer component attached");
            }

            return spriteRenderer;
        }
    }

    public bool HighlightOn
    {
        get { return highlightOn; }

        set
        {
            highlightOn = value;
            __UpdatePresentation(true);
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
        SpriteRenderer.color = highlightOn ? Color.Lerp(color, Color.white, 0.8f) : color;
    }

    private void SetColor(Color c) {
        Color = c;
    }

    private void SetHighlight(bool onHighlight) {
        HighlightOn = onHighlight;
    }

    private void Update() {
        SpriteRenderer.sprite = isMagic ? magicSprite : normalSprite;
    }

    public void SetIsMagic(bool magic) {
        isMagic = magic;
    }
}
