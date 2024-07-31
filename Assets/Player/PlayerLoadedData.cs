using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadedData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        if(LoadedData.loadOccured)
            transform.position = LoadedData.PlayerPosition(); 
    }
}
