using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//librer�as para usar la BD
using System.Data;
using Mono.Data.Sqlite;
using static Unity.Burst.Intrinsics.X86.Avx;

public class DataBase : MonoBehaviour
{
    IDbConnection conn;//declaramos la conexi�n con la base de datos

    string dbName = "entifarm.db";//hacemos un string para llamar al archivo con la base de datos

    public List<Plant> plantsListDB = new List<Plant>();//Declaramos una lista de plantas

    void Start()
    {
        conn = new SqliteConnection(string.Format("URI=file:{0}", dbName));
        conn.Open();//Abrimos la conexi�n con la base de datos

        plantsListDB = getPlants();//Esta devuelve una lista de plantas con todos sus datos de la DB
    }

    public List<Plant> getPlants()
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
}