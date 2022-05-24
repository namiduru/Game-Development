using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float TimeLeft;

    public BridgeConnection[] Bridges;
    public ScreenJuicer ScreenJ;
    public SpinnerJuicer SpinnerJuicer;
    private int _currentBridgeIndex;

    private bool _gameLoopActive;

    #region UI Elements
    [Header("UI Elements")]
    public TimeDecreaser timeDecreaser;
    #endregion

    private void Awake(){
        TimeLeft = 30f;

        _gameLoopActive = true;

        Time.timeScale = 1;
    }

    private void Update(){
        if(!_gameLoopActive)
            return;

        if(TimeLeft <= 0f){
            LoseGame();
        }else{
            TimeLeft -= Time.deltaTime;
        }
    }

    public void PlayGreen(){
        SpinnerJuicer.StartJuicing(BridgeConnectionType.Green, Bridges[_currentBridgeIndex].GetCurrentConnectionPosition());
        PlayPiece(BridgeConnectionType.Green);
    }

    public void PlayBlue(){
        SpinnerJuicer.StartJuicing(BridgeConnectionType.Blue, Bridges[_currentBridgeIndex].GetCurrentConnectionPosition());
        PlayPiece(BridgeConnectionType.Blue);
    }

    public void PlayRed(){
        SpinnerJuicer.StartJuicing(BridgeConnectionType.Red, Bridges[_currentBridgeIndex].GetCurrentConnectionPosition());
        PlayPiece(BridgeConnectionType.Red);
    }
    public void PlayPiece(BridgeConnectionType p_type){
        bool result = Bridges[_currentBridgeIndex].Play(p_type);

        if (result)
        {
            Bridges[_currentBridgeIndex].RebuildConnection();
            ScreenJ.ScreenZoomOut();
            Debug.Log("Correct");
        }
        else
        {
            timeDecreaser.displayTimeDecrease();
            DecreaseGameTime(2f);
            Debug.Log("Incorrect");
        }

        if(Bridges[_currentBridgeIndex].IsComplete()){
            Debug.Log("Bridge Complete");
            StartCoroutine(DelayedBridgeCollapse());
            GoToNextBridge();
        }
    }

    public void GoToNextBridge(){
        _currentBridgeIndex++;

        if(_currentBridgeIndex == Bridges.Length){
            WinGame();
        }
    }

    public void AddGameTime(float p_amount){
        TimeLeft += p_amount;
    }

    public void DecreaseGameTime(float p_amount) {
        if (TimeLeft - p_amount <= 0) {
            TimeLeft = 0;
        } else {
            TimeLeft -= p_amount;
        }
    }

    private void LoseGame(){
        Debug.Log("Game Lost");
        _gameLoopActive = false;
    }

    private void WinGame(){
        Debug.Log("GameComplete");
        _gameLoopActive = false;
    }

    private IEnumerator DelayedBridgeCollapse(){
        yield return new WaitForSeconds(1.5f);

        Bridges[_currentBridgeIndex - 1].BreakConnections();
        
        yield return new WaitForSeconds(0.5f);

        Bridges[_currentBridgeIndex - 1].gameObject.SetActive(false);
    }
}
