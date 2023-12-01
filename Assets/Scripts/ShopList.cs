using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopList : MonoBehaviour
{
    //voy a necesitar algo que mire cuantas entradas hay en la base de datos
    //probaremos a generar un botón por cada entrada que encuentre
    public DataBase dbList;
    public GameObject sampleButton;
    public List<GameObject> shopButtons;

    void Start()
    {
        GenerateShopList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateShopList()
    {
        for (int i = 0; i < dbList.plantsListDB.Count; i++)
        {
            GameObject tmp = Instantiate(sampleButton);
            tmp.transform.SetParent(this.gameObject.transform);
            shopButtons.Add(tmp);
        }
    }
}
