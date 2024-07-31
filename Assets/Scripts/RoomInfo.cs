// using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;
// using UnityEngine.AI; 
// using NavMeshPlus.Components;
// using Unity.AI.Navigation;
// using NavMeshSurface = NavMeshPlus.Components.NavMeshSurface;
public class DoorLocation : MonoBehaviour
{
    // Start is called before the first frame update
    
    //NWSE
    public List<CDirection> dirs; 
    public List<Vector2> coords;
    public Dictionary<CDirection, Vector2> doors; 
    public Tilemap CorridorUp; 
    public Tilemap CorridorDown; 
    public Tilemap CorridorRight;
    public Tilemap CorridorLeft;
    public Tilemap WallsUp; 
    public Tilemap WallsDown; 
    public Tilemap WallsRight;
    public Tilemap WallsLeft;
    public Tilemap Collisions;
    //NWSE
    public List<TileBase> walls;
    public bool hasFire;
    public int X; 
    public int Y; 
    
    
    void Awake(){
        doors = new Dictionary<CDirection, Vector2>();
        // {
            // for(int i=0; i<dirs.Count; i++){
            //     doors.Add(dirs[i], coords[i]);
            //     //Debug.Log("running");
            // }
            //TEMPORAL
        for(int i=0; i<dirs.Count; i++){
            // doors.Add(dirs[i], coords[i]);
            switch(dirs[i]){
                case CDirection.NORTH: doors[dirs[i]]= new Vector2(4, 10); break;
                case CDirection.SOUTH: doors[dirs[i]]= new Vector2(4, 1); break;
                case CDirection.WEST: doors[dirs[i]]= new Vector2(9, 6); break;
                case CDirection.EAST: doors[dirs[i]]= new Vector2(0, 6); break;
            }
            
        }
        //GetComponent
            // }
            //TEMPORAL
            // { CDirection.SOUTH, new Vector2(4, 1) },
            // { CDirection.EAST, new Vector2(0, 6) },
            // { CDirection.NORTH, new Vector2(4, 10) },
            // { CDirection.WEST, new Vector2(9, 6) }
            //TEMPORAL
            // foreach(var pair in doors){
            //     switch(pair.Key){
            //         case CDirection.NORTH: doors[pair.Key]= new Vector2(4, 10); break;
            //         case CDirection.SOUTH: doors[pair.Key]= new Vector2(4, 1); break;
            //         case CDirection.WEST: doors[pair.Key]= new Vector2(9, 6); break;
            //         case CDirection.EAST: doors[pair.Key]= new Vector2(0, 6); break;
            //     }
                
            // }
        // };
    }
    public void paintWall(CDirection direction, int X, int Y){
        //paintSingleTile()
        switch(direction){
            case CDirection.NORTH: 
            WallsUp.ClearAllTiles();
            CorridorUp.ClearAllTiles();
            paintSingleTile(Collisions, walls[0], new Vector2Int((int)doors[CDirection.NORTH].x,(int)doors[CDirection.NORTH].y ));
            // Debug.Log((int)doors[CDirection.NORTH].x+ (int)doors[CDirection.NORTH].y); 
            break;
            case CDirection.SOUTH: 
            WallsDown.ClearAllTiles();
            CorridorDown.ClearAllTiles();
            paintSingleTile(Collisions, walls[2], new Vector2Int((int)doors[CDirection.SOUTH].x,(int)doors[CDirection.SOUTH].y ));
            break;
            case CDirection.WEST: 
            WallsRight.ClearAllTiles();
            CorridorRight.ClearAllTiles();
            paintSingleTile(Collisions, walls[1], new Vector2Int((int)doors[CDirection.WEST].x,(int)doors[CDirection.WEST].y ));
            break;
            case CDirection.EAST: 
            WallsLeft.ClearAllTiles();
            CorridorLeft.ClearAllTiles();
            paintSingleTile(Collisions, walls[3], new Vector2Int((int)doors[CDirection.EAST].x,(int)doors[CDirection.EAST].y ));
            break;
        }
    }
    private void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        //var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile((Vector3Int)position, tile);
    }
    
}
