using UnityEngine;

public enum HexSide
{
    Left,
    Middle,
    Right
}

public class Hex : MonoBehaviour
{
    public Vector2Int coordinates;
    public bool isOccupied = false;
    public GameObject troop;
    public HexSide hexSide;

    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                originalColor = spriteRenderer.color;
            }
            else
            {
                Debug.LogError("SpriteRenderer not found on Hex");
            }
        }
    }

    public void SetOccupied(bool occupied)
    {
        if (spriteRenderer == null)
        {
            Initialize();
        }
        isOccupied = occupied;
        spriteRenderer.color = isOccupied ? originalColor : originalColor; // Keep original color when occupied
    }

    public void Highlight(bool highlight)
    {
        if (spriteRenderer == null)
        {
            Initialize();
        }
        spriteRenderer.color = highlight ? Color.yellow : originalColor;
    }

    void OnMouseDown()
    {
        if (troop != null)
        {
            MapManager.Instance.OnTroopSelected(this);
        }
    }
}