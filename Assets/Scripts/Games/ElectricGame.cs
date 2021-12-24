using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricGame : MiniGame
{
    public Button[] buttons;
    public GameObject[] arrows;
    public float rotationSpeed;
    private int currArrow = 0;

    private void Update()
    {
        Vector3 rotation = arrows[currArrow].transform.rotation.eulerAngles;
        rotation.z -= rotationSpeed * Time.deltaTime;
        arrows[currArrow].transform.rotation = Quaternion.Euler(rotation);
    }

    public void ButtonClick(int btnNumber)
    {
        if(arrows[currArrow].transform.rotation.eulerAngles.z < 15 ||
            arrows[currArrow].transform.rotation.eulerAngles.z > 345)
        {
            if (currArrow == arrows.Length - 1)
            {
                FinishGame(true);
            }
            else
            {
                buttons[currArrow].interactable = false;
                currArrow++;
                buttons[currArrow].interactable = true;
            }
        }
        else
        {
            print(Mathf.Abs(arrows[currArrow].transform.rotation.eulerAngles.z));
            FinishGame(false);
        }
    }
}