using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Data;
using Mono.Data.Sqlite;

public class DataBase : MonoBehaviour
{
    IDbConnection conn;

    string dbName = "entifarm.db";
    void Start()
    {
        conn = new SqliteConnection(string.Format("URI=file:{0}", dbName));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
