// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEditor.Rendering;
using UnityEngine;

public static class LoadedData 
{
    public static MapData mapData; 
    public static PlayerData data; 
    public static ObjectsData objectsData; 
    public static bool loadOccured; 
    
    public static Vector3 PlayerPosition(){
        return new Vector3(data.position[0], data.position[1], data.position[2]);
    }
    public static void LoadInventory(){
        // Debug.Log(data.Inventory[26]); 
        for(int i =0; i<22; i+=2){
            // Debug.Log(data.Inventory[i]+""+ data.Inventory[i+1]);
            if(data.Inventory[i]==10){
                GameObject gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.SmallArmor); 
                gameObject.GetComponent<ArmorScript>().resistance = data.Inventory[i+1]; 
                if(data.Inventory[22] ==i)
                    InventoryManager.Instance.AddToEquipped(gameObject, allItems.Armor); 
                else
                    InventoryManager.Instance.Add(gameObject, allItems.Armor); 

            }else if(data.Inventory[i]==11){
                GameObject gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.MiddleArmor); 
                gameObject.GetComponent<ArmorScript>().resistance = data.Inventory[i+1]; 
                if(data.Inventory[22] ==i)
                    InventoryManager.Instance.AddToEquipped(gameObject, allItems.Armor); 
                else
                    InventoryManager.Instance.Add(gameObject, allItems.Armor);
            }else if(data.Inventory[i]==12){
                GameObject gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.BigArmor); 
                gameObject.GetComponent<ArmorScript>().resistance = data.Inventory[i+1]; 
                if(data.Inventory[22] ==i)
                    InventoryManager.Instance.AddToEquipped(gameObject, allItems.Armor); 
                else
                    InventoryManager.Instance.Add(gameObject, allItems.Armor);
            }
            else if(data.Inventory[i]!=0&&data.Inventory[i+1]<0)
            {
                GameObject gameObject = InstantiateGameObject(i , (allItems)data.Inventory[i]);
                gameObject.GetComponent<BoostScript>().time = Mathf.Abs(data.Inventory[i+1]); 
                gameObject.GetComponent<BoostScript>().Activate(); 
                InventoryManager.Instance.AddToEquipped(gameObject, (allItems)data.Inventory[i]); 
            } 
            else if (data.Inventory[i]!=0)
                InventoryManager.Instance.Add(InstantiateGameObject(i, (allItems)data.Inventory[i]), (allItems)data.Inventory[i]);
        }
    }
    private static GameObject InstantiateGameObject(int i, allItems item){
        GameObject gameObject; 
        switch (item){
            case allItems.Keys: 
            gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.key); 
            gameObject.SetActive(false); 
            return gameObject; 
            case allItems.MedKit: 
            gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.MedKit);
            gameObject.GetComponent<KitValue>().MedKit = (int)data.Inventory[i+1]; 
            return gameObject; 
            case allItems.LittleMedKit: 
            gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.LittleMedKit);
            gameObject.SetActive(false); 
            gameObject.GetComponent<LittleMedKitScript>().littleMedKit = (int)data.Inventory[i+1]; 
            return gameObject; 
            case allItems.SpeedBoost:
            gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.SpeedBoost);
            return gameObject; 
            case allItems.Invisibility:
            gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.Invisibility);
            return gameObject;
            case allItems.Magnifier:
            gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.Magnifier);
            return gameObject;
            case allItems.Strength:
            gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.Strength);
            return gameObject;
            case allItems.Lantern:
            gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.Lantern);
            return gameObject;
            case allItems.Poison:
            gameObject = MonoBehaviour.Instantiate(InventoryManager.Instance.Poison);
            return gameObject; 
            default: return null; 
        }
        // return null; 
    }
    // public static void LoadMapData(DungeonGenerator generator){
    //     generator.Load(new TerrainGenerator2(mapData.rooms)); 
    // }
    // public static void LoadInventoryEquipped(){
    //     GameObject gameObject; 
    //     for(int i =0; i<6; i+=2){
    //         gameObject = InstantiateGameObject(i, (allItems)data.EquippedInventory[i]); 
    //         gameObject.GetComponent<BoostScript>().time = data.EquippedInventory[i+1]; 
    //         InventoryManager.Instance.AddToEquipped(gameObject, (allItems)data.EquippedInventory[i]); 
    //     }
    // }
}
