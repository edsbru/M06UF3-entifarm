using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct Plant
{
    public int id_plant;
    public string plant;
    public float time;
    public int quantity;
    public int sell;
    public int buy;
}


public class InventoryPlant : MonoBehaviour
{
 
    public Plant plant;
    public PlantSelector selector;
    
    

  
    private void Start()
    {

        selector = GameObject.Find("PlantSelector").GetComponent<PlantSelector>(); //Buscar en la escena el objeto PlantSelector
       
    }

    public void setSelectedPlant()
    {
        selector.selectedPlant = plant;
    }


}
