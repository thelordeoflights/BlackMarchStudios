using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject gridGameObject;
    [SerializeField] Vector2Int gridSize;
    [Tooltip("World Grid Size - Should match unityEditor Snap Settings")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get { return unityGridSize; } }
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }
    public Dictionary<Vector2Int, GameObject> Tiles = new Dictionary<Vector2Int, GameObject>();
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] ObstacleManager obstacleManager;

    void Awake()
    {
        CreateGrid();
    }
    public void PlaceObstacle(Vector2Int coordinates)
    {
        Node obstacleTile = GetNode(coordinates);
        if (obstacleTile != null)
        {
            if (obstacleTile.isWalkable)
            {
                obstacleManager.Addobstacle(coordinates);
                BlockedNode(coordinates);
            }
            else
            {
                obstacleManager.Removeobstacle(coordinates);
                UnBlockedNode(coordinates);
            }
        }
    }


    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
    }
    public void BlockedNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }
    public void UnBlockedNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = true;
        }
    }
    public void ResetNode()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;
        return position;
    }
    void CreateGrid()
    {

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 tilePosition = new(x, 0, y);
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
                var tileGameObject = Instantiate(tilePrefab, tilePosition * unityGridSize, Quaternion.identity);

                tileGameObject.transform.parent = gridGameObject.transform;
                Tiles.Add(coordinates, tileGameObject);
            }
        }
    }

}
