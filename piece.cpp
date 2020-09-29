//
// Created by Jack Lynn on 9/29/20.
//

#include "piece.h"

Piece::Piece(int ID) {
    m_ID = ID;
    if (m_ID == 1) {
        m_vect = {{0, 0, 0},{0, 0, 1},{1, 1, 1}};
    } else if (m_ID == 2) {
        m_vect = {{0, 0, 0},{1, 0, 0},{1, 1, 1}};
    } else if (m_ID == 3) {
        m_vect = {{0, 0, 0},{0, 1, 0},{1, 1, 1}};
    } else if (m_ID == 4) {
        m_vect = {{1, 1}, {1, 1}};
    } else if (m_ID == 5) {
        m_vect = {{0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}, {1, 1, 1, 1}};
    } else if (m_ID == 6) {
        m_vect = {{0, 0, 0}, {0, 1, 1}, {1, 1, 0}};
    } else if (m_ID == 7) {
        m_vect = {{0, 0, 0}, {1, 1, 0}, {0, 1, 1}};
    }
}
void Piece::setID(int ID) {
    m_ID = ID;
    setVector(ID);
}
void Piece::setVector(int ID) {
    if (ID == 1) {
        m_vect = {{0, 0, 0},{0, 0, 1},{1, 1, 1}};
    } else if (ID == 2) {
        m_vect = {{0, 0, 0},{1, 0, 0},{1, 1, 1}};
    } else if (ID == 3) {
        m_vect = {{0, 0, 0},{0, 1, 0},{1, 1, 1}};
    } else if (ID == 4) {
        m_vect = {{1, 1}, {1, 1}};
    } else if (ID == 5) {
        m_vect = {{0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}, {1, 1, 1, 1}};
    } else if (ID == 6) {
        m_vect = {{0, 0, 0}, {0, 1, 1}, {1, 1, 0}};
    } else if (ID == 7) {
        m_vect = {{0, 0, 0}, {1, 1, 0}, {0, 1, 1}};
    }
}
int Piece::getID() const {
    return m_ID;
}

vector<vector<int>> Piece::getVector() const {
    return m_vect;
}

void Piece::rotateClock() {
    vector<vector<int>> vect;
    vector<int> inVect;
    int length = getVector().size();
    for (int i = 0; i < length; i++) {
        inVect.push_back(0);
    }
    for (int i = 0; i < length; i++) {
        vect.push_back(inVect);
    }
    for (int i = 0; i < length; i++) {
        for (int j = 0; j < getVector().size(); j++) {
            int k = length - 1 - i;
            vect.at(j).at(k) = getVector().at(i).at(j);
        }
    }

    m_vect = vect;
}

void Piece::rotateCounter() {
    vector<vector<int>> vect;
    vector<int> inVect;
    int length = getVector().size();
    for (int i = 0; i < length; i++) {
        inVect.push_back(0);
    }
    for (int i = 0; i < length; i++) {
        vect.push_back(inVect);
    }
    for (int i = 0; i < length; i++) {
        for (int j = 0; j < getVector().size(); j++) {
            int l = length - 1 - j;
            vect.at(l).at(i) = getVector().at(i).at(j);
        }
    }
    m_vect = vect;
}