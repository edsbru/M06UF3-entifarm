using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantCellLogic : MonoBehaviour
{
    public int fieldPosX;//Posición X relativa al field
    public int fieldPosY;//Posición Y relativa al field
    public PlantSelector selector;
    private TextMeshProUGUI text; //Texto del boton
    public Plant Actualplant;

    private void Start()
    {
        selector = GameObject.Find("PlantSelector").GetComponent<PlantSelector>();
        text = GetComponentInChildren<TextMeshProUGUI>();

    }


    public void onPlantCellClick()
    {
        Actualplant.id_plant = selector.selectedPlant.id_plant;
        Actualplant.plant = selector.selectedPlant.plant;
        Actualplant.time = selector.selectedPlant.time;
        Actualplant.quantity = selector.selectedPlant.quantity;
        Actualplant.sell = selector.selectedPlant.sell;
        Actualplant.buy = selector.selectedPlant.buy;

        text.text = selector.selectedPlant.plant; //El texto del boton con el nombre

    }

}
