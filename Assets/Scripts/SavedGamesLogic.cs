using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SavedGamesLogic : MonoBehaviour
{
    public SavedGame savedGame;
    public DataBase database;
    public TextMeshProUGUI username;
    public TextMeshProUGUI playedTime;
    public TextMeshProUGUI money;

    
    public void UpdateText()
    {
        username.text = database.GetUsernameByID(savedGame.id_user);
        playedTime.text = savedGame.time.ToString();
        money.text = savedGame.money.ToString();
    }

    public void OnSavedGameButtonClicked()
    {
        database.LoadGame(savedGame);
    }

}
