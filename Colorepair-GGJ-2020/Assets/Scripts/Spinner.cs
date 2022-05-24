using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float SpinnerVelocity = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, SpinnerVelocity * Time.deltaTime * 50f);
        // Debug.Log(transform.rotation.eulerAngles.z);
    }
}
