using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenJuicer : MonoBehaviour
{
    public AnimationCurve ZoomOutCurve;

    private float _cameraInitialSize;

    private bool _isJuicing;

    private Coroutine _juiceCoroutine;

    private void Awake(){
        _cameraInitialSize = Camera.main.orthographicSize;
    }
    public void ScreenZoomOut(){
        if(_isJuicing){
            StopCoroutine(_juiceCoroutine);
        }
        _juiceCoroutine = StartCoroutine(ZoomOutJuice());
    }

    private IEnumerator ZoomOutJuice(){
        float timer = 0.25f;
        float maxTimer = timer;

        float startSize = Camera.main.orthographicSize;
        float targetSize = startSize * 0.1f;

        _isJuicing = true;

        yield return new WaitForSeconds(1.1f);

        while(timer > 0f){

            Camera.main.orthographicSize = startSize + targetSize * ZoomOutCurve.Evaluate(1 - timer / maxTimer) / 1.2f;

            Camera.main.transform.Rotate(Vector3.forward * Random.Range(-1f, 1f) * Time.deltaTime * 40f);

            timer -= Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.rotation = Quaternion.identity;
        Camera.main.orthographicSize = _cameraInitialSize;

        _isJuicing = false;

        yield return null;
    }
}
