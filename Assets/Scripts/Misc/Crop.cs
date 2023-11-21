using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private Vector2 coords;

    public void PrintCoords()
    {
        Debug.Log("X: " + coords.x + " - Y: " + coords.y);
    }
}
