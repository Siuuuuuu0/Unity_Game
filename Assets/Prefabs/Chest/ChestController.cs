using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    // Start is called before the first frame update
    protected bool inside=false; 
    void Update(){
        if(inside&&Input.GetKeyDown(KeyCode.K)){
            transform.parent.GetComponent<ChestScript>().Open();
        }
    }
    protected void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag=="Player")
            inside = true; 
    }
    protected void OnTriggerExit2D(Collider2D collider){
        if(collider.tag=="Player")
            inside =false; 
    }

    // Update is called once per frame
    
}
