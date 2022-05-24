using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story1FinalMonsters : MonoBehaviour
{
    public Transform player;
    public Transform[] monsters;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        MoveMonsters();
	}

    public void MoveMonsters()
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].position = Vector3.MoveTowards(monsters[i].position, player.position, 0.01f);
        }
    }
}
