using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public GameObject shopPanel;

    private void Update()
    {
        PauseTimeWhenShopOpened();
    }

    void PauseTimeWhenShopOpened()
    {
        if (shopPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
