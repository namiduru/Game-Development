using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fang : MonoBehaviour
{
    public Animator myAnimator;
    public float OpeningTime;
    public float ClosingTime;

    private bool isOpened;
    private float timer;

	// Use this for initialization
	void Start ()
    {
        isOpened = false;
        timer = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if (timer <= Time.time)
        {
            if (!isOpened)
            {
                myAnimator.SetTrigger("Open");
                setTimer(OpeningTime);    
            }

            else
            {
                myAnimator.SetTrigger("Close");
                setTimer(1f);
            }
            isOpened = !isOpened;
        }
	}

    protected void setTimer (float delay)
    {
        this.timer = Time.time + delay;
    }
}
