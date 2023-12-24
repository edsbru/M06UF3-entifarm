using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Plant
{
    public int id_plant;
    public string plant;
    public float time;
    public int quantity;
    public float sell;
    public float buy;
    public int invetoryQuantity;
} //Struct plant con todos los nombres y tipos de variable de las plantas en la base de datos

public class InventoryPlant : MonoBehaviour
{
    //Variables en el inspector
    public Plant plant; 
    public PlantSelector selector;
    
    private void Start()
    {
        //Buscar en la escena el objeto PlantSelector
        selector = GameObject.Find("PlantSelector").GetComponent<PlantSelector>();
       
    }

    public void setSelectedPlant()
    {
        if(plant.invetoryQuantity > 0) {//si el numero de semillas disponibles es mayor a cero
            selector.selectedPlant = plant;//pasamos la información de la planta al selector
            selector.selectedSeed = this;//pasamos el GO del botón del inventario
        }
    }
}