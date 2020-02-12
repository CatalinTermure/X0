#include <iostream>

using namespace std;

const char player = 'X';
const char ai = '0';
const char gol = '_';

char PG[3][3] = {0};

struct Move {
    int x, y;
};

bool NoMovesLeft(char Playground[3][3]) {
    for(int i = 0; i < 3; i++) {
        for(int j = 0; j < 3; j++) {
            if(Playground[i][j] != gol) {
                return false;
            }
        }
    }
    return true;
}

int evaluate(char Playground[3][3]) {
    for(int i = 0; i < 3; i++) {
        if(Playground[i][0] != gol && Playground[i][0] == Playground[i][1] && Playground[i][1] == Playground[i][2]) {
            if(Playground[i][0] == player) {
                return -100;
            }
            else {
                return 100;
            }
        }
        if(Playground[0][i] != gol && Playground[0][i] == Playground[1][i] && Playground[1][i] == Playground[2][i]) {
            if(Playground[i][0] == player) {
                return -100;
            }
            else {
                return 100;
            }
        }
    }
    if (Playground[0][0] != gol && Playground[0][0] == Playground[1][1] && Playground[1][1] == Playground[2][2]) {
        if(Playground[0][0] == player) {
            return -100;
        }
        else {
            return 100;
        }
    }
    if (Playground[1][1] != gol && Playground[0][2] == Playground[1][1] && Playground[1][1] == Playground[2][0]) {
        if(Playground[1][1] == player) {
            return -100;
        }
        else {
            return 100;
        }
    }
    return 0;
}

int minimax(char PG[3][3], int depth = 0, bool isMax = false) {
    int score = evaluate(PG);
    if(score == -100 || score == 100) {
        return score;
    }

    if(NoMovesLeft(PG)) {
        return 0;
    }

    if(isMax) {
        int maxval = -9999;

        for(int i = 0; i < 3; i++) {
            for(int j = 0; j < 3; j++) {
                if(PG[i][j] == gol) {
                    PG[i][j] = ai;

                    int rez = minimax(PG, depth + 1, !isMax);

                    if(rez > maxval) {
                        maxval = rez;
                    }

                    PG[i][j] = gol;
                }
            }
        }
        return maxval - depth;
    }
    else {
        int minval = 9999;

        for(int i = 0; i < 3; i++) {
            for(int j = 0; j < 3; j++) {
                if(PG[i][j] == gol) {
                    PG[i][j] = player;

                    int rez = minimax(PG, depth + 1, !isMax);

                    if(rez < minval) {
                        minval = rez;
                    }

                    PG[i][j] = gol;
                }
            }
        }
        return minval + depth;
    }
}

Move FindBestMove(char PG[3][3]) {
    int best = -99999;
    Move bestMove;

    for(int i = 0; i < 3; i++) {
        for(int j = 0; j < 3; j++) {
            if(PG[i][j] == gol) {
                PG[i][j] = ai;
                int val = minimax(PG);
                PG[i][j] = gol;

                if(val > best) {
                    best = val;
                    bestMove.x = i;
                    bestMove.y = j;
                }
            }
        }
    }

    return bestMove;
}


int main() {
    for(int i = 0; i < 3; i++) {
        for(int j = 0; j < 3; j++) {
            cin >> PG[i][j];
        }
    }
    Move m = FindBestMove(PG);
    PG[m.x][m.y] = ai;
    for(int i = 0; i < 3; i++) {
        for(int j = 0; j < 3; j++) {
            cout << PG[i][j] << ' ';
        }
        cout << '\n';
    }
    return 0;
}
