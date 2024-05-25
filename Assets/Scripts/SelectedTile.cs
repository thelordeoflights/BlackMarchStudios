using UnityEngine;

public class SelectedTile : MonoBehaviour
{
    [HideInInspector] public Transform clickedTile;
    void Update()
    {

        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.transform != null && raycastHit.transform == transform)
            {
                Debug.Log(transform.name);
            }
            if (Input.GetMouseButtonDown(0) && raycastHit.transform != null && raycastHit.transform == transform)
            {
                clickedTile = transform;
                Debug.Log("clicked tile" + transform);
            }
        }

    }


}
