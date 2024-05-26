using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } set { isPlaceable = value; } }
    public GameObject obsticlePrefab;
    GridManager gridManager;

    public Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }
    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockedNode(coordinates);
            }
        }
    }
    // void Update()
    // {
    //     RaycastHit raycastHit;
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     if (Physics.Raycast(ray, out raycastHit, 100f))
    //     {
    //         if (raycastHit.transform != null && raycastHit.transform.CompareTag("Tile"))
    //         {
    //             if (Input.GetMouseButtonDown(1) && isPlaceable)
    //             {
    //                 Instantiate(obsticlePrefab, transform.position + offset, Quaternion.identity);
    //             }

    //         }
    //     }
    // }

}
