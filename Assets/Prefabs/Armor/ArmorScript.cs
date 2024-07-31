// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Animations;

public class ArmorScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float multiplier{get; protected set;}
    public bool activated = false; 
    public float resistance;
    protected PlayerObject player;
    protected int order; 
    // public allItems item = allItems.Armor; 
    public ArmorType type; 
    public bool added=false; 
    float countdown = 60f; 
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerObject>();
    }
    void Update(){
        if(resistance<0){
            player.armor = null; 
            Deactivate();
            InventoryManager.Instance.RemoveArmor(transform.gameObject);
        }
        // if(gameObject.activeSelf) Debug.Log(true); 
        if(gameObject.activeSelf){
            if(!added){
                Objects.Instance.Add(allItems.Armor, gameObject); 
                added=true; 
            }
            countdown-=Time.deltaTime;
            if(countdown<=0 ) {
                Debug.Log("running");
                Objects.Instance.Remove(allItems.Armor, gameObject);
                Destroy(gameObject);
            } 
        }
        
    }
    public void Remove(){
        if(added){
            added=false; 
            Objects.Instance.objects[allItems.Armor].Remove(gameObject); 
        }
    }
    // Update is called once per frame
    public void Activate(){
        if(player.armor!=null)
            player.armor.GetComponent<ArmorScript>().Deactivate();
        activated = true; 
        PlayAnimation(true, order);
        player.armor = transform.gameObject;
    }
    public void Deactivate(){
        activated =false;
        PlayAnimation(false, order);
        player.armor = null;
    }
    public void PlayAnimation(bool on_off, int i){
        if(on_off)
            player.setArmor(i);
        else
            player.RemoveArmor(i);
    }
    
}
public enum ArmorType{
    Small ,
    Medium ,
    Big 
}
