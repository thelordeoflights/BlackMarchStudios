using UnityEngine;

public class SelectedTile : MonoBehaviour
{
    [HideInInspector] public Transform clickedTile;
    [SerializeField] Pathfinder pathfinder;
    [SerializeField] PlayerMover playerMover;
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
    }
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
            }
        }

    }


}
