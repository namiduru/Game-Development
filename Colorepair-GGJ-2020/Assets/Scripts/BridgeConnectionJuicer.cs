using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeConnectionJuicer : MonoBehaviour
{
    public Transform[] BridgeConnectionPositions;
    public GameObject[] LittlePicePrefabs;

    public NeturalBrdigeJuicer[] Neturals;

    public void BreakConnections()
    {
        StartCoroutine(JuicyBreak());

        for(int i = 0; i < Neturals.Length; i++){
            Neturals[i].BreakIt();
        }
    }

    private IEnumerator JuicyBreak()
    {
        GameObject temp = null;
        Rigidbody tempRb = null;
        for (int i = 0; i < BridgeConnectionPositions.Length; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                temp = Instantiate(LittlePicePrefabs[i]);
                temp.transform.position = BridgeConnectionPositions[i].position;
                temp.transform.position += Vector3.right * Random.Range(-0.5f, 0.5f) + Vector3.up * Random.Range(-0.1f, 0.1f);
                temp.SetActive(true);

                tempRb = temp.GetComponent<Rigidbody>();
                tempRb.AddExplosionForce(200f, BridgeConnectionPositions[i].position, 10f, -10f, ForceMode.Force);

                Destroy(temp, 0.5f);
            }
        }

        yield return null;
    }
}
