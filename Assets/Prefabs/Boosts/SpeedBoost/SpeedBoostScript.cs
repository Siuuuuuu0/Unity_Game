using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostScript : MonoBehaviour
{
    // Start is called before the first frame update
    // private bool activated =false;
    // private float update =0.0f;  
    public float time =10; 
    public float multiplier; 
    public allItems Item; 

    void Start(){
        time = 10; 
        multiplier = 2; 
        Item = allItems.SpeedBoost;
        transform.gameObject.SetActive(false);
    }
    // Update is called once per frame
    // void Update()
    // {
    //     if(activated){
    //         update +=Time.deltaTime; 
    //         if(update>time){
    //             Finished(); 
    //         }
    //     }
    //     // if(Input.GetKeyDown(KeyCode.Space)){
    //     //     Activate(); 
    //     // }
    // }
    public void Activate(){
        GetComponent<BoostScript>().activated=true; 
        // activated =true;
        GameObject.Find("Player").GetComponent<PlayerController>().moveSpeed*=multiplier; 
        RunningBoosts.Instance.runningBoost.Add(transform.gameObject);
        RunningBoosts.Instance.boosts++; 
        // RunningBoosts.Instance.time.Add(time);
    }
    public void Finished(){
        // activated =false;
        GameObject.Find("Player").GetComponent<PlayerController>().moveSpeed/=multiplier; 
        InventoryManager.Instance.useBoost(allItems.SpeedBoost, transform.gameObject);
    }
}
