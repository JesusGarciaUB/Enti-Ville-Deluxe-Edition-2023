using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CellBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI plantName;
    [SerializeField] private TextMeshProUGUI plantCount;

    public void SetValue(string name, string count)
    {
        plantName.text = name;
        plantCount.text = count;
    }
}
