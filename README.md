<h1 align="center"> Tic-Tac-Toe-Plus-v1 </h1>
<p align="center"> Tic-Tac-Toe with a twist. </p>
<p align="center"> Change the board size and win condition. </p>
<p align="center"> Alter to your liking. </p>

<h1 align="center">
 <a href="https://menglinmaker.itch.io/tic-tac-toe-plus">
 :point_right: Play Here :point_left:
 </a>
</h1>

<div align="center">
  <a href="https://menglinmaker.itch.io/tic-tac-toe-plus">
    <img width="500px" src="https://user-images.githubusercontent.com/39476147/198872292-b4db6411-2a7a-4c2b-bfc2-c47d2f63540b.gif">
  </a>
</div>

<div flex align="center">
<img alt="GitHub" src="https://img.shields.io/github/license/menglinmaker/Tic-Tac-Toe-Plus-v1?style=flat-square">
<img src="https://img.shields.io/github/languages/code-size/menglinmaker/Tic-Tac-Toe-Plus-v1?style=flat-square">
<img src="https://img.shields.io/website?down_color=red&down_message=offline&up_color=success&up_message=online&url=https://menglinmaker.itch.io/tic-tac-toe-plus/&style=flat-square">
</div>




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
