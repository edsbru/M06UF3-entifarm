using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Seeds : MonoBehaviour
{
    //Variables del inspector
    public DataBase dbList;
    public GameObject seedButton;
    public List<GameObject> inventoryButtons = new List<GameObject>();
    private PlantSelector plantSelector;
    
    void Start()
    {
        plantSelector = GameObject.Find("PlantSelector").GetComponent<PlantSelector>();//Pillamos el contenido de PlantSelector
        //StartCoroutine(waiter());//Iniciamos la corrutina waiter()
        GenerateSeeds();
    }

    private void Update()
    {
        UpdateText();//Revisamos la cantidad de semillas a cada Frame
    }

    private void GenerateSeeds()//generamos la lista de semillas que tenemos disponibles
    {
        //Creamos tantas entradas como plantas haya en la base de datos
        for (int i = 0; i < dbList.plantsListDB.Count; i++)
        {
            GameObject tmp = Instantiate(seedButton);
            tmp.transform.SetParent(this.gameObject.transform);

            Plant plantTMP;//creamos una PLANT temporal

            plantTMP.id_plant = dbList.plantsListDB[i].id_plant;
            plantTMP.plant = dbList.plantsListDB[i].plant;
            plantTMP.time = dbList.plantsListDB[i].time;
            plantTMP.quantity = dbList.plantsListDB[i].quantity;
            plantTMP.sell = dbList.plantsListDB[i].sell;
            plantTMP.buy = dbList.plantsListDB[i].buy;

            tmp.GetComponent<InventoryPlant>().plant = plantTMP;
            tmp.GetComponent<InventoryPlant>().plant.quantity = plantTMP.quantity;

            inventoryButtons.Add(tmp);//A�adimos el bot�n generado
        }
        UpdateText();
    }

    public void UpdateText()//Funci�n para actualizar el texto de los botones del inventario a medida que pones y recoges plantas
    {
        for (int i = 0; i < inventoryButtons.Count; i++) {//La funci�n se repetir� mientras queden plantas en la base de datos
            inventoryButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = dbList.plantsListDB[i].plant + " " + inventoryButtons[i].GetComponent<InventoryPlant>().plant.quantity;
        }   
    }
}
