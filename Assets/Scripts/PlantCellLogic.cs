using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class PlantCellLogic : MonoBehaviour
{
    public int fieldPosX;//Posición X relativa al field
    public int fieldPosY;//Posición Y relativa al field
    public PlantSelector selector;
    private TextMeshProUGUI text; //Texto del botón
    public Plant plantedSeed;

    public float timer = 0;
    private bool isPlanted = false;

    private bool isGrown = false;
    private bool canPlant = true;

    private void Start()
    {
        selector = GameObject.Find("PlantSelector").GetComponent<PlantSelector>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {

        if (isPlanted == true)
        {
            timer += Time.deltaTime;

            if(timer >= plantedSeed.time)
            {
                text.text = "Terminado";
                isGrown = true;
                isPlanted = false;
            }

        }
       
        
    }

    public void onPlantCellClick()//al pulsar celda de la planta
    {
        if (canPlant == true) { 
             canPlant = false;
             plantedSeed.id_plant = selector.selectedPlant.id_plant;//Recoge los datos de la planta en PlantSelector
             plantedSeed.plant = selector.selectedPlant.plant;
             plantedSeed.time = selector.selectedPlant.time;
             plantedSeed.quantity = selector.selectedPlant.quantity;
             plantedSeed.sell = selector.selectedPlant.sell;
             plantedSeed.buy = selector.selectedPlant.buy;

             text.text = selector.selectedPlant.plant; //El texto del boton con el nombre

             isPlanted = true;
             timer = 0;//Reiniciamos temporizador
        }
        else if (isGrown)
        {
            
            isGrown = false;
            canPlant = true;
            text.text = " ";
            GameObject.Find("MoneyManager").GetComponent<MoneyManager>().AddMoney(plantedSeed.sell);
        }
    }

}
