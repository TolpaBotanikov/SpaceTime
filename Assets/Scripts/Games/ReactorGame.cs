using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorGame : MiniGame
{
    public Text[] buttons;
    private List<int> code = new List<int>();
    private bool canInput = false;
    [SerializeField]
    private float btnShowDelay = 0.25f;
    private int currIndex = 0;


    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            int r = -1;
            if (i != 0)
                do
                {
                    r = Random.Range(0, 9);
                } while (r == code[code.Count - 1]);
            else r = Random.Range(0, 9);
            code.Add(r);
        }
        StartCoroutine(ShowCode());
    }

    IEnumerator ShowCode()
    {
        for (int i = 0; i < code.Count; i++)
        {
            if (i != 0)
            {
                buttons[code[i - 1]].transform.parent.GetComponent<Image>().color = Color.white;
            }
            buttons[code[i]].transform.parent.GetComponent<Image>().color = Color.blue;
            yield return new WaitForSeconds(btnShowDelay);
        }
        buttons[code[code.Count - 1]].transform.parent.GetComponent<Image>().color = Color.white;
        canInput = true;
    }

    public void ButtonClick(Text sender)
    {
        if (!canInput) return;
        if (sender.text == (code[currIndex] + 1).ToString())
        {
            Button button = sender.transform.parent.GetComponent<Button>();
            currIndex++;
            if(currIndex == code.Count)
                FinishGame(true);
        }
        else
        {
            FinishGame(false);
        }
    }
}
