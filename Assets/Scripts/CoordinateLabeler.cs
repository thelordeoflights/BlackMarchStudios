using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.blue;
    [SerializeField] Color pathColor = Color.red;
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    void Awake()
    {
        gridManager = FindAnyObjectByType<GridManager>();
        label = GetComponent<TextMeshPro>();
        // label.enabled = false;
        DisplayCoordinates();
    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectname();
            //label.enabled = true;
        }
        SetLabelColor();
        Togglelabel();
    }
    void Togglelabel()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            label.enabled = !label.enabled;
        }
    }
    void DisplayCoordinates()
    {
        if (gridManager == null)
        {
            return;
        }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.unityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.unityGridSize);

        label.text = coordinates.x + "," + coordinates.y;
    }
    void UpdateObjectname()
    {
        transform.parent.name = coordinates.ToString();
    }
    void SetLabelColor()
    {
        if (gridManager == null)
        {
            return;
        }
        Node node = gridManager.GetNode(coordinates);
        if (node == null)
        {
            return;
        }
        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }
}
