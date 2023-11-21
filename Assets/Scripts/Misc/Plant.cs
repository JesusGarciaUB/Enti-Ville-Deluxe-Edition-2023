using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Plant : ScriptableObject
{
    private int id;
    private string plantName;
    private float growthTime;
    private int quantity;
    private decimal sellPrice;
    private decimal buyPrice;
    private GameObject prefab;

    private void Init(int _id, string _name, float _growthTime, int _quantity, decimal _sellPrice, decimal _buyPrice, GameObject _prefab)
    {
        id = _id;
        plantName = _name;
        growthTime = _growthTime;
        quantity = _quantity;
        sellPrice = _sellPrice;
        buyPrice = _buyPrice;
        prefab = _prefab;
    }

    public static Plant CreatePlant(int _id, string _name, float _growthTime, int _quantity, decimal _sellPrice, decimal _buyPrice, GameObject _prefab)
    {
        var plant = ScriptableObject.CreateInstance<Plant>();
        plant.Init(_id, _name, _growthTime, _quantity, _sellPrice, _buyPrice, _prefab);
        return plant;
    }

    public int Id { get { return id; } }
    public string Name { get {  return plantName; } }
    public float GrowthTime { get {  return growthTime; } }
    public decimal BuyPrice { get {  return buyPrice; } }
    public int Quantity { get { return quantity; } }
    public decimal SellPrice { get { return sellPrice; } }

}
