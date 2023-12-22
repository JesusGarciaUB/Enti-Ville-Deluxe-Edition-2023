using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject parent;
    private Database database;

    private void Start()
    {
        database = Database._DATABASE;
        CellsBucket._CELLS.shopLoaded = true;
        GenerateShop();
    }

    private void GenerateShop()
    {
        foreach (Plant plant in database.getPlants())
        {
            GameObject toInstance = Instantiate(cell, parent.transform);
            toInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = plant.Name;
            toInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = plant.BuyPrice.ToString();
            toInstance.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BUY X" + plant.Quantity;
        }
    }
}
