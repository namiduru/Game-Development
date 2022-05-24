using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingLittleCube : MonoBehaviour
{
    public void Fly(Vector3 p_start, Vector3 p_bezier, Vector3 p_target){
        gameObject.SetActive(true);
        StartCoroutine(SmoothFlyer(p_start, p_bezier, p_target));
    }

    private IEnumerator SmoothFlyer(Vector3 p_start, Vector3 p_bezier, Vector3 p_target){
        float timer = 1f;
        float maxTimer = timer;

        float waitTimer = Random.Range(0f, 0.2f);
        float waitTimerMax = waitTimer;

        Vector3 startScale = Vector3.zero;
        Vector3 targetScale = transform.localScale;
        while(waitTimer > 0f){

            transform.localScale = Vector3.Lerp(startScale, targetScale, 1 - waitTimer / waitTimerMax);

            waitTimer -= Time.deltaTime;

            yield return null;
        }

        transform.localScale = targetScale;

        while(timer > 0f){

            transform.position = QuadBezierCurve(p_start, p_bezier, p_target, 1 - timer / maxTimer);

            timer -= Time.deltaTime;

            yield return null;
        }

        transform.position = p_target;

        Destroy(gameObject);
    }

    private Vector3 QuadBezierCurve(Vector3 p_p0, Vector3 p_p1, Vector3 p_p2, float p_t)
    {
        return (Mathf.Pow(1f - p_t, 2f) * p_p0 + 2 * (1f - p_t) * p_t * p_p1 + Mathf.Pow(p_t, 2f) * p_p2);
    }
}
