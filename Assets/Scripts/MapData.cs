// using System.Collections;
// using System.Collections.Generic;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class MapData 
{
    public float [][] rooms;
    public static List<List<float>> doors = new List<List<float>>(); //value digit 1 = NSWE one of them 1234, digit 2 = int in the dict, digit 3 = position in list, 
    // after coma values represent the dirs 
    private static int idx=0; 
    public float[][] doorsArray; 
    public MapData(TerrainGenerator2 terrain){
        rooms = terrain.SaveField(); 
        doorsArray= new float[doors.Count][];
        int j =0; 
        foreach(var list in doors){
            float[] temp = new float[4];
            for(int i =0; i<4; i++){
                temp[i] =  list[i];
            }
            doorsArray[j] = temp; 
            j++; 
        }
    }
    public static int Add(GameObject gameObject, CDirection cDirection){
        doors.Add(new List<float>{gameObject.transform.position.x, gameObject.transform.position.y, 0, (int)cDirection});

        return idx++; 
    }
    
    
}
