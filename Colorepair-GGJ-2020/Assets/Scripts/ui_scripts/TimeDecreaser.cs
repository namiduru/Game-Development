using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDecreaser : MonoBehaviour
{

    private float shakeAmount = 1f;
    private Vector3 startingPosition;

    private Coroutine currentCoroutine;

     public void displayTimeDecrease() {
        currentCoroutine = StartCoroutine(timeDecreaser());
    }

    void Start(){
        startingPosition = this.transform.position;
    }

    void Update() {
        slideTextForward();
    }

    private IEnumerator timeDecreaser() {
        TextMeshProUGUI textMeshProUGUI  = this.GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = "- 2";

        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
            this.transform.position = startingPosition;
        }

        yield return new WaitForSeconds(1.1f);

        textMeshProUGUI.text = "";
        this.transform.position = startingPosition;
    }

    private void slideTextForward() {
        this.transform.position = new Vector3(this.transform.position.x - this.shakeAmount, this.transform.position.y, this.transform.position.z);
    }
}
