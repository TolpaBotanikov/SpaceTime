using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTriger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        Game.S.fixBtn.interactable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        Game.S.fixBtn.interactable = false;
    }
}
