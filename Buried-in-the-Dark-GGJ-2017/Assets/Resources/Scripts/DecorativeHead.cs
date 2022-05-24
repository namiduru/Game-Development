using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeHead : MonoBehaviour
{
    public Animator myAnimator;
    public GameObject eyeCage;
    public Transform eye;
    public Transform player;

    private bool eyeStart = false;

    void FixedUpdate()
    {
        if (eyeStart)
            eye.position = Vector3.MoveTowards(eye.position, player.position, 0.005f);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            myAnimator.SetTrigger("Active");
            eyeCage.SetActive(true);
            eyeStart = true;
        }
    }
}
