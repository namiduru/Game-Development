using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameMaster gameMaster;
    public Rigidbody2D myRigidbody;
    public Animator myAnimator;
    public GameObject fishFoodPrefab;
    public Scanner scanner;
    public AudioSource scannerSound;
    public Vector2 velocity;

    private string noSonarWaveText = "no sonar waves left!";
    private string noFishFoodsText = "no fish foods left!";

    private float xVel;
    private float yVel;

    private bool win = false;

    public GameObject endingObject;
    public CanvasGroup ending;
    public CanvasGroup victoryEnding;
    private float timer;
    private bool isAlive;

    void Start()
    {
        Time.timeScale = 1f;
        timer = 0f;

        xVel = 20f;
        yVel = 20f;

        if (!Application.loadedLevelName.Equals("EndOfStoryScene"))
        {
            endingObject.SetActive(false);
            ending.alpha = 0f;
        }

        if (Application.loadedLevelName.Equals("EndOfStory2Scene"))
            isAlive = true;
    }

	// Update is called once per frame
	void Update ()
    {
        if (isAlive)
        {
            myRigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * xVel * Time.deltaTime, Input.GetAxis("Vertical") * yVel * Time.deltaTime));
            velocity = myRigidbody.velocity;

            if (Input.GetAxis("Horizontal") < (-0.1f) && velocity.x < 0.2f && velocity.x > (-0.2f))
            {
                myAnimator.SetBool("TurnLeft", true);
                myAnimator.SetBool("TurnRight", false);
            }
            else if (Input.GetAxis("Horizontal") > (0.1f) && velocity.x < 0.2f && velocity.x > (-0.2f))
            {
                myAnimator.SetBool("TurnRight", true);
                myAnimator.SetBool("TurnLeft", false);
            }

            if (Input.GetKeyUp(KeyCode.Space) && gameMaster.getSonarWaves() > 0)
            {
                if (!scanner.getScan())
                {
                    scanner.setScan(true);
                    scannerSound.Play();
                    gameMaster.ArrangeTexts("sonar waves left: " + (gameMaster.getSonarWaves() - 1));
                }
                else
                    gameMaster.ArrangeTexts("next sonar wave is not ready!");
            }

            else if (Input.GetKeyUp(KeyCode.Space) && gameMaster.getSonarWaves() == 0)
            {
                if (gameMaster.CheckAllMessages(noSonarWaveText))
                {
                    gameMaster.ArrangeTexts("I SAID NO SONAR WAVES LEFT!");
                    noSonarWaveText = "I SAID NO SONAR WAVES LEFT!";
                }
                else
                {
                    gameMaster.ArrangeTexts(noSonarWaveText);
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftControl) && gameMaster.getFishFoods() > 0)
            {
                Instantiate(fishFoodPrefab, transform.position, Quaternion.identity);
                gameMaster.DecreaseFishFoods();
            }

            else if (Input.GetKeyUp(KeyCode.LeftControl) && gameMaster.getFishFoods() == 0)
            {
                if (gameMaster.CheckAllMessages(noSonarWaveText))
                {
                    gameMaster.ArrangeTexts("I SAID NO FISH FOODS LEFT!");
                    noFishFoodsText = "I SAID NO FISH FOODS LEFT!";
                }
                else
                {
                    gameMaster.ArrangeTexts(noFishFoodsText);
                }
            }
        }

        else if (!isAlive && timer >= Time.time)
        {
            if (ending != null)
            {
                if (victoryEnding != null)
                {
                    if (victoryEnding.gameObject.active == false || Application.loadedLevelName.Equals("EndOfStoryScene"))
                        ending.alpha += 0.005f;
                }
                else
                {
                    if (Application.loadedLevelName.Equals("Story 2"))
                        ending.alpha += 0.005f;
                }
            }
            if (!Application.loadedLevelName.Equals("EndOfStoryScene") && victoryEnding != null)
                victoryEnding.alpha += 0.01f;

            if (Application.loadedLevelName.Equals("EndOfStoryScene") && ending.alpha == 1)
                Application.LoadLevel("Story 2");

            else if (Application.loadedLevelName.Equals("Story") && victoryEnding.alpha == 1 && win == true)
                Application.LoadLevel("EndOfStoryScene");



        }
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Obstacle"))
        {
            Die();
            //Destroy(gameObject);
            gameMaster.ArrangeTexts("Critical Failure!!!");
        }       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Monster"))
        {
            Die();
            //Destroy(gameObject);
            gameMaster.ArrangeTexts("Critical Failure!!!");
        }

        else if (other.gameObject.tag.Equals("End"))
        {
            //Destroy(gameObject);
            isAlive = false;
            timer = Time.time + 3f;
            win = true;
            victoryEnding.gameObject.SetActive(true);
            victoryEnding.alpha = 0f;
            gameMaster.ArrangeTexts("Danger is evaded!");
        }

        else if (other.gameObject.tag.Equals("Sonar"))
        {
            gameMaster.IncreaseSonarWaves();
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag.Equals("Fish Food"))
        {
            gameMaster.IncreaseFishFoods();
            Destroy(other.gameObject);
        }
    }

    public void Die()
    {
        this.isAlive = false;
        timer = Time.time + 3f;
        endingObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void setIsAlive (bool isAlive)
    {
        this.isAlive = isAlive;
    }
}
