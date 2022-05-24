using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story1Ending : MonoBehaviour {

    public MonsterFishScript fish;
    public Player player;

    public CanvasGroup fader;
    private bool end;

	// Use this for initialization
	void Start () {
        end = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (end)
        {
            fader.alpha += 0.05f;
            if (fader.alpha == 1)
                Application.LoadLevel("EndOfStoryScene");
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            player.setIsAlive(false);
            fish.enabled = false;
            fader.alpha = 0f;
            fader.gameObject.SetActive(true);
            end = true;
        }
    }
}
