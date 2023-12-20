using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//librerías para usar la BD
using System.Data;
using Mono.Data.Sqlite;
using static Unity.Burst.Intrinsics.X86.Avx;
using System;
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
    IDbConnection conn;//declaramos la conexión con la base de datos

    string dbName = "entifarm.db";//hacemos un string para llamar al archivo con la base de datos

    private User user;

    public List<Plant> plantsListDB = new List<Plant>();//Declaramos una lista de plantas
    
    private PlantSelector plantSelector;

    private DrawField drawField;

    private MoneyManager moneyManager;

    void Start()
    {
        conn = new SqliteConnection(string.Format("URI=file:{0}", dbName));
        conn.Open();//Abrimos la conexión con la base de datos

        plantsListDB = GetPlants();//Esta devuelve una lista de plantas con todos sus datos de la DB

        plantSelector = GameObject.Find("PlantSelector").GetComponent<PlantSelector>();

        drawField = GameObject.Find("Field").GetComponent<DrawField>();

        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
    }

    public List<Plant> GetPlants()
    {
        List<Plant> plantsTMP = new List<Plant>();//creamos una lista de plantas temporal
        
        IDbCommand cmd = conn.CreateCommand();//"CreateCommand()"Nos permite mandar queries, hay que crear para cada función nueva

        cmd.CommandText = "SELECT * FROM plants";//"CommandText"Mandamos la query

        IDataReader reader = cmd.ExecuteReader();//función que funciona como un iterador de la BD, ahora apunta a null

        while (reader.Read())//al hacer reader.Read() le decimos al iterador que vaya a la siguiente casilla
                             //ESTO SE EJECUTARÁ MIENTRAS HAYA ENTRADAS EN LA TABLA plants
        {
            Plant p = new Plant();//crea una planta

            p.id_plant = reader.GetInt32(0);//devuelve el id, los ids de las entradas va del 0 al x.
            p.plant = reader.GetString(1);
            p.time = reader.GetFloat(2);
            p.quantity = reader.GetInt32(3);
            p.sell = reader.GetInt32(4);
            p.buy = reader.GetInt32(5);

            plantsTMP.Add(p);//Añadimos la planta a la lista (ahora tiene todos los datos cogidos de DB)
        }

        return plantsTMP;//Retornamos la lista con todas las plantas y sus datos
    }

    public void SaveGame() //En esta función seteamos todos los valores a enviar a la base de datos
    {
        SavedGame sg;

        //sg.id_savedgame = ;
        sg.time = plantSelector.gameTime;
        sg.size = drawField.sizeX;
        sg.money = moneyManager.currentMoney;
        sg.saved = DateTime.Now;
        sg.id_user = user.id_user; //TODO: Implementar logica users


    }
}