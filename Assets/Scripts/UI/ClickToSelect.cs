using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickToSelect : MonoBehaviour
{
    public void Clicked()
    {
        Inventory._INVENTORY.SetSelected(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, transform.GetChild(2).gameObject);
    }
}
