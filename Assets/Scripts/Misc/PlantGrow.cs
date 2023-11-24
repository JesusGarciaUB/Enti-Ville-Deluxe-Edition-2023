using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrow : MonoBehaviour
{
    private int id;
    private string plantName;
    private float growthTime;
    private decimal sellPrice;
    private Vector3 growthTimeFactor;

    private void Awake()
    {
        transform.localScale = growthTimeFactor;
    }

    private void Update()
    {
        if (growthTime > 0)
        {
            transform.localScale += growthTimeFactor;
            growthTime -= Time.deltaTime;
        }
    }
    public void PlantThis(int _id, string _plantName, float _growthTime, decimal _sellPrice)
    {
        id = _id;
        plantName = _plantName;
        growthTime = _growthTime;
        sellPrice = _sellPrice;

        growthTimeFactor = new Vector3(growthTime * Time.deltaTime, growthTime * Time.deltaTime, growthTime * Time.deltaTime);
    }
}
