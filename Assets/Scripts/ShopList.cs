using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopList : MonoBehaviour
{
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
            Plant plantTMP;

            //Le asignamos los valores correspondientes en cada entrada EJ: Nabo
            plantTMP.id_plant = dbList.shopPlantsListDB[i].id_plant;
            plantTMP.plant = dbList.shopPlantsListDB[i].plant;
            plantTMP.time = dbList.shopPlantsListDB[i].time;//60s que tarda en crecer
            plantTMP.quantity = dbList.shopPlantsListDB[i].quantity;//8 semillas que tienes disponibles
            plantTMP.sell = dbList.shopPlantsListDB[i].sell;//1$ que te llevas por cada uno
            plantTMP.buy = dbList.shopPlantsListDB[i].buy;//0$ que cuesta comprarlos
            plantTMP.invetoryQuantity = 0;

            GameObject TMP = Instantiate(shopPanelButtonPrefab);

            TMP.transform.SetParent(this.transform);
            TMP.GetComponent<ShopButtonLogic>().buttonPlant = plantTMP;
            TMP.GetComponent<ShopButtonLogic>().SetInfo(plantTMP.plant, plantTMP.time, plantTMP.quantity, plantTMP.sell, plantTMP.buy);
            
        }
    }
}
