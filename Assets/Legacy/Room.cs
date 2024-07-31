using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
public class Room {
    private Dictionary<CDirection, bool> directions;
    private List<CDirection> remainingDirections;
    private int x =-1;
    private int y=-1;
    private int distance;
    private bool hasKey; 
    
    private int randomInt;
    private int possibleDoors;
    private int doors; 
    private int randomY;
    private int randomX;
    private bool empty=false;
    private bool exit= false;
    private List<List<Cell>> result;
    //public KeySpawner keySpawner;
    
    public Room(){
        empty=true;
    }

    public Room(List<CDirection> directions) {
        this.directions=new Dictionary<CDirection, bool>();
        if (directions.Count==0) {
            throw new Exception("Directions cannot be empty for a room.");
        }
        foreach (CDirection d in directions){
            this.directions.Add(d, true);
        }
        this.remainingDirections = new List<CDirection>(directions);
        this.x = -1;
        this.y = -1;
        this.distance=-1;
        possibleDoors = directions.Count;
        doors = 0;
        hasKey = false;
    }

    public Room(Room r) {
        if (r.getDirections().Count==0) {
            throw new Exception("Directions cannot be empty for a room.");
        }
        this.directions = r.getDirectionToExit();
        this.x = r.getX();
        this.y = r.getY();
        this.remainingDirections = new List<CDirection>(r.getRemainnigDirections());
        possibleDoors = directions.Count;
        hasKey = r.getKey();
    }

    public List<CDirection> getDirections() {
        return new List<CDirection>(directions.Keys);
    }
    public Dictionary <CDirection, bool> getDirectionToExit(){
        return new Dictionary<CDirection, bool>(directions);
    }
    

    public void setDirections(List<CDirection> directions) {
        this.directions = new Dictionary<CDirection, bool>();
        foreach(CDirection d in directions){
            this.directions[d]= true;
        }
        possibleDoors = directions.Count;
        
    }

    public int getOpenings() {
        return remainingDirections.Count;
    }

    
    public String toString() {
        String sb = new String(" ");
        foreach(KeyValuePair<CDirection, bool> e in directions){
            sb+=(CardinalDirection.ToString(e.Key));
            sb+=("  ");
            sb+=(e.Value.ToString());
            sb+=(" - ");

        }
        if(hasKey) sb+=("K");
        if(exit) sb+=("X");
        return sb;
    }

    public int getX() {
        return x;
    }

    public int getY() {
        return y;
    }

