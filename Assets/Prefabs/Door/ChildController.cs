using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    // Start is called before the first frame update
    bool opened = false;
    // bool canOpen = false;
    //PlayerObject playerObject;
    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag=="Player"&&!opened){
            //playerObject = collision.GetComponent<PlayerObject>();
            // if(InventoryManager.Instance.getKeys()) canOpen = true; 
            collision.gameObject.GetComponent<PlayerObject>().canUseKey=true;
            collision.gameObject.GetComponent<PlayerObject>().door = this; 
        }
    }
    public void OnTriggerExit2D(Collider2D collision){
        if(collision.tag=="Player") {
            // canOpen=false;
            collision.gameObject.GetComponent<PlayerObject>().canUseKey=true;
            collision.gameObject.GetComponent<PlayerObject>().door=null;
        }
    }
    // public void Update(){
    //     // if(Input.GetKeyDown(KeyCode.K)&&canOpen) {
    //     //     setOpen();
    //     // };
    // }
    public void setOpen(){
        if(!opened){
            GameObject parentobject = transform.parent.gameObject;
            // InventoryManager.Instance.useKey(parentobject);
            parentobject.GetComponent<DoorController> ().Open();
            opened = true;
        }
    }

}
