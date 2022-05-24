using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator leverAnimator;
    public Animator gateAnimator;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            leverAnimator.SetTrigger("Push");
        }
    }

    public void OpenGate()
    {
        gateAnimator.SetTrigger("Open");
    }
}
