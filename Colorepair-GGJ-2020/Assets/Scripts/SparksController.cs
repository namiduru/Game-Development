using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksController : MonoBehaviour
{
    public Transform[] TravelPositionTransforms;
    private int currentIndex = 0;

    private void Start(){
        // StartMovement();
    }

    public void StartMovement(){
        StartCoroutine(MovementIterator());
    }

    private IEnumerator MoveToNextCheckPoint(){

        if (this.currentIndex + 1 == TravelPositionTransforms.Length) {
            yield return null;
        } else {
            yield return StartCoroutine(MoveBetweenPositions(TravelPositionTransforms[this.currentIndex].position
            , TravelPositionTransforms[this.currentIndex + 1].position));
        }

        yield return null;
    }

    private IEnumerator MovementIterator(){
        int currentIndex = 0;
        int nextIndex = 1;

        while(nextIndex != TravelPositionTransforms.Length){

            yield return StartCoroutine(MoveBetweenPositions(TravelPositionTransforms[currentIndex].position
            , TravelPositionTransforms[nextIndex].position));

            currentIndex++;
            nextIndex++;

            yield return null;
        }
        

        yield return null;
    }

    private IEnumerator MoveBetweenPositions(Vector3 p_startPosition, Vector3 p_targetPosition){
        float timer = Vector3.Distance(p_startPosition, p_targetPosition);
        float timerMax = timer;

        while(timer > 0f){
            timer -= Time.deltaTime;

            transform.position = Vector3.Lerp(p_startPosition, p_targetPosition, 1 - timer / timerMax);

            yield return null;
        }

        transform.position = p_targetPosition;
    }
}
