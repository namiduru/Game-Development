using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassScene : MonoBehaviour
{

    private float timer;
    private float timer2;
    private bool isOpening;

    public CanvasGroup fader;
    public CanvasGroup fader2;

    public string Scene;

    void Start()
    {
        isOpening = true;
        timer = Time.time + 3f;
        timer2 = Time.time;
    }

    void FixedUpdate()
    {
        if (timer >= Time.time)
        {
            if (isOpening)
            {
                fader.alpha -= 0.01f;

                if (fader.alpha == 0f)
                {
                    isOpening = false;
                    timer2 = Time.time + 3f;
                }
            }

            else if (!isOpening && timer2 >= Time.time && timer > (Time.time + 2f))
            {
                fader2.alpha += 0.01f;
            }
        }

        else
        {
            timer = Time.time + 4f;

            if (!isOpening && timer2 <= Time.time)
                Application.LoadLevel(Scene);
        }
    }
}
