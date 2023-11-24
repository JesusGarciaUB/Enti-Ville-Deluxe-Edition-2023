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
}
