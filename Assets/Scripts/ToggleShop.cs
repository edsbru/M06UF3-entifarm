using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShop : MonoBehaviour
{
    public GameObject shopPanel = GameObject.Find("ShopPanel");

    private void Start()
    {
        shopPanel.SetActive(false);
    }
    
    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }
    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }
}
