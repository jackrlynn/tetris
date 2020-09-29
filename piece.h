//
// Created by Jack Lynn on 9/29/20.
//

#ifndef TETRIS_PIECE_H
#define TETRIS_PIECE_H

#include <iostream>
#include <vector>
using namespace std;

class Piece {
public:
    explicit Piece(int ID = 0);
    void setID(int ID);
    int getID() const;
    vector<vector<int>> getVector() const;
    void rotateClock();
    void rotateCounter();
private:
    int m_ID;
    vector<vector<int>> m_vect;
    void setVector(int ID);
};


#endif //TETRIS_PIECE_H
