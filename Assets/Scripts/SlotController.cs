// using System.Collections;
// using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
// using UnityEngine.UI; 
using Image= UnityEngine.UI.Image; 
public class SlotController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image; 
    public Sprite sprite; 
    GameObject object_; 
    public allItems item;
    InventoryCanvas canvas; 
    private int order; 
    InventoryManager inventory;
    InventoryEquippedScript inventoryEquipped; 
    PlayerObject player; 
    // void Start(){
    //     image.sprite = sprite; 
    // }
    void Start(){
        player = GameObject.Find("Player").GetComponent<PlayerObject>(); 
        canvas = transform.parent.GetComponent<InventoryCanvas>();
        inventory = InventoryManager.Instance;
        inventoryEquipped = transform.parent.GetComponent<InventoryCanvas>().inventoryEquipped;
    }
    public void Use(){
        if(canvas.isFull[order]){
            switch(item){
                case allItems.Keys:
                if(player.canUseKey){
                    // inventory.useKey(); 
                    player.canUseKey=false;
                    player.door.setOpen();
                    image.enabled = false;
                    canvas.isFull[order] = false;
                    canvas.occupied--;
                }
                return;
                case allItems.MedKit:
                inventory.useKit(object_); break;
                case allItems.SpeedBoost :
                if(RunningBoosts.Instance.boosts<3 ){
                    object_.GetComponent<SpeedBoostScript>().Activate(); break;} 
                else return; 
                case allItems.Invisibility : 
                if(RunningBoosts.Instance.boosts<3 ){
                    object_.GetComponent<InvisibilityScript>().Activate(); break; }
                else return;
                case allItems.Magnifier :
                if(RunningBoosts.Instance.boosts<3 ){
                    object_.GetComponent<MagnifierScript>().Activate(); break;}
                else return;
                case allItems.LittleMedKit:
                inventory.useLittleKit(object_); break;
                case allItems.Armor :
                object_.GetComponent<ArmorScript>().Activate(); break;
                case allItems.Strength: 
                if(RunningBoosts.Instance.boosts<3 ){
                    object_.GetComponent<StrengthScript>().Activate(); break; }
                else return;
                // default : return;
                
            }
            image.enabled = false;
            canvas.isFull[order] = false;
            inventoryEquipped.Add(item, object_);
            canvas.occupied--;
        }
        // Debug.Log("running");
    }
    public void Add(GameObject gameObject, allItems item, Sprite sprite, int order){
        object_ = gameObject; 
        this.item = item;
        image.sprite = sprite; 
        image.enabled = true;
        this.order=order;
    }
}
