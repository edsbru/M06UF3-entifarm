using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//Botón desactivado si:
    //Si no tienes dinero para comprar la planta

public struct ShopPlant
{
    public float growthTime;
    public int availableSeeds;
    public float moneyPerPlant;
    public float plantCost;
}
public class ShopButtonLogic : MonoBehaviour
{
   
    public DataBase dbList;

    [SerializeField]
    private TextMeshProUGUI growthTime;
    [SerializeField]
    private TextMeshProUGUI availableSeeds;
    [SerializeField]
    private TextMeshProUGUI moneyPerPlant;
    [SerializeField]
    private TextMeshProUGUI plantCost;

    // Start is called before the first frame update
    void Start()
    {
      

        dbList = GameObject.Find("DataBase").GetComponent<DataBase>();

    }

    public void SetInfo(float _growthTime, int _availableSeeds, float _moneyPerPlant, float _plantCost)
    {
        
        growthTime.text = _growthTime.ToString() + " sec";
        availableSeeds.text = _availableSeeds.ToString() + " seeds";
        moneyPerPlant.text = _moneyPerPlant.ToString() + "$/plant";
        plantCost.text = "Cost: " + _plantCost.ToString() + "$";

    }
}