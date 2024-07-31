using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LittleChestController : ChestController
{
    // Start is called before the first frame update
    // private bool inside=false; 
    void Update(){
        if(inside&&Input.GetKeyDown(KeyCode.K)){
            transform.parent.GetComponent<LittleChestScript>().Open();
        }
    }
    // void OnTriggerEnter2D(Collider2D collider){
    //     if(collider.tag=="Player")
    //         inside = true; 
    // }
    // void OnTriggerExit2D(Collider2D collider){
    //     if(collider.tag=="Player")
    //         inside =false; 
    // }

    // Update is called once per frame
    
}
