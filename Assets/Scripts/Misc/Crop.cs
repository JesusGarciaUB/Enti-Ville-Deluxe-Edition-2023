using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private Vector2 coords;
    private GameObject plant;
    
    public bool CanPlant()
    {
        return plant == null;
    }

    public void SetPlant(GameObject _plant)
    {
        plant = _plant;
    }
    public GameObject GetPlant() { return plant; }
    public Vector2 GetCoords() { return coords; }
}
