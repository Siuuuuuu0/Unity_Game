using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class FireScript : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    DungeonGenerator generator;
    public Tilemap tilemap;
    // public Tilemap fire;
    private List<List<float>> fireMap;
    public TileBase fire0; 
    public TileBase fire1; 
    public TileBase fire2; 
    public TileBase fire3; 
    float update;
    
    void Start(){
        generator = GameObject.Find("Generator").GetComponent<DungeonGenerator>();
        fireMap = new List<List<float>>();
        for(int i = 0; i<8; i++){
            fireMap.Add(Enumerable.Repeat<float>(0, 8).ToList());
        }
        update = 0.0f;
    }
    void Update()
    {
        update+=Time.deltaTime;
        if(update>1.0f){
            switch(generator.gameState){
                case GameStates.Day: 
                    FireAlgo();
                    //Debug.Log("Running");
                    break;
                case GameStates.Night: 
                    FireAlgoReverse();
                    break;
            }
            update =0.0f;
        }
    }
    public void FireAlgo(){
        
        for(int i =0; i<8; i++){
            for(int j=0; j<8; j++){
                try{
                    fireMap[i][j] *=  1.0f + (Random.Range(1, 10) / 100.0f);
                    RoundFire(i, j);
                    
                }catch (Exception){}
                //Debug.Log("Running");
            }
        }

    }
    private void RoundFire(int y, int x){
        for(int i=y-1; i<=y+1; i++){
            for(int j=x-1; j<=x+1; j++){
                try{
                    fireMap[i][j]+=0.005f;
                    if(fireMap[i][j]>10.0f) 
                        fireMap[i][j] =10.0f;
                    paintFire(j, i);
                }
                catch(Exception){}
            }
        }
    }
    public void FireAlgoReverse(){
        for(int i =0; i<8; i++){
            for(int j=0; j<8; j++){
                try{
                    fireMap[i][j] /=  1.0f + (Random.Range(1, 10) / 100.0f);
                    ReverseRoundFire(i, j);
                }catch (Exception){}
                //Debug.Log("Running");
            }
        }
    }

    private void ReverseRoundFire(int y, int x)
    {
        for(int i=y-1; i<=y+1; i++){
            for(int j=x-1; j<=x+1; j++){
                try{
                    fireMap[i][j]-=0.005f;
                    if (fireMap[i][j]<0.0f)
                        fireMap[i][j]=0.0f;
                    paintFire(j, i);
                }
                catch(Exception){}
            }
        }
    }
    private void paintTile(Tilemap tilemap, int x, int y, TileBase tile){
        tilemap.SetTile((Vector3Int)(new Vector2Int(x, y)), tile);
    }
    private void paintFire(int x, int y) {
        if(fireMap[y][x]<1.0f) 
            paintTile(tilemap, x+1, y+2, fire0);
        else if(fireMap[y][x]<3.5f)
            paintTile(tilemap, x+1, y+2, fire1);
        else if(fireMap[y][x]<7.5f)
            paintTile(tilemap, x+1, y+2, fire2);
        else 
            paintTile(tilemap, x+1, y+2, fire3);
    }
    public float getTileFire(Vector2Int pos){
        return fireMap[pos.y-2][pos.x-1];
    }
}
