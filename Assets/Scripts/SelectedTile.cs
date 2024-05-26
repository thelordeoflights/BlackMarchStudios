using UnityEngine;

public class SelectedTile : MonoBehaviour
{
    [HideInInspector] public Transform clickedTile;
    [SerializeField] Pathfinder pathfinder;
    [SerializeField] PlayerMover playerMover;
    [SerializeField] GridManager gridManager;
    Vector3 offset = new Vector3(0, 5f, 0);
    void Update()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.transform != null && raycastHit.transform.CompareTag("Tile"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 destination = new Vector2(raycastHit.transform.position.x, raycastHit.transform.position.z);
                    Vector2Int des = Vector2Int.RoundToInt(destination) / 10;
                    pathfinder.StartFindingPath(des);
                    playerMover.RecalculatePath(true);
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    Vector2 destination = new Vector2(raycastHit.transform.position.x, raycastHit.transform.position.z);
                    Vector2Int des = Vector2Int.RoundToInt(destination) / 10;
                    gridManager.PlaceObstacle(des);
                    // if (gridManager.GetNode(tile.coordinates).isWalkable && !pathfinder.WillBlockPath(tile.coordinates))
                    // {
                    //     Debug.Log(tile.coordinates);
                    //     tile.isPlaceable = !tile.isPlaceable;
                    //     gridManager.BlockedNode(tile.coordinates);
                    //     Instantiate(tile.obsticlePrefab, transform.position + offset, Quaternion.identity);
                    // }
                }
            }

        }

    }


}
