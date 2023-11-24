using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawField : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;//Ponemos el prefab de las cell
    [SerializeField] private int defaultX = -350;//Posici�n X por defecto, donde se instancia el primer bot�n
    [SerializeField] private int defaultY = 350;//Posici�n Y por defecto, donde se instancia el primer bot�n
    [SerializeField] private int xOffset = 260;//Separaci�n X entre casillas
    [SerializeField] private int yOffset = 210;//Separaci�n Y entre casillas

    private int sizeX = 4;//Tama�o X de la matriz de casillas
    private int sizeY = 4;//Tama�o Y de la matriz de casillas
    private int x;//variable usada para ir sumando la separaci�n de las casillas en el eje X
    private int y;//variable usada para ir sumando la separaci�n de las casillas en el eje Y

    void Start()
    {
        x = defaultX;//Pongo valor a la var x
        y = defaultY;//Pongo valor a la var y

        for (int i = 0; i < sizeY; i++) //Bucle para hacer las columnas, se repite "sizeY" veces
        {
            for (int j = 0; j < sizeX; j++) //Bucle para hacer las filas, se repite "sizeX" veces
            {
                GameObject tmp = Instantiate(cellPrefab);//Declaro un GO temporal para instanciar una celda
                tmp.transform.SetParent(this.transform);//Especif�co que el transform de referencia es el de "field" (Como el script ya est� en Field pongo "this")
                tmp.transform.localPosition = new Vector2(x,y);//Cambio el transform de acuerdo a los valores de X y Y
                tmp.GetComponent<PlantCellLogic>().fieldPosX = j;//Para poder pasar por consola la pos de la casilla saco el valor de j cuando se instancia cada bot�n/casilla
                tmp.GetComponent<PlantCellLogic>().fieldPosY = i;//Para poder pasar por consola la pos de la casilla saco el valor de i cuando se instancia cada bot�n/casilla
                x += xOffset;//Aplicamos la separaci�n de X
            }

            y -= yOffset;//Aplicamos la separaci�n de Y
            x = defaultX;//Reseteamos el valor de X para poner la siguiente fila
        }
    }
}
