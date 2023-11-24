using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using static Unity.Burst.Intrinsics.X86.Avx;

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
                text.text = "Collect";
                isGrown = true;
                isPlanted = false;
            }
            else if (timer >= plantedSeed.time / 2)
            {
                text.text = "Growing";
            }

        }
       
        
    }

    public void onPlantCellClick()//al pulsar celda de la planta
    {
        if (canPlant == true && selector.selectedPlant.quantity > 0) { 
             canPlant = false;
             plantedSeed.id_plant = selector.selectedPlant.id_plant;//Recoge los datos de la planta en PlantSelector
             plantedSeed.plant = selector.selectedPlant.plant;
             plantedSeed.time = selector.selectedPlant.time;
             plantedSeed.quantity = selector.selectedPlant.quantity;
             plantedSeed.sell = selector.selectedPlant.sell;
             plantedSeed.buy = selector.selectedPlant.buy;

             text.text = selector.selectedPlant.plant; //El texto del boton con el nombre

            
            Seeds tmp = GameObject.Find("Content").GetComponent<Seeds>();

            for(int i = 0; i < tmp.inventoryButtons.Count; i++) {

                if(tmp.inventoryButtons[i].GetComponent<InventoryPlant>().plant.plant == plantedSeed.plant)
                {
                    tmp.inventoryButtons[i].GetComponent<InventoryPlant>().plant.quantity--;
                    selector.selectedPlant.quantity--;
                  
                }
                  
            
            }


            isPlanted = true;
            timer = 0;//Reiniciamos temporizador
           
        }
        else if (isGrown)
        {
            
            isGrown = false;
            canPlant = true;
            text.text = " ";

            Seeds tmp = GameObject.Find("Content").GetComponent<Seeds>();
            for (int i = 0; i < tmp.inventoryButtons.Count; i++)
            {
                if (tmp.inventoryButtons[i].GetComponent<InventoryPlant>().plant.plant == plantedSeed.plant)
                {
                    tmp.inventoryButtons[i].GetComponent<InventoryPlant>().plant.quantity++;
                    selector.selectedPlant.quantity++;

                }
            }
            GameObject.Find("MoneyManager").GetComponent<MoneyManager>().AddMoney(plantedSeed.sell);
        }
    }

}
