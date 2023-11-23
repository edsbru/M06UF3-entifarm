using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int currentMoney = 0;
    TextMeshProUGUI moneyText;

    private void Start()
    {
       moneyText = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        moneyText.text = currentMoney.ToString();
    }

    public void AddMoney(int _amount)
    {
        currentMoney += _amount;
    }

}
