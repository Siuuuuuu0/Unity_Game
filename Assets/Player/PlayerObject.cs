using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;


public class PlayerObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 800.0f; 
    private float update=0.0f;
    public float resistance =1.0f; 
    public GameObject NavMesh; 
    public GameObject armor;
    //private int key=0; 
    //private List<int> kits = new List<int>();
    
    private DungeonGenerator generator;
    public bool invisible = false;
    public bool canUseKey = false;
    public ChildController door; 
    public new bool light=false; 
    private PlayerLantern playerLantern; 
    public DeathScreen deathScreen;
    void Awake(){
        generator = FindObjectOfType<DungeonGenerator>();
    }
    // Update is called once per frame
    void Start(){
        Time.timeScale=1f;
        playerLantern = GetComponentInChildren<PlayerLantern>();
        transform.position = new Vector3(60f,50f,0f);
    }
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.U)&& InventoryManager.Instance.getKits()) {
        //     health += (float)InventoryManager.Instance.useKit();
        // }
        // Debug.Log(health);
        update += Time.deltaTime;
        if (update > 1.0f)
        {
            update = 0.0f;
            try{health-= generator.getTileFire(transform.position)/resistance; /*Debug.Log(health);*/} catch (Exception){}
            //Debug.Log(health);
            //Debug.Log(generator.getTileKit(transform.position));
        }
        if(health<=0){
            deathScreen.Dead();
        }
    }
    public void takeDamage(float damage){
        if(armor!=null){
            health-=damage/(armor.GetComponent<ArmorScript>().multiplier*resistance);
            armor.GetComponent<ArmorScript>().resistance-= damage/100;
        }
        else 
            health-=damage; 
        // if(health<0) 
    }
    public void PoisonDamage(float damage){
        health -= damage; 
    }
    public void setArmor(int i){
        switch (i)
        {
            case 0 :
                //animator.SetBool("armor0", true); 
                break;
            case 1:
                //animator.SetBool("armor1", true);
                break;
            case 2 :
                //animator.SetBool("armor2", true);
                break;
            default: break;
        }
        //animator.SetBool("armor1", true);c
    }
    public void RemoveArmor(int i){
        switch (i)
        {
            case 0 :
                //animator.SetBool("armor0", false); 
                break;
            case 1:
                //animator.SetBool("armor1", false);
                break;
            case 2 :
                //animator.SetBool("armor2", false);
                break;
            default: break;
        }
    }
    public void LightRight(){
        if(light){
            playerLantern.LightRight(); 
        }
    }
    public void LightLeft(){
        if(light){
            playerLantern.LightLeft(); 
        }
    }
    public void LightUp(){
        if(light){
            playerLantern.LightUp(); 
        }
    }
    public void LightDown(){
        if(light){
            playerLantern.LightDown(); 
        }
    }
    // public int Key{
    //     get=>key; 
    //     set{
    //         key=value;
    //     }
    // }
    // public int MedKit{
    //     get=> kits.Last();
    //     set{
    //         kits.Add(value);
    //         //foreach(var val in kits) Debug.Log(val);
    //     }
    // }
}
