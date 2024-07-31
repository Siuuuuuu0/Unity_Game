using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    public bool activated = false;
    // private float update =0.0f;  
    public float time; 
    public allItems item; 

    // Start is called before the first frame update
    void Start()
    {
        time = 10; 
        item = allItems.Lantern;
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
        activated = true; 
       GameObject.Find("Player").GetComponentInChildren<PlayerLantern>().Activate();  
        RunningBoosts.Instance.runningBoost.Add(transform.gameObject);
        RunningBoosts.Instance.boosts++; 
        //animator.SetBool("invisible/outline", true);
    }
    public void Finished(){
        activated =false;
        GameObject.Find("Player").GetComponentInChildren<PlayerLantern>().Deactivate(); 
        InventoryManager.Instance.useBoost(allItems.Lantern, transform.gameObject);
    }
}
