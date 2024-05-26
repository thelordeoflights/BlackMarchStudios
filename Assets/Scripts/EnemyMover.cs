using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] float speed = 1f;
    List<Node> path = new List<Node>();
    [SerializeField] GridManager gridManager;
    [SerializeField] GameEvent playerMovedEvent;
    [SerializeField] PathFinderEnemy pathfinder;


    void Awake()
    {
        playerMovedEvent.Event.AddListener(MoveHandler);
        ReturnToStart();

    }

    private void MoveHandler(Vector2Int destination)
    {
        pathfinder.StartFindingPath(destination);
        RecalculatePath(true);
    }

    public void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath();
        StartCoroutine(FollowPath());

    }
    Vector3 offset = new Vector3(0, 5f, 0);
    void ReturnToStart()
    {
        //transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates) + offset;
    }
    void FinishPath(Vector3 finishPosition)
    {
        if (finishPosition != Vector3.zero)
        {
            Vector2 temPos = new Vector2(finishPosition.x, finishPosition.z);
            Vector2Int temPosInt = Vector2Int.RoundToInt(temPos) / 10;
            pathfinder.StartCoordinates = temPosInt;
            Debug.Log(temPosInt);
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 lastPosition = Vector3.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates) + offset;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            lastPosition = endPosition;
        }
        FinishPath(lastPosition);
    }
}
