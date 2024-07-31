using System;
// using System.Collections;
using System.Collections.Generic;
using System.Linq;
// using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
// using UnityEditor.MemoryProfiler;

// using System.Runtime.CompilerServices;
// using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
public class TerrainGenerator2{
    
    private Rooms rooms;
    // private Descriptor descriptor;
    private Dictionary<int, List<Descriptor>> distances;
    // private List<Room> bottom; 
    // private List<Room> right;
    // private List<Room> left;
    // private List<Room> up;
    private List<List<Descriptor>> field;
    private List<Vector2> openings;
    private CDirection nextDir;
    private List<CDirection> directions;
    private int randomInt;
    //private Descriptor randomRoom;
    private int roomIdx;
    private Descriptor currentRoom;
    private int currentX;
    private int currentY;
    private int goalX;
    private int goalY;
    //private List<List<CDirection>> allPosibilities;
    private int randomInt2;
    private int width; 
    private int height; 
    // private GameObject door; 
    // private GameObject key;
    // private int spawnX;
    // private int spawnY;
    private GameObject chosenGrid;
    // public static float[][] doors = new float[]; 
    public TerrainGenerator2(int width, int height, int spawnX,int spawnY, Rooms rooms, GameObject door, GameObject key){
        // this.key=key; 
        // this.door=door;
        this.rooms=rooms;
        this.height=height;
        this.width=width;
        // this.spawnX=spawnX;
        // this.spawnY=spawnY;
        distances = new Dictionary<int, List<Descriptor>>();
        //allPosibilities = new List<List<CDirection>>();
        // bottom = new List<Room>();
        // up = new List<Room>();
        // right = new List<Room>();
        // left = new List<Room>();
        // setAllPosibilities();
        // populate();
        field = new List<List<Descriptor>>();
        for(int i = 0; i<height; i++){
            field.Add(Enumerable.Repeat<Descriptor>(null, width).ToList());
        }
        directions= new List<CDirection>{CDirection.NORTH, CDirection.SOUTH, CDirection.WEST, CDirection.EAST};
        // directions.Add(CDirection.NORTH); directions.Add(CDirection.SOUTH); directions.Add(CDirection.WEST); directions.Add(CDirection.EAST); 
        //Instantiate(Rooms.Instance.N[0][0], new Vector2(spawnX*8, spawnY*8), Quaternion.identity); 
        field[spawnY][spawnX]= new Descriptor(this.rooms.N[0][0], spawnX, spawnY, 0, 100.1111f);
        distances.Add(0, new List<Descriptor>{field[spawnY][spawnX]} );
        //distances[0].Add(field[spawnY][spawnX]);
        openings=new List<Vector2>{new Vector2(spawnX, spawnY)};
        
        
        currentX=spawnX;
        currentY=spawnY;
        generate();
    
        setDoors();
        setExit();
        // left= null;
        // right=null; 
        // up=null;
        // bottom=null;
        //allPosibilities=null;
        currentRoom=null;
        //randomRoom=null;


    }
    public TerrainGenerator2(int width, int height, float[][] map, Rooms rooms){
        //dir-rad-idx
        this.rooms=rooms; 
        this.height=height; 
        this.width=width; 
        // field = new List<List<Descriptor>>(); 
        field = new List<List<Descriptor>>();
        for(int i = 0; i<height; i++){
            field.Add(Enumerable.Repeat<Descriptor>(null, width).ToList());
        }
        int dir; 
        int rad; 
        int idx; 
        // CDirection dir; 
        for(int i =0; i<height; i++){
            for(int j=0; j<width; j++){
                // Debug.Log(map[i][j]); 
                if(map[i][j]!=0)
                {

                    dir = (int)map[i][j]/100; 
                    rad = (int)map[i][j]/10-dir*10; 
                    idx = (int)map[i][j]-dir*100-rad*10; 
                    // Debug.Log(dir+" "+rad+" "+idx);
                    switch(dir){
                        case 1: 
                        field[i][j] =  new Descriptor(rooms.N[rad][idx], j, i, rad, map[i][j]);
        
                        break; 
                        case 2:
                        field[i][j] =  new Descriptor(rooms.W[rad][idx], j, i, rad, map[i][j]);
                        break; 
                        case 3:
                        field[i][j] =  new Descriptor(rooms.S[rad][idx], j, i, rad, map[i][j]);
                        break; 
                        case 4:
                        field[i][j] =  new Descriptor(rooms.E[rad][idx], j, i, rad, map[i][j]);
                        break; 
                        default : return; 
                    }
                    field[i][j].RemoveDirs(); 
                    
                }
            }
        }
        SetDoorsAfterSave();
    }
    private void SetDoorsAfterSave(){
        for(int i =0; i<LoadedData.mapData.doorsArray.Length; i++){
            GameObject gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.door, new Vector2(LoadedData.mapData.doorsArray[i][0], LoadedData.mapData.doorsArray[i][1]), Quaternion.identity);
            if(LoadedData.mapData.doorsArray[i][2]==1){
                gameObject.GetComponent<DoorController>().OpenAfterSave();
            }
        }
    }
    private void setCase(){
        if(goalY>=0&&goalY<height&&goalX<width&&goalX>=0&&field[goalY][goalX]==null){
            int ForSave; 
            if(field[currentY][currentX].radius>5) {
                ForSave = 100*(int)CardinalDirection.getOposite(nextDir) + 
            50
            + roomIdx
            ; 
            }
            else{
                ForSave = 100*(int)CardinalDirection.getOposite(nextDir) + 
            10*field[currentY][currentX].radius
            + roomIdx
            ; 
            }
            field[goalY][goalX] = new Descriptor(chosenGrid, goalX, goalY, 1+field[currentY][currentX].radius, ForSave);
            // field[goalY][goalX].setCoords(goalX, goalY);
            // field[goalY][goalX].setDistance(1+field[currentY][currentX].getDistance());
            // Debug.Log(field[goalY][goalX].directions.Count);
            field[goalY][goalX].RemoveDirTemp(CardinalDirection.getOposite(nextDir));
            field[currentY][currentX].RemoveDirTemp(nextDir);
            
            if(distances.Count<= field[goalY][goalX].radius){
                distances.Add(field[goalY][goalX].radius, new List<Descriptor>{field[goalY][goalX]});
            }
            else{
                distances[field[goalY][goalX].radius].Add(field[goalY][goalX]);
            }
            if(field[goalY][goalX].openings>=1){
                openings.Add(new Vector2(goalX, goalY));
            }
            if(field[currentY][currentX].openings== 0){
                openings.Remove(new Vector2(currentX, currentY));
            }
            // Debug.Log(field[goalY][goalX].directions.Count);
        }
        else if(goalY>=0&&goalY<height&&goalX<width&&goalX>=0&&field[goalY][goalX].directions.Contains(CardinalDirection.getOposite(nextDir))){
            field[currentY][currentX].radius = Math.Min(field[currentY][currentX].radius, field[goalY][goalX].radius+1);
            // openings[randomInt].radius = Math.Min(field[currentY][currentX].radius, field[goalY][goalX].radius+1);
            // openings[openings.IndexOf(field[goalY][goalX])].RemoveDir(CardinalDirection.getOposite(nextDir));
            //if(!openings.Contains(new Vector2(goalX, goalY))) Debug.Log("WTF");
            // int idx = openings.IndexOf(new Vector2(goalX, goalY));
            field[goalY][goalX].RemoveDirTemp(CardinalDirection.getOposite(nextDir));
            
            // openings[openings.IndexOf(field[goalY][goalX])].radius = System.Math.Min(field[goalY][goalX].radius, field[currentY][currentX].radius+1);
            field[goalY][goalX].radius= Math.Min(field[goalY][goalX].radius, field[currentY][currentX].radius+1);
            field[currentY][currentX].RemoveDirTemp(nextDir);
            // openings[idx]= field[goalY][goalX];
            // openings[randomInt]= field[currentY][currentX];
            // openings[randomInt].RemoveDir(nextDir);
            if(field[currentY][currentX].openings== 0){
                openings.Remove(new Vector2(currentX, currentY));
                
            }
            if(field[goalY][goalX].openings==0){
                // openings.RemoveAt(idx);//idx-1!!
                openings.Remove(new Vector2(goalX, goalY));
            }
        }
        else{
            // directions=new List<CDirection>(currentRoom.directions);
            // directions.RemoveAt(directions.IndexOf(nextDir));
            // field[currentY][currentX].directions = new List<CDirection>(directions);
            // openings[randomInt].directions = new List<CDirection>(directions);
            //openings[randomInt].RemoveDir(nextDir);
            field[currentY][currentX].RemoveDir(nextDir);
            // openings[randomInt]=field[currentY][currentX];
            if(field[currentY][currentX].openings==0){
                openings.Remove(new Vector2(currentX, currentY));
            }
        }
    }
    private void generate(){
        if(openings.Count==0) return;
        //try{
        randomInt = Random.Range(0, openings.Count);
        // }catch (Exception ){
        //     randomInt=0;
        // }
        currentRoom = field[(int)openings[randomInt].y][(int)openings[randomInt].x];
        currentX= (int)openings[randomInt].x; 
        currentY= (int)openings[randomInt].y;
        directions= currentRoom.directions;
        
        //try{
        randomInt2 = Random.Range(0, directions.Count);
        // }catch(Exception ){
        //     randomInt2=0;
        // }
        nextDir = directions[randomInt2];
        
        
        switch (nextDir){
            case CDirection.SOUTH: 
            if(currentRoom.radius<5){
                roomIdx = Random.Range(0, rooms.N[currentRoom.radius].Count);
                chosenGrid = rooms.N[currentRoom.radius][roomIdx];
            }
            else{
                roomIdx = Random.Range(0, rooms.N[5].Count);
                chosenGrid = rooms.N[5][roomIdx];
            }
            
            
            goalY= currentY-1;
            goalX= currentX;
            break;
            case CDirection.NORTH:
            
            if(currentRoom.radius<5){
                roomIdx = Random.Range(0, rooms.S[currentRoom.radius].Count);
                chosenGrid = rooms.S[currentRoom.radius][roomIdx];
            }
            else{
                roomIdx = Random.Range(0, rooms.S[5].Count);
                chosenGrid = rooms.S[5][roomIdx];
            }
            
            goalY= currentY+1;
            goalX= currentX;
            break;
            case CDirection.WEST:
            
            if(currentRoom.radius<5){
                roomIdx = Random.Range(0, rooms.E[currentRoom.radius].Count);
                chosenGrid = rooms.E[currentRoom.radius][roomIdx];
            }
            else{
                roomIdx = Random.Range(0, rooms.E[5].Count);
                chosenGrid = rooms.E[5][roomIdx];
            }
            
            goalY= currentY;
            goalX= currentX+1;
            break;
            case CDirection.EAST: 
            
            if(currentRoom.radius<5){
                roomIdx = Random.Range(0, rooms.W[currentRoom.radius].Count);
                chosenGrid = rooms.W[currentRoom.radius][roomIdx];
            }
            else{
                roomIdx = Random.Range(0, rooms.W[5].Count);
                chosenGrid = rooms.W[5][roomIdx];
            }
            
            goalY= currentY;
            goalX= currentX-1;
            break;

        }
        setCase();
        generate();





    }
    private void setDoors(){
        List<int> nbDoors = new List<int>();
        foreach(KeyValuePair<int, List<Descriptor>> ele in distances){
            if(ele.Key>1){
                try{
                    nbDoors.Add(Random.Range(0, (int)Math.Ceiling((decimal)ele.Value.Count/2)-1)+1);
                }
                catch(Exception ){
                    nbDoors.Add(1);
                }
                List<Descriptor> temp = new List<Descriptor>(ele.Value);
                for(int i = nbDoors[ele.Key-2] ; i>0; i--){
                    randomInt = Random.Range(0, temp.Count);
                    while(!temp[randomInt].hasPlace()){
                        temp.RemoveAt(randomInt);
                        randomInt = Random.Range(0, temp.Count); 
                    }
                    field[temp[randomInt].Y][temp[randomInt].X].AddDoor();
                    temp[randomInt]= field[temp[randomInt].Y][temp[randomInt].X];  
                }
            }
        }
        foreach(KeyValuePair<int, List<Descriptor>> ele in distances){
            if(ele.Key>0&&ele.Key<distances.Count-1){
                List<Descriptor> temp = new List<Descriptor>(ele.Value);
                for(int i = nbDoors[ele.Key-1]; i>0; i--){
                    randomInt= Random.Range(0, temp.Count); 
                    field[temp[randomInt].Y][temp[randomInt].X].AddKey();
                    temp.RemoveAt(randomInt);
                }
            }
        }
        
    }
    public void setExit(){
        List<Descriptor> temp= distances[distances.Count-1];
        int r = Random.Range(0, temp.Count);
        field[temp[r].Y][temp[r].X].SetExit();
    }
    public List<List<Descriptor>> getField() {
        for(int i = 0; i<height; i++){
            for(int j=0; j<width; j++){
                if(field[i][j]==null) field[i][j]=new Descriptor();
            }
        }
        return field;
    }
    // public static List<List<Descriptor>> getRooms(GameObject door, GameObject key, Rooms rooms){
    //     TerrainGenerator2 temp = new TerrainGenerator2(10, 10, 5, 5, rooms, door, key);
    //     return temp.getField();
    // }
    
    public float[][] SaveField(){
        float[][] res = new float[height][]; 
        int i =0; 
        foreach(var list in field){
            res[i] = new float[width]; 
            foreach(var elem in list){
                try{
                    // Debug.Log(elem.Y+ " " + elem.X);
                    res[elem.Y][elem.X] = elem.ForSave; 
                }
                catch(IndexOutOfRangeException) {}
            }
            i++; 
        }
        return res; 
    }
    
    
}

