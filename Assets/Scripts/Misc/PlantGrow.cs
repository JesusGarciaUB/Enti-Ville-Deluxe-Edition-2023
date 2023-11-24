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

    public bool grown = false;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (transform.localScale.x < 1)
        {
            transform.localScale += new Vector3(1 / growthTime * Time.deltaTime, 1 / growthTime * Time.deltaTime, 1 / growthTime * Time.deltaTime);
        } else
        {
            grown = true;
        }
    }
    public void PlantThis(int _id, string _plantName, float _growthTime, decimal _sellPrice)
    {
        id = _id;
        plantName = _plantName;
        growthTime = _growthTime;
        sellPrice = _sellPrice;

        Debug.Log(growthTimeFactor.x);
    }
    public void Recolect()
    {
        Destroy(gameObject);
    }
}
