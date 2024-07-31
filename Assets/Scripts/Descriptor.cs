using System;
// using System.Collections;
using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEditor;
using UnityEngine;
// using UnityEngine.SocialPlatforms;
// using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
public class Descriptor 
{
    // Start is called before the first frame update
    public GameObject grid{get; private set; }
    public int X{get; private set; }
    public int Y{get; private set;}
    private DoorLocation doors;
    //public Dictionary<CDirection, bool> dirs{get; private set;}
    private GameObject door; 
    private GameObject key;
    public int radius{get; set;}
    public int openings{get; private set;}
    private List<CDirection> dirs;
    public List<CDirection> directions{get; set;}
    public int possibleDoors{get; private set;}
    public bool hasFire{get; private set;}
    private FireScript fireScript;
    public float ForSave=0; 
    public Descriptor(){
        openings=0;
        grid=null;
        X=-1; 
        Y=-1; 
        directions=null;
    }
    
    public Descriptor(GameObject grid, int X, int Y){
        possibleDoors=2;
        openings=0;
        // door = GameObject.Find("Door");
        //MonoBehaviour.Instantiate(door, Vector2.zero, Quaternion.identity);
        // key = GameObject.Find("Key");
          
        //this.grid=grid;
        //if(this.grid.GetComponent<DoorLocation>()) Debug.Log(true);
        this.grid = MonoBehaviour.Instantiate(grid, new Vector2(X * 11, Y * 11), Quaternion.identity);
        //foreach(var pair in this.grid.GetComponent<DoorLocation>().doors) Debug.Log(true);
        doors = this.grid.GetComponent<DoorLocation>();
        doors.X=X; 
        doors.Y=Y;
        this.X=X; 
        this.Y=Y; 
        // dirs= new Dictionary<CDirection, bool>();
        directions= new List<CDirection>();
        dirs= new List<CDirection>();
        foreach(var pair in doors.doors){
            //Debug.Log(true);
            // dirs.Add(pair.Key, true);
            directions.Add(pair.Key);
            dirs.Add(pair.Key);
            openings++;
            
        }
        if(openings==1) 
            possibleDoors=1;
        foreach(var dir in dirs){
            switch(dir){
            case CDirection.NORTH: ForSave+=0.1f; break; 
            case CDirection.WEST: ForSave+=0.01f; break; 
            case CDirection.SOUTH: ForSave+= 0.001f; break; 
            case CDirection.EAST: ForSave += 0.0001f; break; 
            }
        }
        setFire();
        setKits();
    }
    public Descriptor(GameObject grid, int X, int Y, int radius):this(grid, X, Y){
        this.radius=radius; 
        door=InventoryManager.Instance.door; 
        key=InventoryManager.Instance.key;
    }
    public Descriptor(GameObject grid, int X, int Y, int radius, float ForSave) 
    : this(grid, X, Y, radius){
        this.ForSave = ForSave; 
    }
    public Descriptor(GameObject grid, int X, int Y, int radius, int ForSave):
    this(grid, X, Y, radius){
        this.ForSave+=ForSave; 
    }
    public void RemoveDir(CDirection direction){
        
            // dirs[direction]=false;
            // if(!directions.Contains(direction)) Debug.Log(false); 
            directions.Remove(direction);
            dirs.Remove(direction);
            openings--;
            //if(openings<2) 
            if(dirs.Count<2) possibleDoors--;
            // Debug.Log(direction +" " + ForSave); 
            switch(direction){
            case CDirection.NORTH: ForSave-=0.1f; break; 
            case CDirection.WEST: ForSave-=0.01f; break; 
            case CDirection.SOUTH: ForSave-= 0.001f; break; 
            case CDirection.EAST: ForSave -= 0.0001f; break; 
            }
            // Debug.Log(ForSave); 
            // switch(direction){
            //     case CDirection.SOUTH:
            //     //MonoBehaviour.Destroy(grid.GetComponent("CorridorDown"));
            //     break;
            //     case CDirection.WEST:
            //     //MonoBehaviour.Destroy(grid.GetComponent("CorrdiorRight"));
            //     break;
            //     case CDirection.NORTH: break;
            //     case CDirection.EAST: break;
            //    // default:break;
            // }
        
        grid.GetComponent<DoorLocation>().paintWall(direction, X, Y);
    }
    private void RemoveDirPrivate(CDirection direction){
        directions.Remove(direction);
        dirs.Remove(direction);
        openings--;
        //if(openings<2) 
        if(dirs.Count<2) possibleDoors--;
        grid.GetComponent<DoorLocation>().paintWall(direction, X, Y);
    }
    public void RemoveDirTemp(CDirection direction){
        directions.Remove(direction);
        openings--;

    }
    public void AddDoor(){
            int random = Random.Range(0, dirs.Count);
            //openings--;
            possibleDoors--;
            GameObject gameObject = MonoBehaviour.Instantiate(door, new Vector2(X*11+0.5f, Y*11+0.5f) + doors.doors[dirs[random]], Quaternion.identity);
            gameObject.GetComponent<DoorController>().cDirection = dirs[random];
            dirs.RemoveAt(random);

        // Instantiate(door, new Vector2(X*8, Y*8) + doors.doors[direction], Quaternion.identity);
    }
    public bool hasPlace(){
        if(possibleDoors>0) return true;
        return false;
    }
    public void AddKey(){
        MonoBehaviour.Instantiate(key, new Vector2(X*11 + 4.5f, Y*11+4.5f), Quaternion.identity);
    }
    public void SetExit(){

    }
    public void setFire(){
        hasFire = doors.hasFire;
        fireScript= grid.GetComponent<FireScript>();
    }
    public float getFire(Vector2Int pos){
        return fireScript.getTileFire(pos);
    }
    private void setKits(){
        if(Random.Range(0, 5)==0){
            MonoBehaviour.Instantiate(InventoryManager.Instance.MedKit, new Vector2(X*11+6.5f, Y*11+6.5f), Quaternion.identity);
        }
    }
    public void RemoveDirs(){
        // Debug.Log(ForSave-Mathf.Floor(ForSave)); 
        // Debug.Log(Mathf.Round((ForSave-Mathf.Floor(ForSave))*10));
        int n = (int)Mathf.Round((ForSave - Mathf.Floor(ForSave))*10f);
        // Debug.Log(Mathf.Ceil((ForSave- Mathf.Floor(ForSave))*10)); 
        int w =  (int)Mathf.Round((ForSave - Mathf.Floor(ForSave) - n*0.1f)*100f);
        int s = (int)Mathf.Round((ForSave - Mathf.Floor(ForSave)- 0.1f*n - 0.01f*w)*1000f);
        int e = (int)Mathf.Round((ForSave - Mathf.Floor(ForSave)-0.1f*n-0.01f*w-0.001f*s)*10000f);
        // Debug.Log(n+" "+w+" "+s+" "+e);
    
        if(n==0&&dirs.Contains(CDirection.NORTH))
            
                RemoveDirPrivate(CDirection.NORTH); 
        if(w==0&&dirs.Contains(CDirection.WEST))
            
                RemoveDirPrivate(CDirection.WEST); 
        if(s==0&&dirs.Contains(CDirection.SOUTH))
            
                RemoveDirPrivate(CDirection.SOUTH); 
        if(e==0&&dirs.Contains(CDirection.EAST))
            
                RemoveDirPrivate(CDirection.EAST); 
        // if(w==0)
        //     try{
        //         RemoveDirPrivate(CDirection.WEST); 
        //     }catch(Exception){}
        // if(s==0)
        //     try{
        //         RemoveDirPrivate(CDirection.SOUTH); 
        //     }catch(Exception){}
        // if(e==0)
        //     try{
        //         RemoveDirPrivate(CDirection.EAST); 
        //     }catch(Exception){}

    }

    
}
