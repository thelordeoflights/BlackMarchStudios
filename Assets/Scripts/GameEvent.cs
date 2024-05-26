using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent", order = 0)]
public class GameEvent : ScriptableObject
{
    public UnityEvent<Vector2Int> Event = new UnityEvent<Vector2Int>();
}