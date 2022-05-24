using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeConnection : MonoBehaviour
{
    public BridgeConnectionType[] BridgeConnections;
    public GameObject[] ConnectionPieceGameObjects;
    private BridgeConnectionSequence _sequence;

    private BridgeConnectionJuicer _bridgeConnectionJuicer;

    // Order is red, green, blue
    [Header("BridgeMaterials")]
    [SerializeField] Material[] normalColors;
    [SerializeField] Material[] lightMaterials; 

    private void Awake(){
        AddSequence(BridgeConnections);

        _bridgeConnectionJuicer = GetComponent<BridgeConnectionJuicer>();
    }

    private void Start(){
        for (int i = 0; i < ConnectionPieceGameObjects.Length; i++)
        {
            //ConnectionPieceGameObjects[i].SetActive(false);
            setBridgeColor(ConnectionPieceGameObjects[i], false);
        }
        BreakConnections();
    }

    public void BreakConnections(){
        for (int i = 0; i < ConnectionPieceGameObjects.Length; i++)
        {
            //ConnectionPieceGameObjects[i].SetActive(false);
            setBridgeColor(ConnectionPieceGameObjects[i], false);
        }
        _bridgeConnectionJuicer.BreakConnections();
    }
    
    public void RebuildConnection(){
        StartCoroutine(DelayedRebuild(_sequence.GetCurrentIndex() - 1));
    }

    public Vector3 GetCurrentConnectionPosition(){
        return ConnectionPieceGameObjects[_sequence.GetCurrentIndex()].transform.position;
    }

    public bool Play(BridgeConnectionType p_type){
        return _sequence.Add(p_type);
    }

    public bool IsComplete(){
        return _sequence.IsComplete();
    }

    private void AddSequence(BridgeConnectionType[] p_tpyes){
        _sequence = new BridgeConnectionSequence(p_tpyes);
    }

    private IEnumerator DelayedRebuild(int p_rebuildIndex){
        yield return new WaitForSeconds(1.1f);

        // ConnectionPieceGameObjects[p_rebuildIndex].SetActive(true);
        setBridgeColor(ConnectionPieceGameObjects[p_rebuildIndex], true);
    }


    private void setBridgeColor(GameObject gameObject, bool isActive = true) {

        Material[] materials;
        if (isActive) {
            materials = normalColors;
        } else {
            materials = lightMaterials;
        }

        switch(gameObject.tag) {
            case "RedBridge": 
                gameObject.GetComponent<MeshRenderer>().material = materials[0];
                break;
            case "GreenBridge":
                gameObject.GetComponent<MeshRenderer>().material = materials[1];
                break;
            case "BlueBridge":
                gameObject.GetComponent<MeshRenderer>().material = materials[2];
                break;
            default:
                break;
        }
    }
}
