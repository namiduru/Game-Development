using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterFishScript : MonoBehaviour {

    public GameMaster gameMaster;
    public Transform MarineTransform;
    [Range(0.01f, 0.05f)]
    public float FishVelocityX;
    public float FishVelocityY;

    private bool isActive;
    private float timer;
    private readonly float foodTimer = 5f;

    public List<Vector3> _MarineTrackingList = new List<Vector3>();
    public Vector3 TargetTransform;
    public bool IsMarineFunctionWaiting = false;

    private void Start()
    {
        isActive = false;
        FishVelocityX = 0.075f;
        FishVelocityY = 0.075f;
        timer = Time.time;
    }

    private void FixedUpdate()
    {
        StartCoroutine(AddMarineTransformToList(0.5f));

        if (timer <= Time.time && isActive)
        {
            SetTargetPoint();
            TrackingMarine();
        }
    }

    private IEnumerator AddMarineTransformToList(float trackingTimeGap)
    {
       
        if (!IsMarineFunctionWaiting)
        {
            IsMarineFunctionWaiting = true;           
            if(MarineTransform != null) _MarineTrackingList.Add(MarineTransform.position);
            yield return new WaitForSeconds(trackingTimeGap);
            IsMarineFunctionWaiting = false;        
        }
      
    }

    private void SetTargetPoint()
    {
        if (_MarineTrackingList.Count != 0)
        {
            TargetTransform = _MarineTrackingList[0];
            if (Vector3.Distance (TargetTransform, transform.position) <= 0.5f)
                _MarineTrackingList.RemoveAt(0);
        }
        else
        {
            if (MarineTransform != null)
                TargetTransform = MarineTransform.position;
        }
    }

    public void TrackingMarine()
    {

        if (TargetTransform != null)
        {

            if ((this.transform.position.x == TargetTransform.x)
            && (this.transform.position.y == TargetTransform.y))
            {
                TargetTransform = Vector3.zero;
            }
            else
            {
                float xDis, yDis;
                xDis = transform.position.x - TargetTransform.x;
                yDis = transform.position.y - TargetTransform.y;

                if (xDis <= 0.3f && xDis >= -0.3f)
                    FishVelocityX = 0f;
                else
                {
                    if (xDis < 0)
                        FishVelocityX = 0.075f;
                    else
                        FishVelocityX = -0.075f;
                }

                if (yDis <= 0.3f && yDis >= -0.3f)
                {
                    FishVelocityY = 0f;
                }
                else
                {
                    if (yDis < 0)
                        FishVelocityY = 0.075f;
                    else
                        FishVelocityY = -0.075f;
                }
                this.transform.position += new Vector3(FishVelocityX, FishVelocityY, 0f);                                 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals ("Feed Fish"))
        {
            timer = Time.time + foodTimer;
            Destroy(other.gameObject);
        }
    }

    public void setIsActive (bool isActive)
    {
        this.isActive = isActive;
    }
}