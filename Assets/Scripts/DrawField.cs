using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawField : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;//Ponemos el prefab de las cell
    [SerializeField] private int defaultX = -350;//Posición X por defecto, donde se instancia el primer botón
    [SerializeField] private int defaultY = 350;//Posición Y por defecto, donde se instancia el primer botón
    [SerializeField] private int xOffset = 260;//Separación X entre casillas
    [SerializeField] private int yOffset = 210;//Separación Y entre casillas

    private int sizeX = 4;//Tamaño X de la matriz de casillas
    private int sizeY = 4;//Tamaño Y de la matriz de casillas
    private int x;//variable usada para ir sumando la separación de las casillas en el eje X
    private int y;//variable usada para ir sumando la separación de las casillas en el eje Y

    void Start()
    {
        x = defaultX;//Pongo valor a la var x
        y = defaultY;//Pongo valor a la var y

        for (int i = 0; i < sizeY; i++) //Bucle para hacer las columnas, se repite "sizeY" veces
        {
            for (int j = 0; j < sizeX; j++) //Bucle para hacer las filas, se repite "sizeX" veces
            {
                GameObject tmp = Instantiate(cellPrefab);//Declaro un GO temporal para instanciar una celda
                tmp.transform.SetParent(this.transform);//Especifíco que el transform de referencia es el de "field" (Como el script ya está en Field pongo "this")
                tmp.transform.localPosition = new Vector2(x,y);//Cambio el transform de acuerdo a los valores de X y Y
                tmp.GetComponent<PlantCellLogic>().fieldPosX = j;//Para poder pasar por consola la pos de la casilla saco el valor de j cuando se instancia cada botón/casilla
                tmp.GetComponent<PlantCellLogic>().fieldPosY = i;//Para poder pasar por consola la pos de la casilla saco el valor de i cuando se instancia cada botón/casilla
                x += xOffset;//Aplicamos la separación de X
            }

            y -= yOffset;//Aplicamos la separación de Y
            x = defaultX;//Reseteamos el valor de X para poner la siguiente fila
        }
    }
}
