# Tic-Tac-Toe-Plus-v1
Tic-Tac-Toe with a twist
Change the board size and win condition.
Alter to your liking.

## [Play Here](https://menglinmaker.itch.io/tic-tac-toe-plus)

## Algorithm Overview
The algorithm is designed to take advantage of certain constraints.

**Assumptions/constraints:**
* Winning condition only happens after a token is placed down.
* Only horizontal, vertical and diagonals need to be check for winning condition
* Player win condition ignores position of other players.

**Variables:**
* p: number of players
* w: board width
* h: board height
* n: number of consecutive tokens to win

**Algorithm**
1. Setup boolean playerLocationMatrix of size p*w*h. Initialised to false.
2. For each token placed, update playerLocationMatrix and check win condition for that player.
3. Collect location data from horizontal, vertical and diagonal ray of size n or under.
4. From each ray, count number of consecutive tokens.
5. If number of consecutive tokens > n, then the player wins.
