using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseShop : MonoBehaviour
{
    [SerializeField] private GameObject shop;

    public void SetShopVisibility()
    {
        shop.SetActive(!shop.activeSelf);
    }
}
