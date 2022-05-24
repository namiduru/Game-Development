using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [Header("Transform Of Sumbmarine")]
    public Transform SubmarineTransform;
    
    [Header("Main Camera")]
    public Camera MainCamera;

    public bool IsOnYDiretion;

    private float _SubmarineXPosition;    

    private void FixedUpdate()
    {
        if (!IsOnYDiretion)
        {
            if (SubmarineTransform != null)
            {
                if (SubmarineTransform.position.x >= -4)
                {
                    MainCamera.transform.position =
                        new Vector3(SubmarineTransform.position.x + 3.91f, MainCamera.transform.position.y, MainCamera.transform.position.z);
                }
            }
        }
        else
        {
            if (SubmarineTransform != null)
            {
                MainCamera.transform.position =
                        new Vector3(SubmarineTransform.position.x, SubmarineTransform.position.y, MainCamera.transform.position.z);
                
            }
        }
    }
}