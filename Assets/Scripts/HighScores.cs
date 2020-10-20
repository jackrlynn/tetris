using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour {

    public Text highScoreName1;
    public Text highScoreName2;
    public Text highScoreName3;
    public Text highScoreName4;
    public Text highScoreName5;

    public Text highScore1;
    public Text highScore2;
    public Text highScore3;
    public Text highScore4;
    public Text highScore5;

    //playerNameInput refers to a text box which is part of an InputField UI element
    public Text playerNameInput;
    string playerName;

    //this value represents which rank the Player's score attained
    int entryChanged;

    // Start is called before the first frame update
    void Start() {

        //PlayerPrefs.DeleteAll();

        entryChanged = PlayerPrefs.GetInt("High Score Entry");
         
        //initial setting of high score UI text
        highScore1.text = PlayerPrefs.GetInt("High Score1").ToString();
        highScore2.text = PlayerPrefs.GetInt("High Score2").ToString();
        highScore3.text = PlayerPrefs.GetInt("High Score3").ToString();
        highScore4.text = PlayerPrefs.GetInt("High Score4").ToString();
        highScore5.text = PlayerPrefs.GetInt("High Score5").ToString();

        highScoreName1.text = PlayerPrefs.GetString("High Score Name1");
        highScoreName2.text = PlayerPrefs.GetString("High Score Name2");
        highScoreName3.text = PlayerPrefs.GetString("High Score Name3");
        highScoreName4.text = PlayerPrefs.GetString("High Score Name4");
        highScoreName5.text = PlayerPrefs.GetString("High Score Name5");


        UpdateHighScoreNames();
    }

    void Update() {

        playerName = playerNameInput.text;
        UpdateHighScoreNames();
    }

    //checks the value of entryChanged and changes the value of the corresponding
    //high score name to playerName
    void UpdateHighScoreNames() {

        if (entryChanged == 1) {
            highScoreName1.text = playerName;
            PlayerPrefs.SetString("High Score Name1", playerName);
        }else if (entryChanged == 2) {
            highScoreName2.text = playerName;
            PlayerPrefs.SetString("High Score Name2", playerName);
        }else if (entryChanged == 3) {
            highScoreName3.text = playerName;
            PlayerPrefs.SetString("High Score Name3", playerName);
        }else if (entryChanged == 4) {
            highScoreName4.text = playerName;
            PlayerPrefs.SetString("High Score Name4", playerName);
        }else if (entryChanged == 5) {
            highScoreName5.text = playerName;
            PlayerPrefs.SetString("High Score Name5", playerName);
        }
    }

}
