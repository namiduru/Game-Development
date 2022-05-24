using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionInputJuicer : MonoBehaviour
{
    public Transform[] BezierPoints;

    public GameObject CubePrefab;

    public Transform BlueRefRect, RedRefRect, GreenRefRect;

    public Material RedMaterial, BlueMaterial, GreenMaterial;

    private void Update(){

        if(Input.GetKeyDown(KeyCode.Space)){
            //LetThemFly(BridgeConnectionType.Green, BezierPoints[1].position, BezierPoints[2].position);

            //StartJuicing(BridgeConnectionType.Red, BezierPoints[2].position);
        }

        //Debug.DrawLine(BezierPoints[0].position, BezierPoints[1].position, Color.blue);
        //Debug.DrawLine(BezierPoints[1].position, BezierPoints[2].position, Color.blue);

        Debug.DrawLine(BlueRefRect.transform.position, BlueRefRect.transform.position + BlueRefRect.up * 5f, Color.blue);
        Debug.DrawLine(RedRefRect.transform.position, RedRefRect.transform.position + RedRefRect.up * 5f, Color.red);
        Debug.DrawLine(GreenRefRect.transform.position, GreenRefRect.transform.position + GreenRefRect.up * 5f, Color.green);
    }

    public void StartJuicing(BridgeConnectionType p_type, Vector3 p_targetPosition){
        Vector3 bezierPoint = Vector3.zero;
        Vector3 bezierDirection = Vector3.zero;
        Vector3 startPosition = Vector3.zero;
        Vector3 middlePosition = Vector3.zero;

        switch (p_type)
        {
            case BridgeConnectionType.Blue:
                {
                    bezierDirection = BlueRefRect.up;
                    startPosition = BlueRefRect.position;
                    break;
                }
            case BridgeConnectionType.Green:
                {
                    bezierDirection = GreenRefRect.up;
                    startPosition = GreenRefRect.position;
                    break;
                }
            case BridgeConnectionType.Red:
                {
                    bezierDirection = RedRefRect.up;
                    startPosition = GreenRefRect.position;
                    break;
                }
        }


        //bezierDirection = Quaternion.AngleAxis(180, Vector3.forward) * bezierDirection;

        Debug.Log(bezierDirection);

        //Debug.Break();

        middlePosition = (startPosition + p_targetPosition) / 2;

        Plane middlePlane = new Plane(Vector3.up, middlePosition);
        middlePlane = new Plane(middlePosition, middlePosition + Vector3.forward, middlePosition + Vector3.right);
        float distanceToPlane = 0f;
        bool hits = middlePlane.Raycast(new Ray(startPosition, bezierDirection), out distanceToPlane);

        if(!hits){
            Debug.Log("You fucked up");
        }

        bezierPoint = startPosition + bezierDirection * distanceToPlane;

        LetThemFly(p_type, bezierPoint, p_targetPosition);
    }

    private void LetThemFly(BridgeConnectionType p_type, Vector3 p_bezierPosition, Vector3 p_targetPosition){
        Material selectedMaterial = null;
        Transform selectedStartTransform = null;
        switch (p_type)
        {
            case BridgeConnectionType.Blue:
                {
                    selectedStartTransform = BlueRefRect;
                    selectedMaterial = BlueMaterial;
                    break;
                }
            case BridgeConnectionType.Green:
                {
                    selectedStartTransform = GreenRefRect;
                    selectedMaterial = GreenMaterial;
                    break;
                }
            case BridgeConnectionType.Red:
                {
                    selectedStartTransform = RedRefRect;
                    selectedMaterial = RedMaterial;
                    break;
                }
        }

        for (int i = 0; i < 40; i++)
        {
            GameObject temp = Instantiate(CubePrefab);
            temp.transform.position = selectedStartTransform.position
                + selectedStartTransform.right * Random.Range(-1f, 1f)
                + selectedStartTransform.up * Random.Range(-0.45f, 0.45f);

                temp.transform.localScale *= 1.3f;

            temp.GetComponent<MeshRenderer>().material = selectedMaterial;

            temp.SetActive(false);

            FlyingLittleCube tempScript = temp.AddComponent<FlyingLittleCube>();
            tempScript.Fly(temp.transform.position
                , p_bezierPosition + Vector3.right * Random.Range(-1f, 1f) + Vector3.up * Random.Range(1f, 1f)
                , p_targetPosition + Vector3.right * Random.Range(-0.35f, 0.35f) + Vector3.up * Random.Range(-0.1f, 0.1f));
        }
    }

    private void DrawBezier(Vector3 p_p0, Vector3 p_p1, Vector3 p_p2){
        for (float i = 0f; i < 1f; i += 0.05f)
        {
            Debug.DrawLine(QuadBezierCurve(p_p0, p_p1, p_p2, i)
                , QuadBezierCurve(p_p0, p_p1, p_p2, i + 0.05f)
            );
        }
    }

    private Vector3 QuadBezierCurve(Vector3 p_p0, Vector3 p_p1, Vector3 p_p2, float p_t)
    {
        return (Mathf.Pow(1f - p_t, 2f) * p_p0 + 2 * (1f - p_t) * p_t * p_p1 + Mathf.Pow(p_t, 2f) * p_p2);
    }
}