    public void setCoords(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public void removeRemainingDirection(CDirection d){
        remainingDirections.Remove(d);
    }
    public List<CDirection> getRemainnigDirections(){
        return remainingDirections;
    }
    public void setDistance(int distance){
        this.distance=distance;
    }
    public int getDistance(){
        return distance;
    }
    public void addKey(){
        hasKey=true;
    }
    public bool getKey(){
        return hasKey;
    }
    public void addDoor(){
            List <CDirection>tempDirections = directions.Where(pair=> pair.Value == true)
            .ToDictionary(pair=>pair.Key, pair=>pair.Value).Keys.ToList();
            try{ 
                randomInt=Random.Range(0, tempDirections.Count);
            }catch(Exception){
                 randomInt=0;
                
            }
            directions[tempDirections[randomInt]]= false;
            possibleDoors--; 
            doors++;
    }
    public bool hasPlace(){
        if (possibleDoors > 0 && doors<2)  return true; 
        return false;
    }

    public List<List<Cell>> ToList(){

        if(!empty){
            int randomY = Random.Range(1, 6);
            int randomX = Random.Range(1, 6);

            result = new List<List<Cell>>();
            List <Cell> temp = new List<Cell>(); 
            for(int i =0 ; i<7; i++){
                temp.Add(new Wall(y*8, x*8+i));
            }
            temp.Add(null);
            if(directions.ContainsKey(CDirection.NORTH)){
                if(!directions[CDirection.NORTH]){
                    temp[3]= new Door(y*8, x*8+3, false);
                }
                else {
                    temp[3]= new Hall(y*8, x*8+3,0,  false);
                }
            }
            result.Add(temp);
            for(int i= 1; i<6; i++){
                temp = new List<Cell>(); 
                for(int j= 0; j<8; j++){
                    if(j==0 || j==6){
                        temp.Add(new Wall(y*8 + i, x*8 + j)); 
                    }
                    else if(j==7){
                        temp.Add(null);
                    }
                    else{
                        if(hasKey&&i==randomY&&j==randomX){
                            temp.Add(new Hall(y*8 + i, x*8 + j,0, true));
                            
                        }
                        else {
                            temp.Add(new Hall(y*8 + i, x*8 + j, 0, false));
                        }
                    }
                }
                
                if(i==3 && directions.ContainsKey(CDirection.WEST)){
                    if(!directions[CDirection.WEST]){
                        temp[0]=new Door(y*8+i, x*8, false);
                    }
                    else{
                        temp[0]= new Hall(y*8+i, x*8,0,  false);
                    }
                    //result.add(temp);
                }
                if(i==3 && directions.ContainsKey(CDirection.EAST)){
                    if(!directions[CDirection.EAST]){
                        temp[6]=new Door(y*8+i, x*8+6, false);
                    }
                    else{
                        temp[6]= new Hall(y*8+i, x*8+6,0,  false);
                    }
                    temp[7]= new Hall(y*8+i, x*8+7, 0, false);

                }
                if(i==2&&directions.ContainsKey(CDirection.EAST)){
                    temp[7]= new Wall(y*8+i, x*8+7);
                }
                if(i==4&&directions.ContainsKey(CDirection.EAST)){
                    temp[7] =new Wall(y*8+i, x*8+7);
                }
                result.Add(temp);
            }
            temp = new List<Cell>();
            for(int i =0 ; i<7; i++){
                temp.Add(new Wall(y*8+6, x*8+i));
            }
            temp.Add(null);
            if(directions.ContainsKey(CDirection.SOUTH)){
                if(!directions[CDirection.SOUTH]){
                    temp[3]= new Door(y*8+6, x*8+3, false);
                }
                else{
                    temp[3] =new Hall(y*8+6, x*8+3, 0,false);
                }
                
                
            }
            
            result.Add(temp);
            temp = new List<Cell>(Enumerable.Repeat<Cell>(null, 8).ToList());
            if(directions.ContainsKey(CDirection.SOUTH)){
                temp[2] =new Wall(y*8+7, x*8+2);
                temp[3] = new Hall(y*8+7, x*8+3, 0, false);
                temp[4] = new Wall(y*8+7, x*8+4);
            }
            result.Add(temp);
            
        }
        else{
            List<Cell> temp =  new List<Cell>(Enumerable.Repeat<Cell>(null, 8).ToList());
            result = new List<List<Cell>>(Enumerable.Repeat<List<Cell>> (temp, 8).ToList());
            
        }
        try{
            setFire();
            if(exit){
                setExit();
            }
            setMedKit();
        }
        catch(Exception ){}
        return result;
    }
    public List<List<Cell>> ToListUpdate(){
        return result;
    }

    private  void setFire(){
        float fire = 0.1f*Random.Range(5, 10);
        
        randomX = Random.Range(1, 11);
        randomY= Random.Range(1, 6);
        if(randomX<6){
            if(result[randomY][randomX].Key){
                result[randomY][randomX]= new Hall(y*8+randomY, x*8+randomX, fire, true);
            }
            else{
                result[randomY][randomX]= new Hall(y*8+randomY, x*8+randomX, fire);
            }
            
            
        }
    }
    public void setExit(bool exit){
        this.exit =exit;
    }
    private void setExit(){
        result[1][1] =  new Exit(y*8+1, x*8+1, 0);  
    }
    public void FireAlgo(){
        
        for(int i =0; i<7; i++){
            for(int j=0; j<7; j++){
                try{
                    if(result[i][j].Fire>0.0f){
                        result[i][j].Fire *= ( 1.0f + (Random.Range(1, 10) / 100.0f));
                        RoundFire(i, j);
                    }
                }catch (Exception){}
                //Debug.Log("Running");
            }
        }

    }
    private void setMedKit(){
        randomX = Random.Range(1, 26);
        randomY=Random.Range(1, 6);
        int hp = Random.Range(50, 250);
        if(randomX <6){
            try{
                ((Hall)result[randomY][randomX]).AddKit(hp);
            }
            catch(Exception ){}
        }
    }
    private void RoundFire(int y, int x){
        for(int i=y-1; i<=y+1; i++){
            for(int j=x-1; j<=x+1; j++){
                try{
                    result[i][j].Fire+=0.005f;
                }
                catch(Exception){}
            }
        }
    }
    public void FireAlgoReverse(){
        for(int i =0; i<7; i++){
            for(int j=0; j<7; j++){
                try{
                    if(result[i][j].Fire>0.0f){
                        result[i][j].Fire /= ( 1.0f + (Random.Range(1, 10) / 100.0f));
                        ReverseRoundFire(i, j);
                    }
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
                    result[i][j].Fire-=0.005f;
                }
                catch(Exception){}
            }
        }
    }


    // public void paintRoomFirst(Tilemap map, Tilemap secondary, Tilemap collisions,  Dictionary<CellType, TileBase>  tiles, Dictionary<TileBase, TileData> dataFromTiles){
    //     for(int i =0 ; i<7; i++){
    //         if(i !=3)
    //         paintSingleTile(collisions, tiles[CellType.Wall] , new Vector2Int( x*8+i, y*8));
    //     }
    //     if(directions.ContainsKey(CDirection.NORTH)){
    //         if(!directions[CDirection.NORTH]){
    //             paintSingleTile(collisions, tiles[CellType.Door], new Vector2Int(x*8+3, y*8));

    //         }
    //         else {
    //             //paintSingleTile(collisions, null, new Vector2Int(x*8+3, y*8));
    //             paintSingleTile(map, tiles[CellType.Hall0], new Vector2Int(x*8+3, y*8));
    //         }
    //     }
    //     else paintSingleTile(collisions, tiles[CellType.Wall], new Vector2Int(x*8+3, y*8));
    //     for(int i= 1; i<6; i++){
    //         for(int j= 0; j<7; j++){
    //             if(j==0 || j==6){
    //                 if(i!=3)
    //                 paintSingleTile(collisions, tiles[CellType.Wall], new Vector2Int(x*8 + j, y*8 + i));
    //             }
    //             else if(exit&& i==1&&j==1){
    //                 paintSingleTile(map, tiles[CellType.Exit], new Vector2Int(x*8 + j, y*8 + i));
    //             }
    //             else {
    //                 paintSingleTile(map, tiles[CellType.Hall0], new Vector2Int(x*8 + j, y*8 + i));
    //                 // if(hasKey&&i==randomY&&j==randomX){


    //                 //     paintSingleTile(secondary, tiles[CellType.Key], new Vector2Int(x*8+j, y*8 + i));
    //                 // }
    //             }
    //         }
    //     }
    //     if(directions.ContainsKey(CDirection.WEST)){
    //             if(!directions[CDirection.WEST]){

    //                 paintSingleTile(collisions, tiles[CellType.Door], new Vector2Int(x*8, y*8+3));
    //             }
    //             else{
    //                 //paintSingleTile(collisions, null, new Vector2Int(x*8, y*8+i));
    //                 paintSingleTile(map, tiles[CellType.Hall0], new Vector2Int(x*8, y*8+3));
    //             }
    //             //result.add(temp);
    //     }
    //     else paintSingleTile(collisions,tiles[CellType.Wall], new Vector2Int(x*8, y*8+3));

    //     if(directions.ContainsKey(CDirection.EAST)){
    //         paintSingleTile(collisions, tiles[CellType.Wall] , new Vector2Int( x*8+7, y*8+2));
    //         paintSingleTile(collisions, tiles[CellType.Wall] , new Vector2Int( x*8+7, y*8+4));
    //         if(!directions[CDirection.EAST]){
    //                 paintSingleTile(collisions, tiles[CellType.Door], new Vector2Int(x*8+6, y*8+3));
    //         }
    //         else{
    //             //paintSingleTile(collisions, null, new Vector2Int(x*8+6, y*8+i));
    //             paintSingleTile(map, tiles[CellType.Hall0], new Vector2Int(x*8+6, y*8+3));
    //         }

    //         paintSingleTile(map, tiles[CellType.Hall0], new Vector2Int(x*8+7, y*8+3));
    //     }
    //     else{
    //         paintSingleTile(collisions,tiles[CellType.Wall], new Vector2Int(x*8+6, y*8+3));
    //     }
    //     for(int i =0; i<=6; i++){
    //         if(i!=3){
    //             paintSingleTile(collisions,tiles[CellType.Wall], new Vector2Int(x*8+i, y*8+6));
    //         }
    //     }
    //     if(directions.ContainsKey(CDirection.SOUTH)){
    //         if(!directions[CDirection.SOUTH]){
    //             paintSingleTile(collisions, tiles[CellType.Door] , new Vector2Int( x*8+3, y*8+6));
    //         }
    //         else{

    //             paintSingleTile(map, tiles[CellType.Hall0] , new Vector2Int( x*8+3, y*8+6));
    //         }
    //         paintSingleTile(collisions, tiles[CellType.Wall] , new Vector2Int(x*8+2,  y*8+7));
    //         paintSingleTile(collisions, tiles[CellType.Wall] , new Vector2Int(x*8+4,  y*8+7));
    //         paintSingleTile(map, tiles[CellType.Hall0] , new Vector2Int(x*8+3,  y*8+7));
    //     }
    //     else paintSingleTile(collisions, tiles[CellType.Wall], new Vector2Int(x*8+3, y*8+6));

    // }
    // private void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    // {
    //     var tilePosition = tilemap.WorldToCell((Vector3Int)position);
    //     tilemap.SetTile(tilePosition, tile);
    // }

}
public enum CellType{
    Hall0, 
    Hall1, 
    Hall2, 
    Hall3, 
    Exit, 
    Wall, 
    Door, 
    Key,
    MedKit
}
