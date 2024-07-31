using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;
public class TerrainGenerator{
    private Dictionary<int, List<Room>> distances;
    private List<Room> bottom; 
    private List<Room> right;
    private List<Room> left;
    private List<Room> up;
    private List<List<Room>> field;
    private List<Room> openings;
    private CDirection nextDir;
    private List<CDirection> directions;
    private int randomInt;
    private Room randomRoom;
    private int roomIdx;
    private Room currentRoom;
    private int currentX;
    private int currentY;
    private int goalX;
    private int goalY;
    private List<List<CDirection>> allPosibilities;
    private int randomInt2;
    private int width; 
    private int height; 
    private int spawnX;
    private int spawnY;
    public TerrainGenerator(int width, int height, int spawnX,int spawnY){
        this.height=height;
        this.width=width;
        this.spawnX=spawnX;
        this.spawnY=spawnY;
        distances = new Dictionary<int, List<Room>>();
        allPosibilities = new List<List<CDirection>>();
        bottom = new List<Room>();
        up = new List<Room>();
        right = new List<Room>();
        left = new List<Room>();
        setAllPosibilities();
        populate();
        field = new List<List<Room>>();
        for(int i = 0; i<height; i++){
            field.Add(Enumerable.Repeat<Room>(null, width).ToList());
        }
        directions= new List<CDirection>();
        directions.Add(CDirection.NORTH); directions.Add(CDirection.SOUTH); directions.Add(CDirection.WEST); directions.Add(CDirection.EAST);  
        field[spawnY][spawnX]= new Room(directions);
        field[spawnY][spawnX].setDistance(0);
        distances.Add(field[spawnY][spawnX].getDistance(), new List<Room>() );
        distances[0].Add(field[spawnY][spawnX]);
        openings=new List<Room>();
        openings.Add(field[spawnY][spawnX]);
        field[spawnY][spawnX].setCoords(spawnX, spawnY);
        
        
        currentX=spawnX;
        currentY=spawnY;
        generate();
    
        setDoors();
        setExit();
        left= null;
        right=null; 
        up=null;
        bottom=null;
        allPosibilities=null;
        currentRoom=null;
        randomRoom=null;


    }
    private void setAllPosibilities(){
        List<CDirection> temp = new List<CDirection>();
        temp.Add(CDirection.NORTH); temp.Add(CDirection.SOUTH); temp.Add(CDirection.WEST); temp.Add(CDirection.EAST);
        allPosibilities.Add(new List<CDirection>(temp));
        
        temp.RemoveAt(3);
        
        temp.RemoveAt(2);
        
        allPosibilities.Add(new List<CDirection>(temp));
        
        temp.RemoveAt(1);
        
        temp.Add(CDirection.WEST);
        
        allPosibilities.Add(new List<CDirection>(temp));
        temp.RemoveAt(1);
        temp.Add(CDirection.EAST);
        allPosibilities.Add(new List<CDirection>(temp));
        temp.RemoveAt(1);
        temp.RemoveAt(0);
        temp.Add(CDirection.SOUTH);
        temp.Add(CDirection.WEST);
        allPosibilities.Add(new List<CDirection>(temp));
        temp.RemoveAt(1);
        temp.Add(CDirection.EAST);
        allPosibilities.Add(new List<CDirection>(temp));
        temp.RemoveAt(1);
        temp.RemoveAt(0);
        temp.Add(CDirection.WEST);
        temp.Add(CDirection.EAST);
        allPosibilities.Add(new List<CDirection>(temp));
        temp.RemoveAt(1);
        allPosibilities.Add(new List<CDirection>(temp));
        temp.RemoveAt(0);
        temp.Add(CDirection.EAST);
        allPosibilities.Add(new List<CDirection>(temp));
        temp.RemoveAt(0);
        temp.Add(CDirection.SOUTH);
        allPosibilities.Add(new List<CDirection>(temp));
        temp.RemoveAt(0);
        temp.Add(CDirection.NORTH);
        allPosibilities.Add(new List<CDirection>(temp));

    }
    private void populate(){
        for(int i= 0; i<4; i++){
            up.Add(new Room(allPosibilities[1]));
            up.Add(new Room(allPosibilities[2]));
            up.Add(new Room(allPosibilities[3]));
            bottom.Add(new Room(allPosibilities[1]));
            bottom.Add(new Room(allPosibilities[4]));
            bottom.Add(new Room(allPosibilities[5]));
            right.Add(new Room(allPosibilities[3]));
            right.Add(new Room(allPosibilities[5]));
            right.Add(new Room(allPosibilities[6]));
            left.Add(new Room(allPosibilities[2]));
            left.Add(new Room(allPosibilities[4]));
            left.Add(new Room(allPosibilities[6]));
        }
        
        up.Add(new Room(allPosibilities[10]));
        bottom.Add(new Room(allPosibilities[9]));
        right.Add(new Room(allPosibilities[8]));
        left.Add(new Room(allPosibilities[7]));
        
    }
    private void setCase(){
        if(goalY>=0&&goalY<height&&goalX<width&&goalX>=0&&field[goalY][goalX]==null){
            field[goalY][goalX] = new Room(randomRoom);
            field[goalY][goalX].setCoords(goalX, goalY);
            field[goalY][goalX].setDistance(1+field[currentY][currentX].getDistance());
            field[goalY][goalX].removeRemainingDirection(CardinalDirection.getOposite(nextDir));
            field[currentY][currentX].removeRemainingDirection(nextDir);
            openings[randomInt].removeRemainingDirection(nextDir);
            if(distances.Count<= field[goalY][goalX].getDistance()){
                distances.Add(field[goalY][goalX].getDistance(), new List<Room>());
                distances[field[goalY][goalX].getDistance()].Add(field[goalY][goalX]);
            }
            else{
                distances[field[goalY][goalX].getDistance()].Add(field[goalY][goalX]);
            }
            if(field[goalY][goalX].getOpenings()>=1){
                openings.Add(field[goalY][goalX]);
            }
            if(field[currentY][currentX].getOpenings()== 0){
                openings.RemoveAt(randomInt);
            }
        }
        else if(goalY>=0&&goalY<height&&goalX<width&&goalX>=0&&field[goalY][goalX].getDirections().Contains(CardinalDirection.getOposite(nextDir))){
            field[currentY][currentX].setDistance(System.Math.Min(field[currentY][currentX].getDistance(), field[goalY][goalX].getDistance()+1));
            openings[randomInt].setDistance(Math.Min(field[currentY][currentX].getDistance(), field[goalY][goalX].getDistance()+1));
            openings[openings.IndexOf(field[goalY][goalX])].removeRemainingDirection(CardinalDirection.getOposite(nextDir));
            field[goalY][goalX].removeRemainingDirection(CardinalDirection.getOposite(nextDir));
            openings[openings.IndexOf(field[goalY][goalX])].setDistance(System.Math.Min(field[goalY][goalX].getDistance(), field[currentY][currentX].getDistance()+1));
            field[goalY][goalX].setDistance(System.Math.Min(field[goalY][goalX].getDistance(), field[currentY][currentX].getDistance()+1));
            field[currentY][currentX].removeRemainingDirection(nextDir);
            openings[randomInt].removeRemainingDirection(nextDir);
            if(field[currentY][currentX].getOpenings()== 0){
                openings.RemoveAt(randomInt);
            }
            if(field[goalY][goalX].getOpenings()==0){
                openings.Remove(field[goalY][goalX]);
            }
        }
        else{
            directions=new List<CDirection>(currentRoom.getDirections());
            directions.RemoveAt(directions.IndexOf(nextDir));
            field[currentY][currentX].setDirections(new List<CDirection>(directions));
            openings[randomInt].setDirections(new List<CDirection>(directions));
            openings[randomInt].removeRemainingDirection(nextDir);
            field[currentY][currentX].removeRemainingDirection(nextDir);
            if(field[currentY][currentX].getOpenings()==0){
                openings.RemoveAt(randomInt);
            }
        }
    }
    private void generate(){
        if(openings.Count==0) return;
        try{
            randomInt = Random.Range(0, openings.Count);
        }catch (Exception ){
            randomInt=0;
        }
        currentRoom = openings[randomInt];
        currentX= currentRoom.getX(); 
        currentY= currentRoom.getY();
        directions= new List<CDirection>(currentRoom.getRemainnigDirections());
        
        try{
        randomInt2 = Random.Range(0, directions.Count);
        }catch(Exception ){
            randomInt2=0;
        }
        nextDir = directions[randomInt2];
        roomIdx = Random.Range(0, up.Count);
        
        switch (nextDir){
            case CDirection.NORTH: 
            
            randomRoom= bottom[roomIdx]; 
            
            goalY= currentY-1;
            goalX= currentX;
            break;
            case CDirection.SOUTH:
            
            randomRoom= up[roomIdx];
            
            goalY= currentY+1;
            goalX= currentX;
            break;
            case CDirection.EAST:
            
            randomRoom= left[roomIdx];
            
            goalY= currentY;
            goalX= currentX+1;
            break;
            case CDirection.WEST: 
            
            randomRoom= right[roomIdx];
            
            goalY= currentY;
            goalX= currentX-1;
            break;

        }
        setCase();
        generate();





    }
    private void setDoors(){
        List<int> nbDoors = new List<int>();
        foreach(KeyValuePair<int, List<Room>> ele in distances){
            if(ele.Key>1){
                try{
                    nbDoors.Add(Random.Range(0, (int)Math.Ceiling((decimal)(ele.Value.Count)/2)-1)+1);
                }
                catch(Exception ){
                    nbDoors.Add(1);
                }
                List<Room> temp = new List<Room>(ele.Value);
                for(int i = nbDoors[(ele.Key-2)] ; i>0; i--){
                    randomInt = Random.Range(0, temp.Count);
                    while(!temp[randomInt].hasPlace()){
                        temp.RemoveAt(randomInt);
                        randomInt = Random.Range(0, temp.Count); 
                    }
                    field[temp[randomInt].getY()][temp[randomInt].getX()].addDoor();
                    temp[randomInt]= field[temp[randomInt].getY()][temp[randomInt].getX()];  
                }
            }
        }
        foreach(KeyValuePair<int, List<Room>> ele in distances){
            if(ele.Key>0&&ele.Key<distances.Count-1){
                List<Room> temp = new List<Room>(ele.Value);
                for(int i = nbDoors[ele.Key-1]; i>0; i--){
                    randomInt= Random.Range(0, temp.Count); 
                    field[temp[randomInt].getY()][temp[randomInt].getX()].addKey();
                    temp.RemoveAt(randomInt);
                }
            }
        }
        // for(Integer i: nbDoors){
        //     System.out.println(i.toString());
        // }
        // for(Integer i:nbDoors){
        //     System.out.print(i.toString()+ " ");
        // }
        // System.out.println("");
    }
    public void setExit(){
        List<Room> temp= distances[distances.Count-1];
        int r = Random.Range(0, temp.Count);
        field[temp[r].getY()][temp[r].getX()].setExit(true);
    }
    public List<List<Room>> getField() {
        for(int i = 0; i<height; i++){
            for(int j=0; j<width; j++){
                if(field[i][j]==null) field[i][j]=new Room();
            }
        }
        return field;
    }
    public static List<List<Room>> getRooms(){
        TerrainGenerator temp = new TerrainGenerator(5, 5, 2, 2);
        return temp.getField();
    }
    public void printField(){
        for(int i=0; i<6; i++){
            Debug.Log("||");
            for(int j=0; j<6; j++){
                try{
                    Debug.Log(field[i][j].toString());
                    Debug.Log(" "+field[i][j].getDistance());
                }
                catch(Exception ){
                    Debug.Log(" -- ");
                }
                Debug.Log("||"); 
            }
            Debug.Log("\n");
        }
        
    }
    
    
    
}


