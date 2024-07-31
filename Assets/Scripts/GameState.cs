using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class GameState : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    DungeonGenerator Generator;
    public static GameState Instance; 
    public GameStates gameState;
    private float update = 0.0f;
    public float updateTime = 60.0f;
    void Awake(){
        Instance = this;
    }
    void Update(){
        
        update+=Time.deltaTime;
        if(update > updateTime ) {
            update=0.0f;
            changeGameState();
        }
    }
    private void changeGameState(){
        if(gameState == GameStates.Day) {
            gameState = GameStates.Night;
        }
        else gameState = GameStates.Day;
        Generator.gameState = gameState; 
        Debug.Log(gameState);
    }

}
public enum GameStates{
        Day, 
        Night
    }
