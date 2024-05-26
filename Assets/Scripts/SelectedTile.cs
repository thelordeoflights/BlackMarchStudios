using TMPro;
using UnityEngine;

public class SelectedTile : MonoBehaviour
{
    [HideInInspector] public Transform clickedTile;
    [SerializeField] Pathfinder pathfinder;

    [SerializeField] PlayerMover playerMover;
    [SerializeField] GridManager gridManager;
    [SerializeField] Material defaultColor;
    [SerializeField] Material selectedColor;
    [SerializeField] GameEvent playerMovedEvent;
    GameObject selectedTile;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    void Update()
    {

        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (selectedTile != null)
            {
                selectedTile.GetComponentInChildren<Renderer>().material = defaultColor;
            }
            if (raycastHit.transform != null && raycastHit.transform.CompareTag("Tile"))
            {
                selectedTile = raycastHit.transform.gameObject;
                textMeshProUGUI.text = "Tile:" + raycastHit.transform.position.x / 10 + "," + raycastHit.transform.position.z / 10;
                selectedTile.GetComponentInChildren<Renderer>().material = selectedColor;
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 destination = new Vector2(raycastHit.transform.position.x, raycastHit.transform.position.z);
                    Vector2Int des = Vector2Int.RoundToInt(destination) / 10;
                    pathfinder.StartFindingPath(des);
                    playerMovedEvent.Event.Invoke(des);
                    playerMover.RecalculatePath(true);
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    Vector2 destination = new Vector2(raycastHit.transform.position.x, raycastHit.transform.position.z);
                    Vector2Int des = Vector2Int.RoundToInt(destination) / 10;
                    gridManager.PlaceObstacle(des);

                }
            }

        }
    }

}



