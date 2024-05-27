using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleManagerData", menuName = "ObstacleManagerData", order = 0)]
public class ObstacleManagerData : ScriptableObject
{
    public List<Vector2Int> obstacleCoordinates;
}