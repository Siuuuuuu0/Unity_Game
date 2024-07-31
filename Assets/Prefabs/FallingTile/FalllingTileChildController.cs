using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalllingTileChildController : MonoBehaviour
{
    // Start is called before the first frame update
    private FallingFloor fallingFloor;
    void Start(){
        fallingFloor = transform.parent.GetComponent<FallingFloor>();
    }
    void OnTriggerExit2D(Collider2D collider){
        if(collider.tag=="Player"){
            fallingFloor.Exited();
        }
    }
}
