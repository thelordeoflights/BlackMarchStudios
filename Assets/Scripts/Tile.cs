using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
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
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (Input.GetMouseButtonDown(1) && raycastHit.transform != null && raycastHit.transform == transform)
                {
                    if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
                    {
                        isPlaceable = !isPlaceable;
                        gridManager.BlockedNode(coordinates);
                    }
                }

            }
        }

    }
}