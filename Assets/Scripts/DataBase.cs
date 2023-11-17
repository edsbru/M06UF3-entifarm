using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Data;
using Mono.Data.Sqlite;
using static Unity.Burst.Intrinsics.X86.Avx;

public class DataBase : MonoBehaviour
{
    IDbConnection conn;//abre conexión con la base de datos

    string dbName = "entifarm.db";//hacemos un string para llamar al archivo con la base de datos

    List<Plant> plantsList = new List<Plant>();

    void Start()
    {
        conn = new SqliteConnection(string.Format("URI=file:{0}", dbName));//Abrimos la conexión con la base de datos
        conn.Open();

        getPlants();
    }

    void Update()
    {
        
        
    }

    void getPlants()
    {
        IDbCommand cmd = conn.CreateCommand();//Nos permite mandar queries, hay que crear para cada función nueva

        cmd.CommandText = "SELECT * FROM plants";

        IDataReader reader = cmd.ExecuteReader();//nos permite iterar por la base de datos

        while (reader.Read())//al hacer reader.Read() le decimos al iterador que vaya a la siguiente casilla
        {
            Plant p = new Plant();

            p.id_plant = reader.GetInt32(0);//devuelve el id, los ids de las entradas va del 0 al x.
            p.plant = reader.GetString(1);
            p.time = reader.GetFloat(2);
            p.quantity = reader.GetInt32(3);
            p.sell = reader.GetFloat(4);
            p.buy = reader.GetFloat(5);


            plantsList.Add(p);
        }
    }
}