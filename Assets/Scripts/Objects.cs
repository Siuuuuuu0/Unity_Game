// using System.Collections;
using System.Collections.Generic;
// using Unity.Collections;

// using Unity.VisualScripting;
using UnityEngine;
// using UnityEngineInternal;

public class Objects : MonoBehaviour
{
    // Start is called before the first frame update
    public static Objects Instance; 
    int size; 
    public Dictionary<allItems, List<GameObject>> objects; 
    void Awake()
    {
        Instance = this; 
        objects = new Dictionary<allItems, List<GameObject>>(); 
    }
    public void Add(allItems key, GameObject gameObject){
        if(objects.ContainsKey(key)) 
            objects[key].Add(gameObject); 
        else
            objects.Add(key, new List<GameObject>{gameObject}); 
        size++; 
        // Debug.Log(size); 
    }
    // Update is called once per frame
    public float[][] ToSave(){
        float[][] res = new float[size][]; 
        // float [] positions = new float[size]; 
        // float [] objects_ = new float[size]; 
        // for(int i =0; i<size; i++){
        //     res[i] = new float[3]; 
        // }
        int j =0; 
        foreach(var pair in objects){
            foreach(var ele in pair.Value){
                switch(pair.Key){
                    case allItems.Chest :
                    if(ele.GetComponent<ChestScript>().opened){
                        res[j] = new float[4]{
                            (int)allItems.Chest, ele.transform.position.x, ele.transform.position.y, 0
                        };
                    }
                    else{
                        res[j] = new float[4]{
                            (int)allItems.Chest, ele.transform.position.x, ele.transform.position.y, ele.GetComponent<ChestScript>().count
                        }; 
                        foreach(var pair_ in ele.GetComponent<ChestScript>().items){
                            foreach(var ele_ in pair_.Value){
                                j++; 
                                res[j] = GetArray(pair_.Key, ele_); 
                            }
                        }
                        // break; 
                    }
                    break;
                    case allItems.LittleChest : 
                    if(ele.GetComponent<LittleChestScript>().opened){
                        res[j] = new float[4]{
                            (int)allItems.LittleChest, ele.transform.position.x, ele.transform.position.y, 0
                        };
                    }
                    else{
                        res[j] = new float[4]{
                            (int)allItems.LittleChest, ele.transform.position.x, ele.transform.position.y, ele.GetComponent<LittleChestScript>().count
                        }; 
                        foreach(var pair_ in ele.GetComponent<LittleChestScript>().items){
                            foreach(var ele_ in pair_.Value){
                                j++; 
                                res[j] = GetArray(pair_.Key, ele_); 
                            }
                        }
                        // break;
                    } break; 
                    default : res[j] = GetArray(pair.Key, ele); break; 

                }
                j++; 
                if(j==size) return res; 
                // Debug.Log(j); 
            }
        }
        return res; 
    }
    // void Update()
    // {
        
    // }
    private float[] GetArray(allItems item, GameObject ele){
        float [] res; 
        switch(item){
            case allItems.Keys: 
                res = new float[3]{
                    (int)item, ele.transform.position.x, ele.transform.position.y, 
                    
                }; 
                return res; 
            case allItems.MedKit:
                res = new float[4]{
                    (int)item, ele.transform.position.x, ele.transform.position.y, 
                    ele.GetComponent<KitValue>().MedKit
                }; 
                return res; 
            case allItems.LittleMedKit:
                res = new float[4]{
                    (int)item, ele.transform.position.x, ele.transform.position.y, 
                    ele.GetComponent<LittleMedKitScript>().LittleMedKit
                };  
                return res;
            case allItems.SpeedBoost : case allItems.Invisibility : case allItems.Magnifier : case allItems.Strength : case allItems.Lantern :
                res = new float[4]{
                    (int)item, ele.transform.position.x, ele.transform.position.y, 
                    ele.GetComponent<BoostScript>().time
                };  
                    
                return res;
            case allItems.Armor:
                switch(ele.GetComponent<ArmorScript>().type) {
                    case ArmorType.Small : 
                        res = new float[5]{
                        (int)item, ele.transform.position.x, ele.transform.position.y, 
                        1, ele.GetComponent<ArmorScript>().resistance
                        };  
                        return res; 
                    // res[(int)ele.transform.position.x][(int)ele.transform.position.y] = (int)pair.Key+ 0.1f + ele.GetComponent<ArmorScript>().resistance/1000f;break; 
                    case ArmorType.Medium : 
                        res = new float[5]{
                        (int)item, ele.transform.position.x, ele.transform.position.y, 
                        2, ele.GetComponent<ArmorScript>().resistance
                        };  
                        return res;
                    case ArmorType.Big : 
                        res = new float[5]{
                        (int)item, ele.transform.position.x, ele.transform.position.y, 
                        3, ele.GetComponent<ArmorScript>().resistance
                        };  
                        return res;
                } 
                break;
            default : return new float[0];  
        
        }
        return new float[0];
    }
    public void Remove(allItems item, GameObject object_){
        objects[item].Remove(object_);
    }
}
