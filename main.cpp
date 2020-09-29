#include <iostream>
#include <vector>
#include "piece.h"

using namespace std;


int main() {

    Piece piece{7};
    for (int i = 0; i < piece.getVector().size(); i++) {
        vector<int> vect = piece.getVector().at(i);
        for (int j = 0; j < vect.size(); j++) {
            cout << vect.at(j) << " ";
        }
        cout << endl;
    }

    cout << "Counter-clockwise rotation: " << endl;
    piece.rotateCounter();
    for (int i = 0; i < piece.getVector().size(); i++) {
        vector<int> vect = piece.getVector().at(i);
        for (int j = 0; j < vect.size(); j++) {
            cout << vect.at(j) << " ";
        }
        cout << endl;
    }

    cout << "Clockwise rotation: " << endl;
    piece.rotateClock();
    for (int i = 0; i < piece.getVector().size(); i++) {
        vector<int> vect = piece.getVector().at(i);
        for (int j = 0; j < vect.size(); j++) {
            cout << vect.at(j) << " ";
        }
        cout << endl;
    }


    return 0;
}
