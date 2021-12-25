using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game S;

    public GameObject[] games;
    public bool isMinigameEnabled = false;
    public GameObject canvas;
    public Button fixBtn;
    public List<GameTriger> gamePoints = new List<GameTriger>();
    public int breakdownCnt;
    public float breakingDelay;
    public GameTriger currTriger;
    [SerializeField]
    private int hp;
    public Text hpText;
    public Text gameOverText;

    public int Hp 
    {
        get => hp;
        set
        {
            hp = value;
            hpText.text = hp.ToString();
            if (hp <= 0)
            {
                gameOverText.gameObject.SetActive(true);
                isMinigameEnabled = true;
            }
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("GenerationTest");
        SceneManager.UnloadSceneAsync(scene);
    }

    private void Awake()
    {
        S = this;
    }



    private IEnumerator BreakModules()
    {
        while (breakdownCnt > 0)
        {
            List<GameTriger> unbrokenModules = gamePoints.Where(g => g.Broken = false).ToList();
            unbrokenModules[Random.Range(0, gamePoints.Count)].Broken = true;
            yield return new WaitForSeconds(breakingDelay);
        }
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
