using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class PlantSelector : MonoBehaviour
{
    //Variables en el inspector
    public Plant selectedPlant;//Aqu� se guarda la info de la planta seleccionada
    public InventoryPlant selectedSeed;//Aqu� especificamos que casilla del inventario hemos clicado

    public float gameTime = 0;//timer que guarda el tiempo de juego

    private void Update()
    {
        gameTime += Time.deltaTime;
    }


}
