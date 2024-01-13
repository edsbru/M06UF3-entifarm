using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopButtonLogic : MonoBehaviour
{
   
    public DataBase dbList;

    [SerializeField]
    private TextMeshProUGUI plantName;
    [SerializeField]
    private TextMeshProUGUI growthTime;
    [SerializeField]
    private TextMeshProUGUI availableSeeds;
    [SerializeField]
    private TextMeshProUGUI moneyPerPlant;
    [SerializeField]
    private TextMeshProUGUI plantCost;
    
    private Seeds seedsScript;
    private MoneyManager moneyManager;

    public Plant buttonPlant;

    // Start is called before the first frame update
    void Start()
    {
        seedsScript = GameObject.Find("Content").GetComponent<Seeds>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        dbList = GameObject.Find("DataBase").GetComponent<DataBase>();
    }

    public void SetInfo(string _plantName, float _growthTime, int _availableSeeds, float _moneyPerPlant, float _plantCost)
    {
        plantName.text = _plantName.ToString();
        growthTime.text = "Growth time: " + _growthTime.ToString() + " sec";
        availableSeeds.text = "Get: "+ _availableSeeds.ToString() + " seeds";
        moneyPerPlant.text = _moneyPerPlant.ToString() + "$/plant";
        plantCost.text = "Cost: " + _plantCost.ToString() + "$";

    }

    public void BuyPlant()
    {
        if ((float)moneyManager.currentMoney >= buttonPlant.buy)
        {
            for(int i = 0; i < seedsScript.inventoryButtons.Count; i++)
            {
                if (buttonPlant.id_plant == seedsScript.inventoryButtons[i].GetComponent<InventoryPlant>().plant.id_plant) //Miramos si la id de  la planta del boton es la misma que la del inventario
                {
                    moneyManager.currentMoney -= (decimal)buttonPlant.buy;
                    seedsScript.inventoryButtons[i].GetComponent<InventoryPlant>().plant.invetoryQuantity += buttonPlant.quantity;
                    seedsScript.UpdateText();
                }
            }
        }
    }
}