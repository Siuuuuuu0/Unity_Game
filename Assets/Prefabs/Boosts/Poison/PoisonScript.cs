using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float time =10; 
    public allItems Item; 
    public float damage; 

    void Start(){
        damage = 0.2f; 
        time = 10; 
        Item = allItems.Poison;
        transform.gameObject.SetActive(false);
        Activate();
        
        
    }
    public void Activate(){
        GetComponent<BoostScript>().activated=true; 
        // activated =true;
        // GameObject.Find("Player").GetComponent<PlayerObject>().resistance*=multiplier; 
        RunningBoosts.Instance.runningBoost.Add(transform.gameObject);
        // InventoryManager.Instance.ActivateDebuff(allItems.Poison, transform.gameObject); 
        RunningBoosts.Instance.boosts++; 
        // RunningBoosts.Instance.time.Add(time);
    }
    public void Finished(){
        // activated =false;
        // GameObject.Find("Player").GetComponent<PlayerObject>().resistance/=multiplier; 
        InventoryManager.Instance.RemoveDebuff(transform.gameObject);
    }
}
