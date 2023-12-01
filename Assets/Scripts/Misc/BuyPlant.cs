using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyPlant : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI plantName;
    [SerializeField] private TextMeshProUGUI price;

    public void Buy()
    {
        if (decimal.Parse(price.text) <= Inventory._INVENTORY.GetMoney())
        {
            Inventory._INVENTORY.WasteMoney(decimal.Parse(price.text));
            Inventory._INVENTORY.AddToInventory(plantName.text);
        }
    }
}
