using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedGameButtons : MonoBehaviour
{
    public DataBase database;
    public GameObject savedGameButtonPrefab;
    public GameObject content;
    public List<SavedGame> savedGames = new List<SavedGame>();


    void Awake()
    {
        database = GameObject.Find("DataBase").GetComponent<DataBase>();
        StartCoroutine(Waiter());
    }
    public IEnumerator Waiter()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        savedGames = database.GetSavedGames(); //Lista de partidas guardadas
        GenerateButtons();
    }

    public void GenerateButtons()
    {
        for(int i = 0; savedGames.Count > i; i++)
        {
            GameObject tmp = Instantiate(savedGameButtonPrefab);
            tmp.transform.SetParent(content.transform);
            tmp.GetComponent<SavedGamesLogic>().savedGame = savedGames[i];
            tmp.GetComponent<SavedGamesLogic>().database = database;
            tmp.GetComponent<SavedGamesLogic>().UpdateText();
        }
    }

    

}
