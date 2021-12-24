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
            code.Add(Random.Range(0, 9));
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
