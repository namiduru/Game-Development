using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BridgeConnectionType{
    Blue,
    Red,
    Green
}

public class BridgeConnectionSequence
{
    public Dictionary<int, BridgeConnectionType> Sequence;

    private int _currentSequenceIndex;

    public BridgeConnectionSequence(BridgeConnectionType[] p_squence){
        Sequence = new Dictionary<int, BridgeConnectionType>();

        for(int i = 0; i < p_squence.Length; i++){
            Sequence.Add(i, p_squence[i]);
        }

        _currentSequenceIndex = 0;
    }

    public int GetCurrentIndex(){
        return _currentSequenceIndex;
    }

    public bool Add(BridgeConnectionType p_type){
        if(p_type == Sequence[_currentSequenceIndex]){
            _currentSequenceIndex++;
            return true;
        }else{
            return false;
        }
    }

    public bool IsComplete(){
        if(_currentSequenceIndex == Sequence.Count){
            return true;
        }else{
            return false;
        }
    }
}
