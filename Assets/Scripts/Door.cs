using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    public bool closed = true;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        if (!closed)
            anim.SetBool("character_nearby", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        anim.SetBool("character_nearby", false);
    }
}
