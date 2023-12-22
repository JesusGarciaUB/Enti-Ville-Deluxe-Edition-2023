using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsBucket : MonoBehaviour
{
    [SerializeField] private List<Crop> crops;
    public static CellsBucket _CELLS;
    public bool inventoryLoaded = false;
    public bool shopLoaded = false;
    public bool loaded = false;

    private void Awake()
    {
        _CELLS = this;  
    }

    private void Update()
    {
        if (!loaded && shopLoaded && inventoryLoaded)
        {
            Database._DATABASE.LoadGame();
            loaded = true;
        }
    }

    public List<Crop> GetCrops()
    {
        return crops;
    }
}
