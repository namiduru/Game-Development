using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameManager GM;
    [SerializeField] float heightCoefficient = 0.12f;
    [SerializeField] GameObject spinner;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.y < Screen.height * heightCoefficient) {
            if (spinner.transform.eulerAngles.z > 240 && spinner.transform.eulerAngles.z < 360) {
                Debug.Log("Green is clicked!");
                GM.PlayGreen();
            } else if (spinner.transform.eulerAngles.z > 0 && spinner.transform.eulerAngles.z < 120) {
                Debug.Log("Red is clicked!");
                GM.PlayRed();
            } else {
                Debug.Log("Blue is clicked");
                GM.PlayBlue();
            }
        }    
    }

}
