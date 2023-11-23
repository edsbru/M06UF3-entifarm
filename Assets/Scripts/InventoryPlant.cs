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
}


public class InventoryPlant : MonoBehaviour
{
    public enum Plants { Nabo, Patata, Col }
    public Plants plants;

    Plant plant;
    public PlantSelector selector;
    
    

  
    private void Start()
    {
        selector = GameObject.Find("PlantSelector").GetComponent<PlantSelector>(); //Buscar en la escena el objeto PlantSelector
       
        //TODO: Poner todas las variables de una planta
        switch (plants)
        {
            case Plants.Nabo:
                plant.plant = "Nabo";
                break;
            case Plants.Patata:
                plant.plant = "Patata";
                break;
            case Plants.Col:
                plant.plant = "Col";
                break;
        }
     

    }

    public void setSelectedPlant()
    {
        selector.selectedPlant = plant;
    }


}
