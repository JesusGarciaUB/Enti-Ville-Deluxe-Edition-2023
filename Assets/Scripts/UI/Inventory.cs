using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Inventory : MonoBehaviour
{
    private List<KeyValuePair<string, int>> inventory = new List<KeyValuePair<string, int>>();
    private Database database;
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject parent;
    public Plant selectedCrop;
    public GameObject selectedBorder;
    private bool plantedThisFrame = false;
    private int plantedThisFramePos = 0;
    [SerializeField] private TextMeshProUGUI moneyText;
    private decimal money = 0;

    public static Inventory _INVENTORY;

    private void Awake()
    {
        if (_INVENTORY == null) _INVENTORY = this;
        CellsBucket._CELLS.inventoryLoaded = true;
    }
    private void Start()
    {
        database = Database._DATABASE;
        RawToInventory();
        GenerateList();
    }

    private void Update()
    {
        if (plantedThisFrame)
        {
            ReturnToNormal();
            plantedThisFrame = false;
        }
    }

    public bool ChangeOnMoney(decimal val)
    {
        if (money + val >= 0)
        {
            money += val;
            moneyText.text = money.ToString();
            return true;
        }
        return false;
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

            foreach (var plant in database.getPlants())
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
        for(int x = 0; x < parent.transform.childCount; x++)
        {
            Destroy(parent.transform.GetChild(x).gameObject);
        }

        foreach (var item in inventory)
        {

            GameObject toInstance = Instantiate(cell, parent.transform);
            toInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Key;
            toInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + item.Value.ToString();

            if (selectedCrop != null && item.Key == selectedCrop.Name)
            {
                toInstance.transform.GetChild(2).gameObject.SetActive(true);
                selectedBorder = toInstance.transform.GetChild(2).gameObject;
            }
        } 
    }
    public void ClickedOutside()
    {
        if (selectedCrop != null)
        {
            selectedCrop = null;
            selectedBorder.SetActive(false);
            selectedBorder = null;
        }
    }
    public void SetSelected(string crop, GameObject border)
    {
        if (selectedCrop != null)
        {
            selectedBorder.SetActive(false);
        }
        selectedBorder = border;
        selectedBorder.SetActive(true);

        foreach (var plant in database.getPlants())
        {
            if (plant.Name == crop)
            {
                selectedCrop = plant;
                return;
            }
        }
    }
    public void Planted()
    {
        int pos = 0;

        foreach(var item in inventory)
        {
            if (selectedCrop.Name == item.Key)
            {
                KeyValuePair<string, int> newValue = new KeyValuePair<string, int>(item.Key, item.Value - 1);
                inventory.Remove(item);
                inventory.Add(newValue);

                plantedThisFrame = true;
                plantedThisFramePos = pos;
                return;
            }
            pos++;
        }
    }
    public void UnPlanted(string name)
    {
        int pos = 0;

        foreach (var item in inventory)
        {
            if (name == item.Key)
            {
                KeyValuePair<string, int> newValue = new KeyValuePair<string, int>(item.Key, item.Value + 1);
                inventory.Remove(item);
                inventory.Add(newValue);

                plantedThisFrame = true;
                plantedThisFramePos = pos;
                return;
            }
            pos++;
        }
    }
    public void ReturnToNormal()
    {
        for (int i = plantedThisFramePos; i < inventory.Count - 1; i++)
        {
            inventory.Add(new KeyValuePair<string, int>(inventory[plantedThisFramePos].Key, inventory[plantedThisFramePos].Value));
            inventory.Remove(inventory[plantedThisFramePos]);
        }

        GenerateList();
    }
    public bool CanPlant()
    {
        foreach (var item in inventory)
        {
            if (selectedCrop.Name == item.Key)
            {
                return item.Value > 0;
            }
        }
        
        return false;
    }
    public decimal GetMoney()
    {
        return money;
    }
    public void WasteMoney(decimal moneyWasted)
    {
        this.money -= moneyWasted;
        moneyText.text = money.ToString();
    }
    public void AddToInventory(string plantName)
    {
        int quantity = 0;
        bool toadd = false;
        int pos = 0;

        foreach (var plant in database.getPlants())
        {
            if (plant.Name == plantName)
            {
                quantity = plant.Quantity;
                Database._DATABASE.BuySomething(plant.Id);
            }
        }

        foreach (var item in inventory)
        {
            if (item.Key == plantName)
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

        inventory.Add(new KeyValuePair<string, int>(plantName, quantity));

        for (int i = pos; i < inventory.Count - 1; i++)
        {
            inventory.Add(new KeyValuePair<string, int>(inventory[pos].Key, inventory[pos].Value));
            inventory.Remove(inventory[pos]);
        }

        GenerateList();
    }
    public void PlantedFromLoad() { plantedThisFrame = false; }
}
