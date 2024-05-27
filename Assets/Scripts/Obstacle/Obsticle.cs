using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    [SerializeField] GameObject obsticlePrefab;
    [SerializeField] bool isPlaceable;
    Vector3 offset = new Vector3(0, 5f, 0);

    void Update()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.transform != null && raycastHit.transform.CompareTag("Tile"))
            {
                if (Input.GetMouseButtonDown(1) && isPlaceable)
                {
                    Instantiate(obsticlePrefab, transform.position + offset, Quaternion.identity);
                }

            }
        }
    }
}
