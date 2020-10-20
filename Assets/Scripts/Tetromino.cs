using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour {

    float fall = 0;
    //maximum time between each downward movement is set to 1 second
    public float fallSpeed = 1;
    //play area is a 10x20 grid with (0,0) origin in bottom left
    int gridWidth = 10;

    public bool allowRotation = true;
    public bool limitRotation = false; 

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        CheckUserInput();
    }

    void CheckUserInput() {

        //checks for input to move tetromino in specified direction
        //up arrow rotates the object
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.position += new Vector3(-1, 0);

            if (CheckIsValidPosition()) {
                FindObjectOfType<Game>().UpdateGrid(this);
            }else {
                transform.position += new Vector3(1, 0);
            }
        }else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position += new Vector3(1, 0);

            if (CheckIsValidPosition()) {
                FindObjectOfType<Game>().UpdateGrid(this);
            }else {
                transform.position += new Vector3(-1, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            //types of rotating allowed depends on tetromino type
            if (allowRotation) {
                if (limitRotation) {
                    if (transform.rotation.eulerAngles.z >= 90) {
                        transform.Rotate(0, 0, -90);
                    }else {
                        transform.Rotate(0, 0, 90); 
                    }
                }else {
                    transform.Rotate(0, 0, 90);
                }

                if (CheckIsValidPosition()) {
                    FindObjectOfType<Game>().UpdateGrid(this);
                }
                else {
                    transform.Rotate(0, 0, -90);
                }
            }

        }else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed) {
            transform.position += new Vector3(0, -1);

            //we can use the else condition to check if the tetromino has reached its
            //final position
            if (CheckIsValidPosition()) {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else {
                transform.position += new Vector3(0, 1);

                FindObjectOfType<Game>().DeleteRow();

                //if the tetromino's final position is at the top -> game over 
                if (FindObjectOfType<Game>().CheckIsAboveGrid(this)) {
                    FindObjectOfType<Game>().GameOver(); 
                }

                enabled = false;
                FindObjectOfType<Game>().SpawnNextTetromino();
            }
            fall = Time.time; 
        }
    }

    //checks if tetromino is in bounds and not occupying another tetromino's position
    bool CheckIsValidPosition() {

        foreach (Transform mino in transform) {
            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);
            if ((int)pos.x < 0 || (int)pos.x >= gridWidth || (int)pos.y < 0) {
                return false;
            }

            if (FindObjectOfType<Game>().GetTransformAtGridPosition(pos) != null 
                && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform) {
                return false; 
            }
        }

        return true;
    }
}
