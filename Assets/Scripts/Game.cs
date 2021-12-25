using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game S;

    public GameObject[] games;
    public bool isMinigameEnabled = false;
    public GameObject canvas;
    public Button fixBtn;
    public List<GameTriger> gamePoints = new List<GameTriger>();

    private void Awake()
    {
        S = this;
    }
    public void StartGame(GameObject game)
    {
        Instantiate(game, canvas.transform);
        isMinigameEnabled = true;
    }

    public void StartGame()
    {
        Instantiate(games[Random.Range(0, games.Length)], canvas.transform);
        isMinigameEnabled = true;
    }

    public void FixButonClick()
    {
        StartGame();
    }
}
