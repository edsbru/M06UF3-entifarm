using System.Collections.Generic;
using UnityEngine;

//librerías para usar la BD
using System.Data;
using Mono.Data.Sqlite;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using System.Globalization;

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


//Clase de la base de datos
public class DataBase : MonoBehaviour
{
    IDbConnection conn;//declaramos la conexión con la base de datos

    string dbName = "entifarm.db";//hacemos un string para llamar al archivo con la base de datos

    public User user;//Declaramos un usuario

    public List<Plant> plantsListDB = new List<Plant>();//Declaramos una lista de plantas
    
    public List<Plant> shopPlantsListDB = new List<Plant>();//Declaramos lista de plantas de la tienda

    private PlantSelector plantSelector;

    private DrawField drawField;

    private MoneyManager moneyManager;

    void Start()
    {
        CultureInfo culture = CultureInfo.InvariantCulture;
        System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

        if (GameObject.Find("DataBase") != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(this);
        }

        conn = new SqliteConnection(string.Format("URI=file:{0}", dbName));
        conn.Open();//Abrimos la conexión con la base de datos

        plantsListDB = GetPlants();//Esta devuelve una lista de plantas con todos sus datos de la DB
        Debug.Log(GetUsernameByID(1));
        shopPlantsListDB = GetShopPlants();
    }

    //Funciones DB
    public void GetUserId()
    {
        IDbCommand cmd = conn.CreateCommand();//"CreateCommand()"Nos permite mandar queries, hay que crear para cada función nueva

        cmd.CommandText = "SELECT * FROM users";//Mandamos la query
        
        IDataReader reader = cmd.ExecuteReader();//función que funciona como un iterador de la BD, ahora apunta a null


        while (reader.Read())
        {
            if (user.user == reader.GetString(1))
            {
                user.id_user = reader.GetInt32(0);
            }
        }

    }

    public List<Plant> GetPlants()
    {
        List<Plant> plantsTMP = new List<Plant>();//creamos una lista de plantas temporal
        
        IDbCommand cmd = conn.CreateCommand();//"CreateCommand()"Nos permite mandar queries, hay que crear para cada función nueva

        cmd.CommandText = "SELECT * FROM plants";//Mandamos la query

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

    public List<Plant> GetShopPlants()
    {
        List<Plant> shopPlantsTMP = new List<Plant>();//creamos una lista de plantas temporal

        IDbCommand cmd = conn.CreateCommand();//"CreateCommand()"Nos permite mandar queries, hay que crear para cada función nueva

        cmd.CommandText = "SELECT * FROM plants";//"CommandText"Mandamos la query

        IDataReader reader = cmd.ExecuteReader();//función que funciona como un iterador de la BD, ahora apunta a null

        while (reader.Read())//al hacer reader.Read() le decimos al iterador que vaya a la siguiente casilla
                             //ESTO SE EJECUTARÁ MIENTRAS HAYA ENTRADAS EN LA TABLA plants
        {
            Plant sp = new Plant();//crea una Planta

            sp.id_plant = reader.GetInt32(0);
            sp.time = reader.GetFloat(2);//devuelve el id, los ids de las entradas va del 0 al x.
            sp.quantity = reader.GetInt32(3);
            sp.sell = reader.GetFloat(4);
            sp.buy = reader.GetFloat(5);
            

            shopPlantsTMP.Add(sp);//Añadimos la planta a la lista (ahora tiene todos los datos cogidos de DB)
        }

        return shopPlantsTMP;//Retornamos la lista con todas las plantas y sus datos
    }

    public List<SavedGame> GetSavedGames()
    {
        List<SavedGame> savedGamesTMP = new List<SavedGame>();//creamos una lista de saved games

        IDbCommand cmd = conn.CreateCommand();//"CreateCommand()"Nos permite mandar queries, hay que crear para cada función nueva

        cmd.CommandText = "SELECT * FROM savedgames";//Mandamos la query

        IDataReader reader = cmd.ExecuteReader();//función que funciona como un iterador de la BD, ahora apunta a null

        while (reader.Read())//al hacer reader.Read() le decimos al iterador que vaya a la siguiente casilla
                             //ESTO SE EJECUTARÁ MIENTRAS HAYA ENTRADAS EN LA TABLA plants
        {
            SavedGame sg = new SavedGame();//crea un nuevo SavedGame

            sg.time = reader.GetFloat(1); //TODO: hacer correctamente la lectura de datos
            sg.money = reader.GetDecimal(3);
            sg.id_user = reader.GetInt32(5);

            savedGamesTMP.Add(sg);//Añadimos el saved game a la lista (ahora tiene todos los datos cogidos de DB)
        }

        return savedGamesTMP;//Retornamos la lista con todos saved games y sus datos
    }
    public void SaveGame() //En esta función seteamos todos los valores a enviar a la base de datos
    {
        plantSelector = GameObject.Find("PlantSelector").GetComponent<PlantSelector>();

        drawField = GameObject.Find("Field").GetComponent<DrawField>();

        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();        

        SavedGame sg;
        sg.time = (float)Math.Round(plantSelector.gameTime, 2);
        sg.size = drawField.sizeX;
        sg.money = moneyManager.currentMoney;
        sg.saved = DateTime.Now;
        sg.id_user = user.id_user;

        IDbCommand cmd = conn.CreateCommand();//"CreateCommand()"Nos permite mandar queries, hay que crear para cada función nueva

        cmd.CommandText = $"INSERT INTO savedgames (time, size, money, saved, id_user) VALUES ('{sg.time}', {sg.size}, {sg.money}, '{sg.saved}', {sg.id_user})";

        cmd.ExecuteNonQuery();
    }

    public void LoadGame(SavedGame _savedGame)
    {
        //TODO: Cargar la partida
        SceneManager.LoadScene(1);
    }

    public void CreateUserOnDatabase()
    {
        IDbCommand cmd = conn.CreateCommand();//"CreateCommand()"Nos permite mandar queries, hay que crear para cada función nueva
        
        user.user = GameObject.Find("UsernameInputField").GetComponent<TMP_InputField>().text;
        user.password = GameObject.Find("PasswordInputField").GetComponent<TMP_InputField>().text;
        
        cmd.CommandText = $"INSERT INTO users (user, password) VALUES (\"{user.user}\", \"{user.password}\")";//Mandamos la query
        cmd.ExecuteNonQuery();

        GetUserId();

        SceneManager.LoadScene(1);
    }

    public string GetUsernameByID(int _id)
    {
        string username;
        IDbCommand cmd = conn.CreateCommand();//"CreateCommand()"Nos permite mandar queries, hay que crear para cada función nueva
        cmd.CommandText = $"SELECT user FROM users WHERE id_user == {_id}";//Mandamos la query
        IDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            username = reader["user"].ToString();
        }
        else
        {
            username = "Rick Rolled";
        }
        return username;

        
    }



}