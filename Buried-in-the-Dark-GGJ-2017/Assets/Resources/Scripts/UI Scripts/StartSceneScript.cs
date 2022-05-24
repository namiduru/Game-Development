using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneScript : MonoBehaviour
{
    public GameObject faderObject;
    public CanvasGroup fader;
    private float timer;

    void Start()
    {
        timer = Time.time + 3f;
    }

    void Update()
    {
        if (timer >= Time.time)
        {
            fader.alpha -= 0.01f;

            if (fader.alpha == 0f)
                faderObject.SetActive(false);
        }
    }

    public void PlayButtonFunction(string sceneName)
    {
        if (sceneName == "Quit")
            Application.Quit();
        else
            Application.LoadLevel(sceneName);
    }
}
