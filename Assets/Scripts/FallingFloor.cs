using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingFloor : MonoBehaviour
{
    // Start is called before the first frame update
    // public Animator animator;
    // public Tilemap tilemap; 
    //private Collider2D collider;
    private bool countdown;
    // private float update; 
    private Vector2Int pos; 
    private List<List<GameObject>> tiles;
    private GameObject player; 
    public GameObject tile;
    private int X; 
    private int Y; 
    private bool inside;
    private float originalSpeed; 
    void Start()
    {
        // update =0.0f;
        tiles = new List<List<GameObject>>();
        X = GetComponent<DoorLocation>().X;
        Y = GetComponent<DoorLocation>().Y;
        for(int i = 0; i<8; i++){
            tiles.Add(Enumerable.Repeat<GameObject>(null, 8).ToList());
        }   
        for(int i=0; i<8; i++){
            for(int j =0; j<8; j++){
                tiles[i][j]= Instantiate(tile, new Vector2(j+1.5f+X*11, i+2.5f+Y*11), Quaternion.identity);
            }
        }
        player = GameObject.Find("Player");
        originalSpeed = player.GetComponent<PlayerController>().moveSpeed; 
        //collider = GetComponent<Collider2D>();
        
        

    }
    void Update(){
        // update+=Time.deltaTime;
        if(countdown){
            if(inside){
                pos = new Vector2Int((int)player.transform.position.x%11 - 1, (int)player.transform.position.y%11 -2);
                try{
                    if(tiles[pos.y][pos.x].GetComponent<FallingTile>().Resistance<10){
                        //player.Fall()
                        //primary entry collider in the center, so that the player has less time to esacep, 
                        //second collider bigger taking up all the room for the inside variable
                    }
                } catch(Exception){}
            }
            
            DecreaseResistance();
            
        }


        
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag=="Player"){
            countdown = true;
            inside =true;
            player.GetComponent<PlayerController>().moveSpeed/=2;


        }
    }
    // void OnTriggerExit2D(Collider2D collider){
    //     if(collider.tag=="Player"){
    //         player.GetComponent<PlayerController>().moveSpeed*=2;
    //         inside = false;
    //     }
    // }
    private void DecreaseResistance(){
        for(int i =0; i<8; i++){
            for(int j=0; j<8; j++){
                if(pos.x==j&&pos.y==i) tiles[i][j].GetComponent<FallingTile>().Resistance-=0.045f; 
                else if(Mathf.Sqrt(Mathf.Pow(j-pos.x, 2)+Mathf.Pow(i-pos.y, 2))<2) tiles[i][j].GetComponent<FallingTile>().Resistance-=0.03f;
                else tiles[i][j].GetComponent<FallingTile>().Resistance-=0.015f;
            }
        }
    }
    public void Exited(){
        player.GetComponent<PlayerController>().moveSpeed= originalSpeed;
        inside = false;
    }
    
    
}
