using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndQuit : MonoBehaviour
{
    public DataBase dataBase;

    private void Start()
    {
        dataBase = GameObject.Find("DataBase").GetComponent<DataBase>();

    }

    public void SaveAndChangeToTitle()
    {
        dataBase.SaveGame();

        SceneManager.LoadScene(0);
    }
}
