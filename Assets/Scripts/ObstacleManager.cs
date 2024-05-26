using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] ObstacleManagerData obstacleManagerData;
    [SerializeField] GridManager gridManager;
    Dictionary<Vector2Int, GameObject> Obstaclelist = new Dictionary<Vector2Int, GameObject>();
    Vector3 offset = new Vector3(0, 0.5f, 0);
    void Start()
    {
        obstacleManagerData.obstacleCoordinates = new();
    }
    public void Addobstacle(Vector2Int coordinates)
    {
        obstacleManagerData.obstacleCoordinates.Add(coordinates);
        Vector3 obstaclePosition = new Vector3(coordinates.x, 0, coordinates.y);
        var ObstacleGO = Instantiate(obstacle, (obstaclePosition + offset) * gridManager.UnityGridSize, Quaternion.identity);
        Obstaclelist.Add(coordinates, ObstacleGO);
    }
    public void Removeobstacle(Vector2Int coordinates)
    {
        if (Obstaclelist.ContainsKey(coordinates))
        {
            Destroy(Obstaclelist[coordinates]);
            Obstaclelist.Remove(coordinates);

        }
        if (obstacleManagerData.obstacleCoordinates.Contains(coordinates))
        {
            obstacleManagerData.obstacleCoordinates.Remove(coordinates);
        }
    }

}
