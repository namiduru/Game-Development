using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeturalBrdigeJuicer : MonoBehaviour
{
    public GameObject LittlePicePrefab;

    public int InstAmount;

    public void BreakIt()
    {
        //StartCoroutine(JuicyBreak());
    }

    private IEnumerator JuicyBreak()
    {
        GameObject temp = null;
        Rigidbody tempRb = null;

        for (int j = 0; j < InstAmount; j++)
        {
            temp = Instantiate(LittlePicePrefab);
            temp.SetActive(true);
            temp.transform.position = transform.position;
            temp.transform.position += Vector3.right * Random.Range(-1f, 1f) * transform.localScale.x + Vector3.up * Random.Range(-0.1f, 0.1f);
            temp.SetActive(true);

            tempRb = temp.GetComponent<Rigidbody>();
            tempRb.AddExplosionForce(200f, transform.position, 10f, -10f, ForceMode.Force);

            Destroy(temp, 0.5f);
        }

        yield return null;
    }
}
