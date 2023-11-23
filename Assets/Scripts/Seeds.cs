using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Seeds : MonoBehaviour
{
    public DataBase dbList;
    public GameObject seedButton;
    
    void Start()
    {
        StartCoroutine(waiter());
    }


    IEnumerator waiter()//Parece que Unity tarda en leer la base de datos así que esperamos un poco para que pueda devolver resultados
    {
        yield return new WaitForSeconds(0.5f);
        GenerateSeeds();
    }


    private void GenerateSeeds()//generamos la lista de semillas que tenemos disponibles
    {

        for (int i = 0; i < dbList.plantsListDB.Count; i++)
        {
            GameObject tmp = Instantiate(seedButton);
            tmp.transform.SetParent(this.gameObject.transform);

            tmp.GetComponentInChildren<TextMeshProUGUI>().text = dbList.plantsListDB[i].plant;

            Plant plantTMP;

            plantTMP.id_plant = dbList.plantsListDB[i].id_plant;
            plantTMP.plant = dbList.plantsListDB[i].plant;
            plantTMP.time = dbList.plantsListDB[i].time;
            plantTMP.quantity = dbList.plantsListDB[i].quantity;
            plantTMP.sell = dbList.plantsListDB[i].sell;
            plantTMP.buy = dbList.plantsListDB[i].buy;

            tmp.GetComponent<InventoryPlant>().plant = plantTMP;

        }



    }
}
