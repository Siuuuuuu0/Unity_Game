using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.TerrainTools;
using UnityEngine.Tilemaps;
public class Visualizer : MonoBehaviour
{
    
    [SerializeField]
    private Tilemap floorTileMap; 
    [SerializeField]
    private Tilemap Collisions;
    [SerializeField]
    private Tilemap SecondaryField;
    [SerializeField]
    private TileBase HallTile;
    [SerializeField]
    private TileBase HallTile2;
    [SerializeField]
    private TileBase HallTile3;
    [SerializeField]
    private TileBase HallTile4;
    [SerializeField]
    private TileBase WallTile;
    [SerializeField]
    private TileBase DoorTile;
    [SerializeField]
    private TileBase ExitTile;
    [SerializeField]
    private TileBase Key;
    [SerializeField]
    private TileBase MedKit;
    [SerializeField]
    GameObject key;
    [SerializeField] 
    GameObject Door;
    [SerializeField]
    GameObject medKit;
    Dictionary<CellType, TileBase> tiles; 
    
    void Start(){
        tiles = new Dictionary<CellType, TileBase>{
            {CellType.Hall0, HallTile},
            {CellType.Hall1, HallTile2},
            {CellType.Hall2, HallTile3},
            {CellType.Hall3, HallTile4},
            {CellType.Wall, WallTile},
            {CellType.Door, DoorTile},
            {CellType.Exit, ExitTile},
            {CellType.Key, Key},
            {CellType.MedKit, MedKit} 
        };
    }

    public void paintFloorTiles(List<List<Room>> rooms){
        
        paintTiles(rooms); 
    }

    private void paintTiles(List<List<Room>> rooms)
    {
        foreach(var list in rooms) {
            foreach(var room in list){
                //Debug.Log(new Vector2Int(room.getX(), room.getY()).ToString());
                if(room.getX()!=-1&&room.getY()!=-1){
                    paintRoom(room);
                    //room.paintRoomFirst(floorTileMap, SecondaryField, Collisions, tiles, null)   ;
                }
            }
            
        }
    }
    public void paintRoomUpdate(Room r){
        List<List<Cell>> temp = r.ToListUpdate();
        foreach(var list in temp){
            foreach(var cell in list){
                
                if(cell is Hall ){
                    float fire = cell.Fire;
                    if(fire <1.0f){
                        paintSingleTile(floorTileMap, HallTile, new Vector2Int(cell.X, cell.Y));
                    }
                    else if(fire <3.0f){
                        paintSingleTile(floorTileMap, HallTile2, new Vector2Int(cell.X, cell.Y));
                    }
                    else if(fire <6.0f){
                        paintSingleTile(floorTileMap, HallTile3, new Vector2Int(cell.X, cell.Y));
                    }
                    else paintSingleTile(floorTileMap, HallTile4, new Vector2Int(cell.X, cell.Y));
                    
                    // if(cell.Key){
                    //     paintSingleTile(SecondaryField, Key, new Vector2Int(cell.X, cell.Y));
                    // }
                    // if(cell.MedKit>0){
                    //     paintSingleTile(SecondaryField, MedKit, new Vector2Int(cell.X, cell.Y));
                    // }
                }
                // else if(cell is Wall )
                //     paintSingleTile(Collisions, WallTile, new Vector2Int(cell.X, cell.Y));
                // // else if(cell is Door )
                // //     paintSingleTile(Collisions, DoorTile, new Vector2Int(cell.X, cell.Y));
                // else if(cell is Exit )
                //     paintSingleTile(floorTileMap, ExitTile, new Vector2Int(cell.X, cell.Y));
            }
        }
    }
    public void paintRoom(Room r){
        List<List<Cell>> temp = r.ToList();
        foreach(var list in temp){
            foreach(var cell in list){
                
                if(cell is Hall ){
                    float fire = cell.Fire;
                    if(fire <1.0f){
                        paintSingleTile(floorTileMap, HallTile, new Vector2Int(cell.X, cell.Y));
                    }
                    else if(fire <3.0f){
                        paintSingleTile(floorTileMap, HallTile2, new Vector2Int(cell.X, cell.Y));
                    }
                    else if(fire <6.0f){
                        paintSingleTile(floorTileMap, HallTile3, new Vector2Int(cell.X, cell.Y));
                    }
                    else paintSingleTile(floorTileMap, HallTile4, new Vector2Int(cell.X, cell.Y));
                    
                    if(cell.Key){
                        //paintSingleTile(SecondaryField, Key, new Vector2Int(cell.X, cell.Y));
                        spawnKey(new Vector3(cell.X+0.5f, cell.Y+0.5f, 0));
                    }
                    if(cell.MedKit>0){
                        spawnMedKit(new Vector3(cell.X+0.5f, cell.Y+0.5f, 0));
                        //paintSingleTile(SecondaryField, MedKit, new Vector2Int(cell.X, cell.Y));
                    }
                }
                else if(cell is Wall )
                    paintSingleTile(Collisions, WallTile, new Vector2Int(cell.X, cell.Y));
                else if(cell is Door )
                    spawnDoor(new Vector3(cell.X+0.5f, cell.Y+0.5f, 0));
                    //paintSingleTile(Collisions, DoorTile, new Vector2Int(cell.X, cell.Y));
                else if(cell is Exit )
                    paintSingleTile(floorTileMap, ExitTile, new Vector2Int(cell.X, cell.Y));
            }
        }
    }
    private void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }
    public void paintKit(Vector2Int pos){
        paintSingleTile(SecondaryField, MedKit, pos);
    }
    public void paintKey(Vector2Int pos){
        paintSingleTile(SecondaryField, Key, pos);
    }
    private void spawnPrefab(Vector3 spawnPos, GameObject prefab){
        GameObject spawnedPrefab = Instantiate(prefab, spawnPos, Quaternion.identity);
    }
    public void spawnKey(Vector3 spawnpos){
        spawnPrefab(spawnpos, key);
    } 
    private void spawnDoor(Vector3 spawnpos){
        spawnPrefab(spawnpos, Door);
    }
    private void spawnMedKit(Vector3 pos){
        spawnPrefab(pos, medKit);
    }
}

// dirs = which one is it facing
// else if(Cell is Wall){
//     if(Cell.X%8==0){
//         if(Cell.Y%8==6){
//             NW
//         }
//         else if(Cell.Y%8==0){
//             SW
//         }
//         else{
//             W
//         }
//     }
//     else if(Cell.X%8==6){
//         if(Cell.Y%8==6){
//             NE
//         }
//         else if(Cell.Y%8==0){
//             SE
//         }
//         else{
//             E
//         }
//     }
//     else if (Cell.Y%8==0){
//         S
//     }
//     else{
//         N
//     }
// }
    
