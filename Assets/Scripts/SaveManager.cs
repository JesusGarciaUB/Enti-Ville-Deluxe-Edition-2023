using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private void Awake()
    {
        Database._DATABASE.LoadGame();
    }
}
