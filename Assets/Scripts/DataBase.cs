using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//librer�as para usar la BD
using System.Data;
using Mono.Data.Sqlite;
using static Unity.Burst.Intrinsics.X86.Avx;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//Structs de las tablas de la base de datos
public struct SavedGame
{
    public int id_savedgame;
    public float time;
    public int size;
    public decimal money;
    public DateTime saved;
    public int id_user;
}
public struct User
{
    public int id_user;
    public string user;
    public string password;
}
public struct SavedGameCell
{
    public int id_savedgame_cell;
    public int x;
    public int y;
    public float time;
    public int id_plant;
    public int id_savedgame;
}
public struct PlantUser
{
    public int id_plant_user;
    public int id_plant;
    public int id_user;
}


public class DataBase : MonoBehaviour
{
    IDbConnection conn;//declaramos la conexi�n con la base de datos

    string dbName = "entifarm.db";//hacemos un string para llamar al archivo con la base de datos

    private User user;

    public List<Plant> plantsListDB = new List<Plant>();//Declaramos una lista de plantas
    
    public List<ShopPlant> shopPlantsListDB = new List<ShopPlant>();//Declaramos lista de plantas de la tienda

    private PlantSelector plantSelector;

    private DrawField drawField;

    private MoneyManager moneyManager;

    void Start()
    {
        if (GameObject.Find("DataBase") != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(this);
        }

        conn = new SqliteConnection(string.Format("URI=file:{0}", dbName));
        conn.Open();//Abrimos la conexi�n con la base de datos

        plantsListDB = GetPlants();//Esta devuelve una lista de plantas con todos sus datos de la DB

        shopPlantsListDB = GetShopPlants();
    }

    public List<Plant> GetPlants()
    {
        List<Plant> plantsTMP = new List<Plant>();//creamos una lista de plantas temporal
        
        IDbCommand cmd = conn.CreateCommand();//"CreateCommand()"Nos permite mandar queries, hay que crear para cada funci�n nueva

        cmd.CommandText = "SELECT * FROM plants";//"CommandText"Mandamos la query

        IDataReader reader = cmd.ExecuteReader();//funci�n que funciona como un iterador de la BD, ahora apunta a null

        while (reader.Read())//al hacer reader.Read() le decimos al iterador que vaya a la siguiente casilla
                             //ESTO SE EJECUTAR� MIENTRAS HAYA ENTRADAS EN LA TABLA plants
        {
            Plant p = new Plant();//crea una planta

            p.id_plant = reader.GetInt32(0);//devuelve el id, los ids de las entradas va del 0 al x.
            p.plant = reader.GetString(1);
            p.time = reader.GetFloat(2);
            p.quantity = reader.GetInt32(3);
            p.sell = reader.GetInt32(4);
            p.buy = reader.GetInt32(5);

            plantsTMP.Add(p);//A�adimos la planta a la lista (ahora tiene todos los datos cogidos de DB)
        }

        return plantsTMP;//Retornamos la lista con todas las plantas y sus datos
    }


    public List<ShopPlant> GetShopPlants()
    {
        List<ShopPlant> shopPlantsTMP = new List<ShopPlant>();//creamos una lista de plantas temporal

        IDbCommand cmd = conn.CreateCommand();//"CreateCommand()"Nos permite mandar queries, hay que crear para cada funci�n nueva

        cmd.CommandText = "SELECT * FROM plants";//"CommandText"Mandamos la query

        IDataReader reader = cmd.ExecuteReader();//funci�n que funciona como un iterador de la BD, ahora apunta a null

        while (reader.Read())//al hacer reader.Read() le decimos al iterador que vaya a la siguiente casilla
                             //ESTO SE EJECUTAR� MIENTRAS HAYA ENTRADAS EN LA TABLA plants
        {
            ShopPlant sp = new ShopPlant();//crea una shopPlanta

            sp.growthTime = reader.GetFloat(2);//devuelve el id, los ids de las entradas va del 0 al x.
            sp.availableSeeds = reader.GetInt32(3);
            sp.moneyPerPlant = reader.GetFloat(4);
            sp.plantCost = reader.GetFloat(5);
            

            shopPlantsTMP.Add(sp);//A�adimos la planta a la lista (ahora tiene todos los datos cogidos de DB)
        }

        return shopPlantsTMP;//Retornamos la lista con todas las plantas y sus datos
    }

    public void SaveGame() //En esta funci�n seteamos todos los valores a enviar a la base de datos
    {
        plantSelector = GameObject.Find("PlantSelector").GetComponent<PlantSelector>();

        drawField = GameObject.Find("Field").GetComponent<DrawField>();

        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();


        SavedGame sg;

        sg.id_savedgame = 1;
        sg.time = plantSelector.gameTime;
        sg.size = drawField.sizeX;
        sg.money = moneyManager.currentMoney;
        sg.saved = DateTime.Now;
        sg.id_user = user.id_user; //TODO: Implementar logica users


    }

    public void CreateUserOnDatabase()
    {

        IDbCommand cmd = conn.CreateCommand();//"CreateCommand()"Nos permite mandar queries, hay que crear para cada funci�n nueva
        
        
        user.user = GameObject.Find("UsernameInputField").GetComponent<TMP_InputField>().text;
        user.password = GameObject.Find("PasswordInputField").GetComponent<TMP_InputField>().text;
        

        cmd.CommandText = $"INSERT INTO users (user, password) VALUES (\"{user.user}\", \"{user.password}\")";//Mandamos la query
        cmd.ExecuteNonQuery();

        SceneManager.LoadScene(1);
    }
}