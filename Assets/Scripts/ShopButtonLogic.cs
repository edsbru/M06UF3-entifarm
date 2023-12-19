using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//Botón desactivado si:
    //Si no tienes dinero para comprar la planta

public struct ShopPlant
{
    public string growthTime;
    public string availableSeeds;
    public string moneyPerPlant;
    public string plantCost;
}
public class ShopButtonLogic : MonoBehaviour
{
    public List<ShopPlant> shopPlantsList;
    public DataBase dbList;
    private TextMeshProUGUI growthTime;
    private TextMeshProUGUI availableSeeds;
    private TextMeshProUGUI moneyPerPlant;
    private TextMeshProUGUI plantCost;

    // Start is called before the first frame update
    void Start()
    {
        growthTime = GetComponentInChildren<TextMeshProUGUI>();
        availableSeeds = GetComponentInChildren<TextMeshProUGUI>();
        moneyPerPlant = GetComponentInChildren<TextMeshProUGUI>();
        plantCost = GetComponentInChildren<TextMeshProUGUI>();

        GenerateShopButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateShopButtons()
    {
        //Genera tantos botones como entradas haya en la BD
        for (int i = 0; i < dbList.plantsListDB.Count; i++)
        {
            //Creamos una planta temporal
            ShopPlant plantTMP;

            //Le asignamos los valores correspondientes en cada entrada EJ: Nabo
            plantTMP.growthTime = dbList.plantsListDB[i].time.ToString();//60s que tarda en crecer
            plantTMP.availableSeeds = dbList.plantsListDB[i].quantity.ToString();//8 semillas que tienes disponibles
            plantTMP.moneyPerPlant = dbList.plantsListDB[i].sell.ToString();//1$ que te llevas por cada uno
            plantTMP.plantCost = dbList.plantsListDB[i].buy.ToString();//0$ que cuesta comprarlos
            
            shopPlantsList.Add(plantTMP);
        }
    }
}