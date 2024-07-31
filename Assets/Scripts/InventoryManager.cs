// using System.Collections;
using System;
using System.Collections.Generic;
// using System.Linq;
// using System.Security.Cryptography;
// using JetBrains.Annotations;
// using Unity.VisualScripting;
using UnityEngine;
// using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static InventoryManager Instance;
    public Dictionary<allItems, List<GameObject>> Items = new Dictionary<allItems, List<GameObject>>();
    public GameObject key; 
    public GameObject MedKit; 
    public GameObject LittleMedKit; 
    public GameObject SpeedBoost; 
    public GameObject Invisibility; 
    public GameObject Magnifier;
    public GameObject Armor;
    public GameObject SmallArmor; 
    public GameObject MiddleArmor;
    public GameObject BigArmor; 
    public GameObject Strength; 
    public GameObject inventoryCanvas; 
    private InventoryCanvas canvas; 
    public GameObject inventoryEquipped; 
    private InventoryEquippedScript inventoryEquippedScript; 
    public GameObject Poison; 
    public GameObject Lantern; 
    public GameObject chest; 
    public GameObject littleChest; 
    public GameObject door; 
    public MonsterAttackCanvas monsterAttackCanvas;
    // float xy=49f;
    // float update = 5f;
    // public GameObject PoisonArrow; 
    // public GameObject Camera; 
    void Awake()
    {
        Instance = this;
    }
    void Start(){
        canvas = inventoryCanvas.GetComponent<InventoryCanvas>();
        inventoryEquippedScript = inventoryEquipped.GetComponent<InventoryEquippedScript>();
        if(LoadedData.loadOccured) 
            LoadedData.LoadInventory(); 
          
    }
    

    // public void addKey(GameObject key){
    //     //Debug.Log("Running");
    //     if(Items.ContainsKey(allItems.Keys)) {
    //         Items[allItems.Keys].Add(key); 
    //     }
    //     else{
    //         Items[allItems.Keys] = new List<GameObject>
    //         {
    //             key
    //         };
    //     }
    // }
    // public void addMedKit(GameObject medKit){
    //     if(Items.ContainsKey(allItems.MedKit)) {
    //         Items[allItems.MedKit].Add(medKit); 
    //     }
    //     else{
    //         Items[allItems.MedKit] = new List<GameObject>{medKit};
    //     }
    // }
    public void useKey(GameObject object_){
        
        if(Items.ContainsKey(allItems.Keys)&&Items[allItems.Keys].Count!=0){
            // GameObject temp = Items[allItems.Keys].Last();
            // Items[allItems.Keys].RemoveAt(Items[allItems.Keys].Count-1);
            // Destroy(temp);
            
            Items[allItems.Keys].Remove(object_);
            Destroy(object_);
        }
        

    }
    public void useKit(GameObject object_){
        if(Items.ContainsKey(allItems.MedKit)&&Items[allItems.MedKit].Count!=0) {
            // GameObject temp = Items[allItems.MedKit].Last();
            // Items[allItems.MedKit].RemoveAt(Items[allItems.MedKit].Count-1);
            // temp.GetComponent<KitValue>().useKit();
            // Destroy(temp);
            object_.GetComponent<KitValue>().useKit(); 
            Items[allItems.MedKit].Remove(object_);
            Destroy(object_);
            
        }
    }
    public bool getKeys(){
        //Debug.Log("Running");
        if(Items.ContainsKey(allItems.Keys)&&Items[allItems.Keys].Count!=0) {
            
            //Debug.Log(Items[allItems.Keys][0]);
            return true;
        }
        return false;
    }
    public bool getKits(){
        if(Items.ContainsKey(allItems.MedKit)&&Items[allItems.MedKit].Count!=0) return true;
        return false;
    }
    // public void addLittleMedKit(GameObject littleMedKit){
    //     if(Items.ContainsKey(allItems.LittleMedKit)){
    //         Items[allItems.LittleMedKit].Add(littleMedKit); 
    //     }
    //     else{
    //         Items.Add(allItems.LittleMedKit, new List<GameObject> {littleMedKit});
    //     }
    // }
    public void useLittleKit(GameObject object_){
        if(Items.ContainsKey(allItems.LittleMedKit)&&Items[allItems.LittleMedKit].Count!=0) {
            // GameObject temp = Items[allItems.LittleMedKit].Last();
            // Items[allItems.LittleMedKit].RemoveAt(Items[allItems.LittleMedKit].Count-1);
            // int res = temp.GetComponent<LittleMedKitScript>().LittleMedKit;
            object_.GetComponent<LittleMedKitScript>().useKit();
            // Destroy(temp);
            // return res;
            // inventoryEquipped.GetComponent<InventoryEquippedScript>().Remove(object_);
            Items[allItems.LittleMedKit].Remove(object_);
            Destroy(object_);
        }
        // else {
        //     return 0;
        // }
    }
    public bool Add(GameObject object_, allItems item){
        if(canvas.occupied<10){
            if(Items.ContainsKey(item)) 
                Items[item].Add(object_);
            else
                Items.Add(item, new List<GameObject>{
                    object_
                });
            canvas.Add(object_, item);
            RemoveFromObjects(item, object_); 
            return true; 
        }
        else {
            // object_.GetComponent<FloatObject>().Float();
            return false;
        }
    }
    public void AddToEquipped(GameObject gameObject, allItems item){
        if(Items.ContainsKey(item)) 
                Items[item].Add(gameObject);
        else
            Items.Add(item, new List<GameObject>{
                gameObject
            });
        inventoryEquippedScript.Add(item, gameObject); 
    }   
    public void useBoost(allItems boost, GameObject gameObject){
        if(Items.ContainsKey(boost)&&Items[boost].Count!=0) {
            // GameObject temp = Items[boost].;
            Items[boost].Remove(gameObject);
            inventoryEquipped.GetComponent<InventoryEquippedScript>().Remove(gameObject);
            Destroy(gameObject);
        }
        else {
            
        }
    }
    public void ActivateDebuff(allItems debuff, GameObject gameObject){
        inventoryEquipped.GetComponent<InventoryEquippedScript>().Add(debuff, gameObject); 
    }
    public void RemoveDebuff(GameObject gameObject){
        inventoryEquipped.GetComponent<InventoryEquippedScript>().Remove(gameObject); 
        Destroy(gameObject);
    }
    public void RemoveArmor(GameObject object_){
        Items[allItems.Armor].Remove(object_);
        inventoryEquipped.GetComponent<InventoryEquippedScript>().Remove(object_);
        Destroy(object_);
    }
    public float[] GetInventoryForSave(){
        float[] res = new float[23];
        int i =0; 
        res[22]=-1; 
        foreach(var pair in Items){
            foreach(var value in pair.Value){
                if(pair.Key==allItems.Armor){
                    if(value.GetComponent<BigArmor>()!=null) res[i] = 2+(int)pair.Key;
                    else if(value.GetComponent<MiddleArmor>()!=null) res[i] = 1+(int)pair.Key; 
                    if(value.GetComponent<ArmorScript>().activated) res[22] = i; 
                }
                else
                    res[i] = (int)pair.Key; 
                res[i+1] = GetCorrespondingValue(pair.Key, value);
                i+=2; 
            }
        }
        return res; 
    }
    private float GetCorrespondingValue(allItems item, GameObject gameObject){
        switch(item){
            case allItems.MedKit : return gameObject.GetComponent<KitValue>().MedKit; 
            case allItems.LittleMedKit : return gameObject.GetComponent<LittleMedKitScript>().littleMedKit; 
            case allItems.Armor : return gameObject.GetComponent<ArmorScript>().resistance; 
            case allItems.SpeedBoost : case allItems.Invisibility: case allItems.Magnifier: case allItems.Strength: 
            case allItems.Lantern: 
            if(gameObject.GetComponent<BoostScript>().activated )
                return -gameObject.GetComponent<BoostScript>().time; 
            else 
            return gameObject.GetComponent<BoostScript>().time; 
            default : return 0; 
        }
    }
    private void RemoveFromObjects(allItems item, GameObject gameObject){
        try{
            switch (item)
            {
                case allItems.Armor : gameObject.GetComponent<ArmorScript> ().Remove(); break; 
                default: break; 
            }
        } catch(Exception){}
    }
    // public float[] GetInventoryEquippedForSave(){
    //     float [] res= inventoryEquipped.GetComponent<InventoryEquippedScript>().GetInventoryEquippedForSave(); 
    //     // foreach
    //     return res; 
    // }
    
    // Update is called once per frame
    // void Update(){
    //     if(update>=5f){
    //         Instantiate(chest, new Vector2(xy, xy), Quaternion.identity);
    //         update=0f;
    //     }
    //     update+=Time.deltaTime; 
    //     xy+=update;
    // }
}
public enum allItems{
    Keys =1, 
    MedKit =2, 
    LittleMedKit = 3, 
    SpeedBoost = 4, 
    Invisibility = 5, 
    Magnifier = 6, 
    Strength =7, 
    Poison = 8, 
    Lantern = 9, 
    Armor = 10, //small 0; middle +1; big+2 if activated last idx of the array points to it
    Chest =11, 
    LittleChest = 12

    
}
