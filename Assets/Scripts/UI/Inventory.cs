using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<KeyValuePair<string, int>> inventory = new List<KeyValuePair<string, int>>();
    [SerializeField] private Database database;
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject parent;
    private string selectedCrop;
    private GameObject selectedBorder;

    public static Inventory _INVENTORY;

    private void Awake()
    {
        if (_INVENTORY == null) _INVENTORY = this;
    }
    private void Start()
    {
        RawToInventory();
        GenerateList();
    }

    private void RawToInventory()
    {
        List<KeyValuePair<int, int>> raw = database.GetInventory();

        foreach (KeyValuePair<int, int> ids in raw)
        {
            string name = "";
            int quantity = 0;
            bool toadd = false;
            int pos = 0;

            foreach (var plant in database.plants)
            {
                if (plant.Id == ids.Key)
                {
                    name = plant.Name;
                    quantity = plant.Quantity;
                }
            }

            foreach (var item in inventory)
            {
                if (item.Key == name)
                {
                    toadd = true;
                }
                if (!toadd) pos++;
            }

            if (toadd)
            {
                quantity += inventory[pos].Value;
                inventory.Remove(inventory[pos]);
            }

            inventory.Add(new KeyValuePair<string, int>(name, quantity));
        }
    }

    private void GenerateList()
    {
        foreach (var item in inventory)
        {

            GameObject toInstance = Instantiate(cell, parent.transform);
            toInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Key;
            toInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + item.Value.ToString();
        } 
    }

    private void ClickedOutside()
    {
        if (selectedCrop != null)
        {
            selectedCrop = null;
            selectedBorder.SetActive(false);
            selectedBorder = null;
        }
    }
}
