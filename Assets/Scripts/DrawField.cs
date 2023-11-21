using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawField : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int defaultX = -350;
    [SerializeField] private int defaultY = 350;
    [SerializeField] private int xOffset = 260;
    [SerializeField] private int yOffset = 210;



    private int sizeX = 4;
    private int sizeY = 4;
    private int x;
    private int y;

    void Start()
    {

        x = defaultX;
        y = defaultY;

        for (int i = 0; i < sizeY; i++) //Columnas
        {
            for (int j = 0; j < sizeX; j++) //Filas
            {
                GameObject tmp = Instantiate(cellPrefab);
                tmp.transform.SetParent(this.transform);
                tmp.transform.localPosition = new Vector2(x,y);
                tmp.GetComponent<PlantCellLogic>().fieldPosX = j ;
                tmp.GetComponent<PlantCellLogic>().fieldPosY = i ;
                x += xOffset;
            }

            y -= yOffset;
            x = defaultX;
        }
    }

    private void Update()
    {
        

    }

}
