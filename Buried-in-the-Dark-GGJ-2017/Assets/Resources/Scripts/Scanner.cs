using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public GameMaster gameMaster;
    public Transform submarine;
    public Light scanner;
    public Transform myTransform;
    public float duration;
    public float scannerSize = 0.6f;

    private float timer;
    private bool isScan;
    private float initialSpotAngle = 1f;

    void Start()
    {
        isScan = false;
    }

	void FixedUpdate ()
    {
        if (isScan)
            Scan();
        else if (submarine != null)
        {
            myTransform.position = new Vector3(submarine.position.x, submarine.position.y, (-5f));
        }
	}

    protected void Scan()
    {
        if (timer >= Time.time)
        {
            scanner.spotAngle += scannerSize;
        }

        else if (timer <= Time.time)
        {
            scanner.spotAngle = initialSpotAngle;
            gameMaster.DecreaseSonarWaves();
            isScan = false;
        }
    }

    protected void setTimer (float delay)
    {
        timer = Time.time + delay;
    }

    public void setScan (bool isScan)
    {
        this.isScan = isScan;
        setTimer(duration);
    }

    public bool getScan()
    {
        return this.isScan;
    }
}
