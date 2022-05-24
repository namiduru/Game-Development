using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuratScene : MonoBehaviour
{

    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject canvas;

   public void retry() {
       Time.timeScale = 1f;
       SceneManager.LoadScene("MuratScene");
   }

   void Update() {
       if (gameManager.TimeLeft < 2f) {
           Time.timeScale = 0f;
           canvas.SetActive(true);
       }
   }

   public void pauseAndContinueGame() {
       if (Time.timeScale != 0) {
           Time.timeScale = 0f;
       } else {
           Time.timeScale = 1;
       }
   }

   public void LoadScene() {
       SceneManager.LoadScene("MuratScene");
   }
}
