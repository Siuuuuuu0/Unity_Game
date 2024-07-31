
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public float[] position;
    public float health; 
    //1st number idx of the thing as in the allItems Enum, seond its remaining time or resistance etc
    public float[] Inventory; 
    // public float [] EquippedInventory; 
    public PlayerData(GameObject player){
        position = new float[3]; 
        position[0] = player.transform.position.x; 
        position[1] = player.transform.position.y; 
        position[2] = player.transform.position.z; 
        health = player.GetComponent<PlayerObject>().health; 
        Inventory = InventoryManager.Instance.GetInventoryForSave(); 
        // EquippedInventory = InventoryManager.Instance.GetInventoryEquippedForSave(); 
    }
}
