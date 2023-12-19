using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShop : MonoBehaviour
{
    public GameObject shopPanel;

    private void Start()
    {
        shopPanel = GameObject.Find("ShopPanel");
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
