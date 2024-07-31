using System;
// using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

// using System.Data;
// using System.Linq;
// using Unity.AI.Navigation;
// using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
// using UnityEngine.UIElements;
// using Random = UnityEngine.Random;
// using UnityEngine.AI;
// using UnityEngine.UI;
//using Pathfinding;

public class DungeonGenerator : MonoBehaviour
{
    //public NavMeshSurface surface;
    public GameObject navMesh;
    // public NavMeshData data;
    [SerializeField]
    private Visualizer Visualizer; 
    
    private List<List<Room>> field;
    // private float update;
    [SerializeField]
    Tilemap tileMap;
    [SerializeField]
    List<TileData> tileDatas;
    public GameStates gameState; 
    [SerializeField]
    //public GameObject grid;
    private Dictionary<TileBase, TileData> dataFromTiles; 
    public List<List<Descriptor>> Field;
    public GameObject door;
    public GameObject key; 
    public Rooms rooms;
    public TerrainGenerator2 terrain; 
    public bool ToLoad; 
    public void runGeneration(){
        terrain = new  TerrainGenerator2(11, 11, 5, 5, rooms, door, key);
        Field = terrain.getField();
        navMesh.GetComponent<NavMeshGenerator>().Generate();
        //Visualizer.paintFloorTiles(field);
    }
    // protected List<List<Descriptor>>  runTerrainGeneration(){
    //     return (TerrainGenerator2.getRooms(door, key, rooms));
    // }
    public void Start(){
        //Instantiate(grid, new Vector2(50, 50), Quaternion.identity);
        if(!LoadedData.loadOccured){
            runGeneration();
        }
        else{
            Load(new TerrainGenerator2(10, 10, LoadedData.mapData.rooms, rooms));
             
        }
        //AstarPath astar = AstarPath.active;
        //data.GetComponent<NavMeshSurface>().BuildNavMesh();
        //surface.;
        // setMedKits();
        // setKeys();
    }
    public void Load(TerrainGenerator2 terrainGenerator){
        terrain = terrainGenerator; 
        Field = terrain.getField(); 
        navMesh.GetComponent<NavMeshGenerator>().Generate(); 
        LoadObjects(LoadedData.objectsData.objects); 

    }
    public void LoadObjects(float[][] objects){
        for(int i=0; i<objects.Length; i++){
            // Debug.Log(objects[i][0]);
            if(objects[i]==null) continue;
            if((allItems)(int)objects[i][0]==allItems.Chest){
                GameObject chest = Instantiate(InventoryManager.Instance.chest, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity);
                int count = (int)objects[i][3]; 
                if(count==0){
                    chest.GetComponent<ChestScript>().OpenAtStart();
                }
                else{
                    for(int j =1; j<=count; j++){
                        GameObject temp = Instantiate(objects, i+j);
                        temp.SetActive(false); 
                        chest.GetComponent<ChestScript>().addItemAfterSave((allItems)(int)objects[i+j][0], temp);  
                    }
                    i+=count; 
                }
            }
            else if((allItems)(int)objects[i][0]==allItems.LittleChest){
                GameObject chest = Instantiate(InventoryManager.Instance.littleChest, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
                int count = (int)objects[i][3]; 
                if(count==0){
                    chest.GetComponent<LittleChestScript>().OpenAtStart();
                    
                }
                else{
                    for(int j =1; j<=count; j++){
                        GameObject temp = Instantiate(objects, i+j);
                        temp.SetActive(false); 
                        chest.GetComponent<LittleChestScript>().addItemAfterSave((allItems)(int)objects[i+j][0], temp);  
                    }
                    i+=count; 
                }
            }
            else {
                Instantiate(objects, i); 
            }
        }
    }
    public GameObject Instantiate(float[][] objects, int i){
        GameObject gameObject = null; 
        switch((allItems)objects[i][0]){
            case allItems.Keys: return Instantiate(InventoryManager.Instance.key, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
            // break;
            case allItems.MedKit : gameObject = Instantiate(InventoryManager.Instance.MedKit, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
            gameObject.GetComponent<KitValue>().MedKit = (int)objects[i][3]; 
            break; 
            case allItems.LittleMedKit : gameObject = Instantiate(InventoryManager.Instance.LittleMedKit, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
            gameObject.GetComponent<LittleMedKitScript>().littleMedKit = (int)objects[i][3]; 
            break; 
            case allItems.SpeedBoost : gameObject = Instantiate(InventoryManager.Instance.SpeedBoost, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
            gameObject.GetComponent<BoostScript>().time = (int)objects[i][3]; 
            break; 
            case allItems.Strength : gameObject = Instantiate(InventoryManager.Instance.Strength, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
            gameObject.GetComponent<BoostScript>().time = (int)objects[i][3];
            break;
            case allItems.Invisibility : gameObject = Instantiate(InventoryManager.Instance.Invisibility, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
            gameObject.GetComponent<BoostScript>().time = (int)objects[i][3]; 
            break;
            case allItems.Magnifier : gameObject = Instantiate(InventoryManager.Instance.Magnifier, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
            gameObject.GetComponent<BoostScript>().time = (int)objects[i][3]; 
            break;
            case allItems.Lantern : gameObject = Instantiate(InventoryManager.Instance.Lantern, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
            gameObject.GetComponent<BoostScript>().time = (int)objects[i][3]; 
            break;
            case allItems.Armor : 
                switch (objects[i][3])
                {
                    case 1 : gameObject = Instantiate(InventoryManager.Instance.SmallArmor, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
                    gameObject.GetComponent<ArmorScript>().resistance = (int)objects[i][4];
                    break;
                    case 2 : gameObject = Instantiate(InventoryManager.Instance.MiddleArmor, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
                    gameObject.GetComponent<ArmorScript>().resistance = (int)objects[i][4];
                     break;
                    case 3 : gameObject = Instantiate(InventoryManager.Instance.BigArmor, new Vector2(objects[i][1], objects[i][2]), Quaternion.identity); 
                    gameObject.GetComponent<ArmorScript>().resistance = (int)objects[i][4];
                    break;
                    default: break; 
                }
                break; 
        }
        return gameObject; 
    }
    // public void Update(){
        // update += Time.deltaTime;
        // if (update > 1.0f)
        // {
        //     update = 0.0f; 
        //     switch(gameState){
        //         case GameStates.Day : Fire(); break; 
        //         case GameStates.Night:  ReverseFire(); break;
        //     }
            
        // }
        // if(Input.GetMouseButtonDown(0)){
        //     Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     Vector3Int gridpos = tileMap.WorldToCell(mousepos);
        //     TileBase clickedTile = tileMap.GetTile(gridpos);
        //     dataFromTiles[clickedTile].fire = field[gridpos.y/8][gridpos.x/8].ToListUpdate()[gridpos.y%8][gridpos.x%8].Fire;
        //     float fire = dataFromTiles[clickedTile].fire;
        //     print("At pos" + gridpos+ " there is a fire " + fire + " \n Cell position" + 
        //     field[gridpos.y/8][gridpos.x/8].ToListUpdate()[gridpos.y%8][gridpos.x%8].X + " " + 
        //     field[gridpos.y/8][gridpos.x/8].ToListUpdate()[gridpos.y%8][gridpos.x%8].Y);


        // }
        
    // }
    void Awake(){
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach(var data in tileDatas){
            foreach(var tile in data.tiles){
                dataFromTiles.Add(tile, data);
            }
        }
    }

    // private void Fire(){
    //     foreach (var list in field){
    //         foreach(var room in list){
    //             if(room.getX()!=-1&&room.getY()!=-1){
    //                 room.FireAlgo();
    //                 Visualizer.paintRoomUpdate(room);
    //             }
    //         }
    //     }
    // }
    // private void ReverseFire(){
    //     foreach (var list in field){
    //         foreach(var room in list){
    //             if(room.getX()!=-1&&room.getY()!=-1){
    //                 room.FireAlgoReverse();
    //                 Visualizer.paintRoomUpdate(room);
    //             }
    //         }
    //     }
    // }
    // private void Fire(){
    //     for(int i=0; i<40; i++){
    //         for(int j=0; j<40; j++){
    //             try{
    //                 dataFromTiles[tileMap.GetTile(new Vector3Int(i, j, 0))].fire*= ( 1.0f + (Random.Range(1, 10) / 100.0f));
    //                 dataFromTiles[tileMap.GetTile(new Vector3Int(i, j, 0))].fire+=(float)(Random.Range(1, 100)/100.0f);
    //             }
    //             catch (Exception){}
    //         }
    //     }
    // }

    
    // private void setMedKits(){
    //     foreach(var list in field){
    //         foreach(var room in list){
    //             if(room.getX()!=-1&&room.getY()!=1){
    //                 int randomX = Random.Range(1, 26);
    //                 int randomY=Random.Range(1, 4);
    //                 int hp = Random.Range(50, 250);
    //                 if(randomX <6){
    //                     try{
    //                         Visualizer.paintKit(new Vector2Int(room.getX()*8+randomX, room.getY()*8+randomY));
    //                         dataFromTiles[tileMap.GetTile(new Vector3Int(room.getX()*8+randomX, room.getY()*8+randomY, 0))].medKit=hp;
    //                     }
    //                     catch (Exception){}
    //                 }
    //             }
    //         }
    //     }
    // }
    // private void setKeys(){
    //     foreach(var list in field){
    //         foreach(var room in list){
    //             if(room.getX()!=-1&&room.getY()!=-1&&room.getKey()){
    //                 int randomX = Random.Range(1, 6);
    //                 int randomY=Random.Range(4, 6);
    //                 Visualizer.paintKey(new Vector2Int(room.getX()*8+randomX, room.getY()*8+randomY));
    //                 dataFromTiles[tileMap.GetTile(new Vector3Int(room.getX()*8+randomX, room.getY()*8+randomY, 0))].key=true;
    //             }
    //         }
    //     }
    // }
    // public float getTileFire(Vector3 pos){
    //     Vector3Int gridpos = tileMap.WorldToCell(pos); 
    //     TileBase Tile = tileMap.GetTile(gridpos);
    //     dataFromTiles[Tile].fire= field[gridpos.y/8][gridpos.x/8].ToListUpdate()[gridpos.y%8][gridpos.x%8].Fire;
    //     return field[gridpos.y/8][gridpos.x/8].ToListUpdate()[gridpos.y%8][gridpos.x%8].Fire;
    // }
    public float getTileFire(Vector3 pos){
        
        try{
            if(Field[(int)pos.y/11][(int)pos.x/11].hasFire) 
                return Field[(int)pos.y/11][(int)pos.x/11].getFire(new Vector2Int((int)pos.x%11, (int)pos.y%11));
            return 0;
        }catch (Exception){ return 0;}
    }
    // public int getTileKit(Vector3 pos){
    //     Vector3Int gridpos = tileMap.WorldToCell(pos); 
    //     TileBase Tile = tileMap.GetTile(gridpos);
        
    //     return dataFromTiles[Tile].medKit;
    // }
}
