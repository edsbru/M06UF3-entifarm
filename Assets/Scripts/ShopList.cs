using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopList : MonoBehaviour
{
    //voy a necesitar algo que mire cuantas entradas hay en la base de datos
    //probaremos a generar un botón por cada entrada que encuentre
    public DataBase dbList;
    public List<GameObject> shopButtons;
    public GameObject shopPanelButtonPrefab;

    
    

    void Start()
    {
        dbList = GameObject.Find("DataBase").GetComponent<DataBase>();
        
        GenerateShopButtons();
    }

    private void GenerateShopButtons()
    {
        //Genera tantos botones como entradas haya en la BD
        for (int i = 0; i < dbList.shopPlantsListDB.Count; i++)
        {
            //Creamos una shopPlanta temporal
            ShopPlant plantTMP;

            //Le asignamos los valores correspondientes en cada entrada EJ: Nabo
            plantTMP.growthTime = dbList.shopPlantsListDB[i].growthTime;//60s que tarda en crecer
            plantTMP.availableSeeds = dbList.shopPlantsListDB[i].availableSeeds;//8 semillas que tienes disponibles
            plantTMP.moneyPerPlant = dbList.shopPlantsListDB[i].moneyPerPlant;//1$ que te llevas por cada uno
            plantTMP.plantCost = dbList.shopPlantsListDB[i].plantCost;//0$ que cuesta comprarlos

            GameObject TMP = Instantiate(shopPanelButtonPrefab);

            TMP.transform.SetParent(this.transform);
            TMP.GetComponent<ShopButtonLogic>().SetInfo(plantTMP.growthTime, plantTMP.availableSeeds, plantTMP.moneyPerPlant, plantTMP.plantCost);
          
        }
    }
}
