// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine;

public class BoostScript : MonoBehaviour
{
    // Start is called before the first frame update
    public allItems item;
    public float time = 10; 
    public bool activated = false; 
    public bool added = false; 
    Vector2 floatY;
    float originalY;
    float floatStrength = Mathf.Pow(10, -1);
    float countdown = 60f;
    // public bool active;
    void Start(){
        // if(active){
        //     gameObject.SetActive(true);
        // }
        this.originalY = this.transform.position.y;
    }
    public void Finish(){
        // RunningBoosts.Instance.runningBoost.Remove(transform.gameObject);
        switch(item){
            case allItems.SpeedBoost:  
            GetComponent<SpeedBoostScript>().Finished(); 
            // time = GetComponent<SpeedBoostScript>().time; 
            break;
            case allItems.Invisibility: GetComponent<InvisibilityScript>().Finished(); 
            // time = GetComponent<InvisibilityScript>().time; 
            break; 
            case allItems.Magnifier: GetComponent<MagnifierScript>().Finished(); 
            break; 
            case allItems.Strength : GetComponent<StrengthScript>().Finished();
            break;
            case allItems.Poison : GetComponent<PoisonScript>().Finished();
            // time = GetComponent<MagnifierScript>().time;
            break;
        }
    }
    public float GetTime(){
        if(activated) return -time; 
        return time;
    }
    public void Activate(){
        activated = true; 
        switch(item){
            case allItems.SpeedBoost:  
            GetComponent<SpeedBoostScript>().Activate(); 
            // time = GetComponent<SpeedBoostScript>().time; 
            break;
            case allItems.Invisibility: GetComponent<InvisibilityScript>().Activate(); 
            // time = GetComponent<InvisibilityScript>().time; 
            break; 
            case allItems.Magnifier: GetComponent<MagnifierScript>().Activate(); 
            break; 
            case allItems.Strength : GetComponent<StrengthScript>().Activate();
            break;
            case allItems.Poison : GetComponent<PoisonScript>().Activate();
            // time = GetComponent<MagnifierScript>().time;
            break;
            case allItems.Lantern:
            GetComponent<LanternController>().Activate(); break; 
        }
    }
    void Update(){
        
        if(gameObject.activeSelf){
            if(!added){
            // Debug.Log(true); 
                added = true; 
                Objects.Instance.Add(item, gameObject); 
            }
            floatY = transform.position;
            floatY.y = originalY + (Mathf.Sin(Time.time) * floatStrength);
            transform.position = floatY;
            countdown-=Time.deltaTime;
            if(countdown<=0 ) {
                Debug.Log("running");
                Objects.Instance.Remove(item, gameObject);
                Destroy(gameObject);
            } 
        }
            
    }
        
    
}
