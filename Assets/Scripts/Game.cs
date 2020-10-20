using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public static int gridWidth = 10;
    public static int gridHeight = 20;

    //setting the dimensions of the grid
    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    //scoring values
    public int scoreOneLine = 40;
    public int scoreTwoLine = 100;
    public int scoreThreeLine = 300;
    public int scoreFourLine = 1200; 

    private int numberOfRowsThisTurn = 0;

    public Text hud_score;
    public int currentScore = 0;

    // Start is called before the first frame update
    void Start() {

        SpawnNextTetromino();
    }

    void Update() {

        UpdateScore();

        UpdateUI(); 
    }

    //updates scoring UI text
    public void UpdateUI() {

        hud_score.text = currentScore.ToString(); 
    }

    //updates currentScore based on value of numberOfRowsThisTurn
    public void UpdateScore() {

        if (numberOfRowsThisTurn > 0) {
            if (numberOfRowsThisTurn == 1) {
                ClearedOneLine(); 
            }else if (numberOfRowsThisTurn == 2) {
                ClearedTwoLines();
            }else if (numberOfRowsThisTurn == 3) {
                ClearedThreeLines(); 
            }else if (numberOfRowsThisTurn == 4) {
                ClearedFourLines();
            }

            numberOfRowsThisTurn = 0;
        }
    }

    //function for if one line has been cleared this turn
    public void ClearedOneLine() {

        currentScore += scoreOneLine; 
    }

    //function for if two lines has been cleared this turn
    public void ClearedTwoLines() {

        currentScore += scoreTwoLine;
    }

    //function for if three lines has been cleared this turn
    public void ClearedThreeLines() {

        currentScore += scoreThreeLine;
    }

    //function for if four lines has been cleared this turn
    public void ClearedFourLines() {

        currentScore += scoreFourLine; 
    }

    //checks if any part of the tetromino in play is above the grid
    public bool CheckIsAboveGrid(Tetromino tetromino) {

        for (int x = 0; x < gridWidth; x++) {

            foreach (Transform mino in tetromino.transform) {
                
                Vector2 pos = Round(mino.position);
                if (pos.y > gridHeight - 1) {
                    return true; 
                }
            }
        }

        return false; 
    }

    //checks if there is a full row of minos at a given y value
    public bool IsFullRowAt(int y) {

        for (int x = 0; x < gridWidth; x++) {

            if (grid[x, y] == null) {
                return false;
            }
        }

        numberOfRowsThisTurn++;

        return true;
    }

    //deletes all minos at given y value 
    public void DeleteMinoAt(int y) {

        for (int x = 0; x < gridWidth; x++) {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    //moves minos down 1 unit if possible at a given y value
    public void MoveRowDown(int y) {

        for (int x = 0; x < gridWidth; x++) {

            if (grid[x, y] != null) {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0); 
            }
        }
    }

    //using MoveRowDown method to move minos down from a given y value to 
    //the grid height
    public void MoveAllRowsDown(int y) {

        for (int i = y; i < gridHeight; i++) {
            MoveRowDown(i);
        }
    }

    //if a row is full, delete that row and move minos down
    public void DeleteRow() {

        for (int y = 0; y < gridHeight; y++) {

            if (IsFullRowAt(y)) {
                DeleteMinoAt(y);
                MoveAllRowsDown(y + 1);
                y--; 
            }
        }
    }

    //rounds the Vector2 variable passed to the nearest integer
    public Vector2 Round(Vector2 pos) {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y)); 
    }

    //checks the position of the passed tetromino variable and updates
    //grid accordingly
    public void UpdateGrid(Tetromino tetronimo) {

        for (int y = 0; y < gridHeight; y++) {
            for (int x = 0; x < gridWidth; x++) {

                if (grid[x, y] != null) {
                    if (grid[x, y].parent == tetronimo.transform) {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform mino in tetronimo.transform) {
            Vector2 pos = Round(mino.position);

            if (pos.y < gridHeight) {
                grid[(int)pos.x, (int)pos.y] = mino;  
            }
        }
    }

    //checks if mino is above the grid. 
    //if true returns null
    //if false returns x and y value of mino
    public Transform GetTransformAtGridPosition(Vector2 pos) {

        if (pos.y > gridHeight - 1) {
            return null;
        }else {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    //spawns next tetromino and increases the score for placing a tetromino
    public void SpawnNextTetromino() {

        Instantiate(Resources.Load(GetNewTetromino(), typeof(GameObject)), new Vector2(5, 20), Quaternion.identity);
        currentScore += 5; 
    }

    //randomly gets the name of a tetromino type
    string GetNewTetromino() {

        int randomTetromino = Random.Range(0, 7);
        string prefabPath = "Prefabs/";
        string[] tetrominos = { "Tetromino_J", "Tetromino_L", "Tetromino_Long",
                                "Tetromino_S", "Tetromino_Square", "Tetromino_T",
                                "Tetromino_Z"};

        return prefabPath + tetrominos[randomTetromino];
    }

    //checks if a new high score was attained
    //if true load HighScore scene
    //if false load GameOver scene
    public void GameOver() {

        if (isNewHighScore(currentScore)) {
            SceneManager.LoadScene("HighScore");
        }else {
            SceneManager.LoadScene("GameOver");
        }
    }

    //checks if the final score the player got is a new high score
    //if true returns true and adds the player score to PlayerPrefs 
    //and adjust the PlayerPrefs entries accordingly
    //if false returns false
    public bool isNewHighScore(int score) {

        string highScoreEntry;
        int i = 1;
        int entryChanged = -1;

        while (i <= 5 && entryChanged == -1) {
            highScoreEntry = "High Score" + i.ToString(); 

            //if player score is higher than a high score, shift the corresponding 
            //high scores down bye one and input player score into the ranking
            if (score > PlayerPrefs.GetInt(highScoreEntry)) {

                for (int j = 5; j > i; j--) {
                    int tempScore = PlayerPrefs.GetInt("High Score" + (j - 1));
                    PlayerPrefs.SetInt("High Score" + j, tempScore);

                    string tempName = PlayerPrefs.GetString("High Score Name" + (j - 1));
                    PlayerPrefs.SetString("High Score Name" + j, tempName);
                }

                PlayerPrefs.SetInt(highScoreEntry, score);
                entryChanged = i;
                PlayerPrefs.SetInt("High Score Entry", entryChanged);
            }

            i++;
        }

        return entryChanged != -1; 
    }

    /*
    int GetScore(int[] cleanedLines) {

        int scoreSum = 0;
        int level = 0;
        int[] scoreTable = { 0, 40, 100, 300, 1200 };

        foreach (int i in cleanedLines) {
            scoreSum += (level/10 + 1) * (scoreTable[i]);
            level += i; 
        }

        return scoreSum; 
    }
    */
}
