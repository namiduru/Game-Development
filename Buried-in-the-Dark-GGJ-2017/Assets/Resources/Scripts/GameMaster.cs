using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public MonsterFishScript MonsterFish;
    public Transform lava;
    public Player player;
    public Text[] textArray;

    public int sonarWaves = 1;
    public int fishFoods = 1;

    public GameObject faderObject;
    public CanvasGroup fader;
    private float timer;
    

	void Start ()
    {
        timer = Time.time + 3f;
        if (textArray.Length != 0)
            ArrangeTexts("sonar waves left: " + sonarWaves);

        if (MonsterFish != null)
        {
            StartCoroutine(ActivateMonsterFish(18f));
            if (textArray.Length != 0)
                ArrangeTexts("fish foods left: " + fishFoods);
        }

        ArrangeTexts("system is ready to go: ");
        if (Application.loadedLevelName.Equals("EndOfStoryScene"))
        {
            if (textArray.Length != 0)
                ArrangeTexts("Trouble is ahead!");
        }
    }

    void FixedUpdate()
    {
        if (Application.loadedLevelName.Equals("EndOfStory2Scene"))
            lava.transform.position += new Vector3(0, 0.002f, 0f);

        if (timer >= Time.time && fader != null)
        {
            fader.alpha -= 0.01f;
            if (fader.alpha == 0f)
            {
                faderObject.SetActive(false);
                player.setIsAlive(true);
            }
        }

        if (lava != null)
            lava.position += new Vector3(0f, 0.012f, 0f);
    }

    public IEnumerator ActivateMonsterFish(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        MonsterFish.setIsActive(true);
        if (textArray.Length != 0)
            ArrangeTexts("Something is approaching!!!");
    }

    public void IncreaseSonarWaves()
    {
        this.sonarWaves++;
        ArrangeTexts("sonar waves left: " + sonarWaves);
    }

    public void DecreaseSonarWaves()
    {
        this.sonarWaves--;
    }

    public void IncreaseFishFoods()
    {
        this.fishFoods++;
        ArrangeTexts("fish foods left: " + fishFoods);
    }

    public void DecreaseFishFoods()
    {
        this.fishFoods--;
    }

    public void ArrangeTexts (string newText)
    {
        int i;
        for (i = 0; i < (textArray.Length - 1); i++)
        {
            textArray[i].text = textArray[i + 1].text;
        }
        if (textArray.Length != 0)
            textArray[i].text = newText;
    }

    public void ArrangeTexts(string newText1, string newText2, string newText3)
    {
        textArray[0].text = newText1;
        textArray[1].text = newText2;
        textArray[2].text = newText3;
    }

    public bool CheckLastMessage (string message)
    {
        if (textArray[2].text.Equals(message))
            return true;
        else
            return false;
    }

    public bool CheckAllMessages (string message)
    {
        for (int i = 0; i < textArray.Length; i++)
        {
            if (!textArray[i].text.Equals(message))
                return false;
        }
        return true;
    }

    public int getSonarWaves()
    {
        return this.sonarWaves;
    }

    public int getFishFoods()
    {
        return this.fishFoods;
    }

    public void SceneLoad(string scene)
    {
        Application.LoadLevel(scene);
    }
}