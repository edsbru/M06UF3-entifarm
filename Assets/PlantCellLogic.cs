using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCellLogic : MonoBehaviour
{
    public int fieldPosX;
    public int fieldPosY;
    public void onPlantCellClick()
    {
        Debug.Log(fieldPosX);
        Debug.Log(fieldPosY);
    }

}
