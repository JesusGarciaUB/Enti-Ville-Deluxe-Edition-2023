using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask cropLayer;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, cropLayer))
            {
                hit.transform.gameObject.GetComponent<Crop>().PrintCoords();
            }
        }
    }
}
