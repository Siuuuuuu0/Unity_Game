using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthScript : MonoBehaviour
{
    // Start is called before the first frame update
    // private bool activated =false;
    // private float update =0.0f;  
    public float time =10; 
    private float multiplier=2; 
    public allItems Item; 

    void Start(){
        // time = 10; 
        multiplier = 2; 
        Item = allItems.Strength;
        transform.gameObject.SetActive(false);
    }
    public void Activate(){
        GetComponent<BoostScript>().activated=true; 
        // activated =true;
        GameObject.Find("Player").GetComponent<PlayerObject>().resistance*=multiplier; 
        RunningBoosts.Instance.runningBoost.Add(transform.gameObject);
        RunningBoosts.Instance.boosts++; 
        // RunningBoosts.Instance.time.Add(time);
    }
    public void Finished(){
        // activated =false;
        GameObject.Find("Player").GetComponent<PlayerObject>().resistance/=multiplier; 
        InventoryManager.Instance.useBoost(allItems.Strength, transform.gameObject);
    }
}
