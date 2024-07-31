using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunningBoosts : MonoBehaviour
{
    // Start is called before the first frame update
    // public Dictionary<GameObject, float> runningBoosts; 
    public List<GameObject> runningBoost; 
    // private List<GameObject> toDelete; 
    // public List<float> time;
    public static RunningBoosts Instance; 
    public int boosts=0; 
    private PlayerObject player; 
    void Awake()
    {
        Instance = this; 
        // runningBoosts = new Dictionary<GameObject, float>();
        runningBoost = new List<GameObject>();
        // toDelete= new List<GameObject>(); 
    }
    void Start(){
        player = GameObject.Find("Player").GetComponent<PlayerObject>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // foreach(var boost in runningBoosts){
            // boost.Value-=Time.deltaTime; 
        // }
        foreach(var boost in runningBoost){
            boost.GetComponent<BoostScript>().time-=Time.deltaTime; 
            if(boost.GetComponent<BoostScript>().item==allItems.Poison)
                player.PoisonDamage(boost.GetComponent<PoisonScript>().damage);
            
            if(boost.GetComponent<BoostScript>().time<0){
                GameObject temp = boost; 
                runningBoost.Remove(temp);
                temp.GetComponent<BoostScript>().Finish(); 
                boosts--;
                break;

                // toDelete.Add(boost); 
                // runningBoost.Remove(boost);
            }
        }
        // foreach(var boost in toDelete){
        //     runningBoost.Remove(boost); 
        // }
    }


}
