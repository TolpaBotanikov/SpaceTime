using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game S;

    public GameObject[] games;
    public bool isMinigameEnabled = false;
    public GameObject canvas;

    private void Awake()
    {
        S = this;
        isMinigameEnabled = true;
    }
    public void StartGame(GameObject game)
    {
        Instantiate(game, canvas.transform);
        isMinigameEnabled = true;
    }

    public void StartGame()
    {
        Instantiate(games[0], canvas.transform);
        isMinigameEnabled = true;
    }
}
