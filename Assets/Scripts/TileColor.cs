using UnityEngine;

public class TileColor : MonoBehaviour
{
    [SerializeField] GameObject _tile;
    void Start()
    {
    }
    public void DefaultTileColor()
    {
        var tileRenderer = _tile.GetComponent<Renderer>();
        tileRenderer.material.SetColor("_Color", Color.white);
    }
    public void SetTileColor()
    {
        var tileRenderer = _tile.GetComponent<Renderer>();
        tileRenderer.material.SetColor("_Color", Color.red);
    }
}
