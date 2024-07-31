
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 
public class InventoryEquippedScript : MonoBehaviour
{
    // Start is called before the first frame update
    public SlotEquippedScript [] items; 
    public bool [] isFull;
    InventoryManager inventory = InventoryManager.Instance;
    
    private InventoryCanvas inventoryCanvas; 
    public GameObject canvas;
    public Sprite[] sprites; 
    void Awake()
    {

        inventoryCanvas = canvas.GetComponent<InventoryCanvas>();
        items = GetComponentsInChildren<SlotEquippedScript>();
        foreach(var item in items){
            item.image.enabled=false;
        }
        
        
    }

    public void Add(allItems item, GameObject gameObject){
        switch(item){
            case allItems.SpeedBoost : AddToSlot(sprites[0], gameObject, item); break;
            case allItems.Invisibility : AddToSlot(sprites[1], gameObject, item); break;
            case allItems.Magnifier : AddToSlot(sprites[2], gameObject, item); break;
            case allItems.Strength : AddToSlot(sprites[6], gameObject, item); break; 
            case allItems.Poison : AddToSlot(sprites[7], gameObject, item); break;
            case allItems.Lantern: AddToSlot(sprites[8], gameObject, item); break;
            case allItems.Armor : 
                Sprite sprite; 
                if (gameObject.GetComponent<SmallArmor>()!=null) 
                    sprite = sprites[3];
                else if(gameObject.GetComponent<MiddleArmor>()!=null) 
                    sprite = sprites[4];
                else 
                    sprite = sprites[5];
                if(isFull[0]) {
                    // items[0].GetComponent<ArmorScript> ().Deactivate();
                    inventoryCanvas.Add(items[0].gameObject, allItems.Armor);
                    items[0].Remove(); 
                    // inventoryCanvas.Add(items[0].gameObject, allItems.Armor);
                }
                items[0].Add(sprite, gameObject, 0, item);
                isFull[0]=true;
                break;
            default: break;
        }
    }
    private void AddToSlot(Sprite sprite, GameObject gameObject, allItems item){
        for(int i =1; i<items.Length; i++){
            if(!isFull[i]){
                items[i].Add(sprite, gameObject, i, item); 
                isFull[i]=true;
                return;
            }
        }

    }
    public void Remove(GameObject gameObject){
        foreach(var slot in items){
            if(slot.gameObject==gameObject){
                slot.Remove();
                return; 
            }
        }
    }
    // public float[] GetInventoryEquippedForSave(){
    //     float[] res = new float[6]; 
    //     int i =0; 
    //     int j = 0; 
    //     foreach(var item in items){
    //         if(j!=0){
    //             if(isFull[j]){
    //                 res[i] = (int)item.item; 
    //                 res[i+1] = item.gameObject.GetComponent<BoostScript>().time; 
    //             }
    //             else{
    //                 res[i]=-1; 
    //                 res[i+1]=-1; 
    //             }
    //             i+=2; 
    //         }
    //         j++; 
    //     }
    //     return res; 


    // }
}

