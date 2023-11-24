using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnClickBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask cropLayer;
    [SerializeField] private LayerMask buttonLayer;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, cropLayer))
            {
                //Si pulsamos en una plantación y tenemos seleccionada una plantación
                if (Inventory._INVENTORY.selectedCrop != null && Inventory._INVENTORY.CanPlant() && hit.transform.gameObject.GetComponent<Crop>().CanPlant())
                {
                    
                    Inventory._INVENTORY.Planted();
                }
            } else
            {
                Inventory._INVENTORY.ClickedOutside();
            }

        }

    }

}
