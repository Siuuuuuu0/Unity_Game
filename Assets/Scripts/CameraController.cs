using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraController Instance; 
    [SerializeField]
    private GameObject Camera1; 
    [SerializeField]
    private GameObject Camera2; 
    void Awake(){
        Instance = this; 
    }
    void Start ()
    {
        // Instance =this; 
        Camera1.SetActive(true);
        Camera2.SetActive(false);
        
        
    }
    void Update(){
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     Camera2.SetActive(true);
        //     Camera1.SetActive(false);
        // }
    }
    public void Zoom(int zoomscale){
        switch(zoomscale){
            case 1: 
                Camera1.SetActive(true);
                Camera2.SetActive(false);
                break;
            case 2:
                Camera2.SetActive(true);
                Camera1.SetActive(false);
                break;
            default: break;
            
        }
    }
}
