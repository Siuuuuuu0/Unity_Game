using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScript : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerObject player; 
    private HealthbarSlotScript [] items; 
    private float lastvalue;
    public Sprite[] sprites;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerObject>(); 
        items = GetComponentsInChildren<HealthbarSlotScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player.health!=lastvalue){ 
            lastvalue = player.health;
            UpdateUI();
        }
        
    }
    void UpdateUI(){
        // for (int i = 8; i>(int)lastvalue/100; i--){

        // } 
        // Debug.Log(lastvalue);
        for(int i =0; i<8; i++){
            if((lastvalue-100*(7-i))>75){
                items[7-i].setImage(sprites[0]); break; 
            }
            else if((lastvalue-100*(7-i))>50){
                items[7-i].setImage(sprites[1]); break;}
            else if((lastvalue-100*(7-i))>25){
                items[7-i].setImage(sprites[2]); break;}
            else if((lastvalue-100*(7-i))>0){
                items[7-i].setImage(sprites[3]); break;}
            else 
                items[7-i].setImage(sprites[4]);  
        }
    }
}
