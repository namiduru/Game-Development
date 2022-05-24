using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    public Transform[] bubbles;
    public float yLimit;
    public Transform[] returnPoints;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Move_and_CheckBubbles();
	}

    protected void Move_and_CheckBubbles()
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            bubbles[i].position += new Vector3(0f, 0.002f, 0f);

            if (bubbles[i].position.y > yLimit)
            {
                int random = (int)(Random.Range(0f, returnPoints.Length));
                bubbles[i].position = returnPoints[random].position;
            }
        }
    }
}
