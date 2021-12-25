using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class GameTriger : MonoBehaviour
{
    private bool broken;
    public Light lightPoint;

    public bool Broken 
    {
        get => broken;
        set
        {
            broken = value;
            if (broken)
                lightPoint.gameObject.SetActive(true);
            else
                lightPoint.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        Game.S.fixBtn.interactable = true;
        Game.S.currTriger = this;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        Game.S.fixBtn.interactable = false;
        Game.S.currTriger = null;
    }
}
