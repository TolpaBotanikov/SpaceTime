using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TubeGame : MiniGame
{
    public Text[] buttons;
    private int currNumber = 1;

    private void Start()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        foreach (Text button in buttons)
        {
            int number = numbers[Random.Range(0, numbers.Count)];
            numbers.Remove(number);
            button.text = number.ToString();
        }
    }

    public void ButtonClick(Text sender)
    {
        if (sender.text == currNumber.ToString())
        {
            Button button = sender.transform.parent.GetComponent<Button>();
            button.interactable = false;
            button.GetComponent<Image>().color = Color.green;
            if (currNumber == 10)
            {
                FinishGame(true);
            }
            else
            {
                currNumber++;
            }
        }
        else
        {
            FinishGame(false);
        }
    }
}
