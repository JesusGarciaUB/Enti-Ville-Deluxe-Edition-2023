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

    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, cropLayer))
            {
                hit.transform.gameObject.GetComponent<Crop>().PrintCoords();
            }
            
            List<RaycastResult> results = MousePoint();

            foreach (var result in results)
            {
                if(result.gameObject.layer == buttonLayer)
                {
                    Debug.Log("hola");
                }
            }
        }
    }

    private List<RaycastResult> MousePoint()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        return results;
    }
}
