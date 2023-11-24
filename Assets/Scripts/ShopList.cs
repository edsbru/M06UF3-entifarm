using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopList : MonoBehaviour
{
    //voy a necesitar algo que mire cuantas entradas hay en la base de datos
    //probaremos a generar un botón por cada entrada que encuentre
    public DataBase dbList;

    void Start()
    {
        Debug.Log(dbList.plantsListDB.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
