using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public List<Transform> unUsed_Obstacles = new List<Transform>();
    public List<Transform> used_Obstacles = new List<Transform>();

    private readonly float xVelocity = (-0.05f);
    private readonly Vector3 hidden = new Vector3(0f, 30f, 0f);
    private readonly Vector3 visible = new Vector3(18f, 0f, 0f);
    private readonly float xHidPosition = (-18f);
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        MoveObstacles();
        CheckUsedObstacles();
	}

    protected void CheckUsedObstacles()
    {
        if (used_Obstacles[0].position.x <= xHidPosition)
        {
            unUsed_Obstacles.Add(used_Obstacles[0]);
            used_Obstacles[0].position = hidden;
            used_Obstacles.Remove(used_Obstacles[0]);
            GenerateObstacle();
        }
    }

    protected void GenerateObstacle()
    {
        int randomIndex = (int)(Random.Range(0f, unUsed_Obstacles.Count));
        unUsed_Obstacles[randomIndex].position = visible;
        used_Obstacles.Add(unUsed_Obstacles[randomIndex]);
        unUsed_Obstacles.Remove(unUsed_Obstacles[randomIndex]);   
    }

    protected void MoveObstacles()
    {
        for (int i = 0; i < used_Obstacles.Count; i++)
        {
            used_Obstacles[i].position += new Vector3(xVelocity, 0f, 0f);
        }
    }
}
