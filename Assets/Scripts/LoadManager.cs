
using UnityEngine;
using UnityEngine.SceneManagement; 
public class LoadManager : MonoBehaviour
{
    public GameObject Player; 
    public GameObject terrain; 
    public TerrainGenerator2 terrainGenerator; 
    // public DungeonGenerator generator; 

    private void SavePlayer(){
        SaveSystem.SavePlayer(Player); 
    }
    private void LoadPlayer(){
        PlayerData data = SaveSystem.LoadPlayer(); 
        LoadedData.data = data; 
        
        // Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]); 
    }
    private void SaveMap(){
        SaveSystem.SaveMap(terrainGenerator); 
    }
    private void LoadMap(){
        MapData data = SaveSystem.LoadMap(); 
        LoadedData.mapData = data; 
        

    }
    private void SaveInstances(){
        SaveSystem.SaveInstances(); 
    }
    public void SaveGame(){
        terrainGenerator = terrain.GetComponent<DungeonGenerator>().terrain;
        SaveMap(); 
        SavePlayer(); 
        SaveInstances(); 
    }
    private void LoadInstances(){
        LoadedData.objectsData = SaveSystem.LoadInstances(); 
    }
    public void LoadGame(){
        LoadMap(); 
        LoadPlayer(); 
        LoadInstances(); 
        LoadedData.loadOccured=true; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        // LoadedData.LoadMapData(terrain.GetComponent<DungeonGenerator>()) ;
    }
}
