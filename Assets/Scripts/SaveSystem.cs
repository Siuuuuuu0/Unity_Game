using UnityEngine; 
using System.IO; 
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem 
{
    public static void SavePlayer(GameObject Player){
        BinaryFormatter formatter = new BinaryFormatter(); 
        string path = Application.persistentDataPath + "/player.bin"; 
        FileStream fileStream = new FileStream(path, FileMode.Create); 
        PlayerData data = new PlayerData(Player); 
        formatter.Serialize(fileStream, data); 
        fileStream.Close(); 
    }
    public static PlayerData LoadPlayer(){
        string path = Application.persistentDataPath + "/player.bin"; 
        if(File.Exists(path)){
            BinaryFormatter formatter= new BinaryFormatter(); 
            FileStream fileStream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(fileStream) as PlayerData; 
            fileStream.Close();
            return data; 
        }
        else{
            Debug.LogError("File not found"); 
            return null; 
        }
    }
    public static void SaveMap(TerrainGenerator2 terrain){
        BinaryFormatter formatter = new BinaryFormatter(); 
        string path = Application.persistentDataPath + "/map.bin"; 
        FileStream fileStream = new FileStream(path, FileMode.Create); 
        MapData data = new MapData(terrain); 
        formatter.Serialize(fileStream, data); 
        fileStream.Close(); 
    }
    public static MapData LoadMap(){
        string path = Application.persistentDataPath + "/map.bin"; 
        if(File.Exists(path)){
            BinaryFormatter formatter= new BinaryFormatter(); 
            FileStream fileStream = new FileStream(path, FileMode.Open);
            MapData data = formatter.Deserialize(fileStream) as MapData; 
            fileStream.Close();
            return data; 
        }
        else{
            Debug.LogError("File not found"); 
            return null; 
        }
    }
    public static void SaveInstances(){
        BinaryFormatter formatter = new BinaryFormatter(); 
        string path = Application.persistentDataPath + "/instances.bin"; 
        FileStream fileStream = new FileStream(path, FileMode.Create); 
        ObjectsData data = new ObjectsData(); 
        formatter.Serialize(fileStream, data); 
        fileStream.Close();
    }
    public static ObjectsData LoadInstances(){
        string path = Application.persistentDataPath + "/instances.bin"; 
        if(File.Exists(path)){
            BinaryFormatter formatter= new BinaryFormatter(); 
            FileStream fileStream = new FileStream(path, FileMode.Open);
            ObjectsData data = formatter.Deserialize(fileStream) as ObjectsData; 
            fileStream.Close();
            return data; 
        }
        else{
            Debug.LogError("File not found"); 
            return null; 
        }
    }

}

