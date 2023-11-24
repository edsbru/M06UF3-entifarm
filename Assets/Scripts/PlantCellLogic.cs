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
    public PlantSelector selector;//Acceso al selector de plantas
    private TextMeshProUGUI text; //Texto del botón
    public Plant plantedSeed;//Variable que recoje los datos de la planta que hay en la celda

    public float timer = 0;//temporizador para el crecimiento de las plantas

    //Estados de la cell
    private bool isPlanted = false;//bool para saber si hay una planta en la casilla
    private bool isGrown = false;//bool para saber si la planta de la casilla ha crecido
    private bool canPlant = true;//bool para indicar si se puede plantar una semilla

    private void Start()
    {
        selector = GameObject.Find("PlantSelector").GetComponent<PlantSelector>();//Recogemos los datos de PlantSelector en una variable
        text = GetComponentInChildren<TextMeshProUGUI>();//Hacemos que esta variable acceda al componente de texto TextMeshPro del botón
    }

    private void Update()
    {
        if (isPlanted == true)//si hay algo en la casilla
        {
            timer += Time.deltaTime;//Empieza a sumar tiempo al Timer

            if(timer >= plantedSeed.time)//Si el timer LLEGA O SE PASA del numero de la variable 'time' de la planta plantada
            {
                text.text = "Collect";//Cambia el texto de la casilla a "Collect"
                isGrown = true;//ponemos el bool de isGrown a TRUE para poder recogerla después
                isPlanted = false;//ponemos el bool de isPlanted a FALSE para poder plantar otra semilla
            }
            else if (timer >= plantedSeed.time / 2)//Si el timer se pasa o es igual a la mitad del tiempo de la variable 'time' de la planta plantada
            {
                text.text = "Growing";//Cambiamos el texto del botón a "Growing"
            }
        }
    }

    public void onPlantCellClick()//al pulsar celda de la planta
    {
        if (canPlant == true && selector.selectedPlant.quantity > 0) { //Si la casilla está VACÍA y QUEDAN SEMILLAS para plantar
            
            canPlant = false;//Ponemos canPlant a false
            plantedSeed.id_plant = selector.selectedPlant.id_plant;//Recogemos los datos de la planta escogida en PlantSelector
            plantedSeed.plant = selector.selectedPlant.plant;
            plantedSeed.time = selector.selectedPlant.time;
            plantedSeed.quantity = selector.selectedPlant.quantity;
            plantedSeed.sell = selector.selectedPlant.sell;
            plantedSeed.buy = selector.selectedPlant.buy;

            text.text = selector.selectedPlant.plant; //Cambiamos el texto del boton con el nombre de la planta plantada
            
            Seeds tmp = GameObject.Find("Content").GetComponent<Seeds>();

            for(int i = 0; i < tmp.inventoryButtons.Count; i++) {

                if(tmp.inventoryButtons[i].GetComponent<InventoryPlant>().plant.plant == plantedSeed.plant)
                {
                    tmp.inventoryButtons[i].GetComponent<InventoryPlant>().plant.quantity--;
                    selector.selectedPlant.quantity--;
                }
            }
            isPlanted = true;//con el bool decimos que ya hay algo plantado
            timer = 0;//Reiniciamos temporizador
        }
        else if (isGrown)//SI HAY UNA PLANTA CRECIDA
        {
            //VACIAMOS LA CASILLA
            isGrown = false;//Cambiamos isGrown a FALSE
            canPlant = true;//Cambiamos canPlant a TRUE
            text.text = " ";//Vaciamos el texto de la casilla

            Seeds tmp = GameObject.Find("Content").GetComponent<Seeds>();

            for (int i = 0; i < tmp.inventoryButtons.Count; i++)
            {
                if (tmp.inventoryButtons[i].GetComponent<InventoryPlant>().plant.plant == plantedSeed.plant)
                {
                    tmp.inventoryButtons[i].GetComponent<InventoryPlant>().plant.quantity++;
                    selector.selectedPlant.quantity++;
                }
            }
            //Añadimos al dinero la cantodad definida en la columna 'sell' de la planta recolectada
            GameObject.Find("MoneyManager").GetComponent<MoneyManager>().AddMoney(plantedSeed.sell);
        }
    }
}
