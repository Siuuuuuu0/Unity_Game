
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Sprite> sprites;
    public SlotController [] items; 
    public bool [] isFull;
    // InventoryManager inventory = InventoryManager.Instance;
    public InventoryEquippedScript inventoryEquipped; 
    public int occupied = 0; 
    void Awake()
    {
        
            items = GetComponentsInChildren<SlotController>();
            foreach(var item in items){
                item.image.enabled=false;
            }
        
        // GameObject temp = Instantiate(SpeedPotionButton, items[0].transform, false);
        // temp.transform.SetAsLastSibling();
        // items[0].GetComponent<Im
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Alpha0)){
            items[0].Use();
        }if(Input.GetKeyDown(KeyCode.Alpha1)){
            items[1].Use();
        }if(Input.GetKeyDown(KeyCode.Alpha2)){
            items[2].Use();
        }if(Input.GetKeyDown(KeyCode.Alpha3)){
            items[3].Use();
        }if(Input.GetKeyDown(KeyCode.Alpha4)){
            items[4].Use();
        }if(Input.GetKeyDown(KeyCode.Alpha5)){
            items[5].Use();
        }if(Input.GetKeyDown(KeyCode.Alpha6)){
            items[6].Use();
        }if(Input.GetKeyDown(KeyCode.Alpha7)){
            items[7].Use();
        }if(Input.GetKeyDown(KeyCode.Alpha8)){
            items[8].Use();
        }if(Input.GetKeyDown(KeyCode.Alpha9)){
            items[9].Use();
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void Add(GameObject object_, allItems item){
        // foreach(var ele in isFull) Debug.Log(ele); 
        // Debug.Log("running"); 
        switch(item){
            case allItems.Keys : AddToSlot(object_, item, sprites[0]); break;
            case allItems.MedKit : AddToSlot(object_, item, sprites[1]); break;
            case allItems.LittleMedKit : AddToSlot(object_, item, sprites[2]); break;
            case allItems.SpeedBoost : AddToSlot(object_, item, sprites[3]); break;
            case allItems.Invisibility : AddToSlot(object_, item, sprites[4]); break;
            case allItems.Magnifier : AddToSlot(object_, item, sprites[5]); break;
            case allItems.Strength: AddToSlot(object_, item, sprites[9]); break; 
            case allItems.Poison: AddToSlot(object_, item, sprites[10]); break;
            case allItems.Lantern : AddToSlot(object_, item, sprites[11]); break;
            case allItems.Armor : 
                Sprite sprite; 
                if (object_.GetComponent<SmallArmor>()!=null) 
                    sprite = sprites[6];
                else if(object_.GetComponent<MiddleArmor>()!=null) 
                    sprite = sprites[7];
                else 
                    sprite = sprites[8];
                AddToSlot(object_, item, sprite);
                
            break;
            default: break;
        }
        
    }
    private void AddToSlot(GameObject object_, allItems item, Sprite sprite){
        for(int i =0; i<items.Length; i++){
            if(!isFull[i]){
                // Debug.Log("running"); 
                items[i].Add(object_, item, sprite, i); 
                isFull[i]=true;
                occupied++;
                break;
            }
        }
    }
}
