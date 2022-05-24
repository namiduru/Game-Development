using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstIntro : MonoBehaviour {

    public float WaitTime;
    public string SceneName;

    private void Start()
    {
        StartCoroutine(WaitAndPassToScene());
    }

    public IEnumerator WaitAndPassToScene()
    {
        yield return new WaitForSeconds(WaitTime);
        Application.LoadLevel(SceneName);
    }


}
