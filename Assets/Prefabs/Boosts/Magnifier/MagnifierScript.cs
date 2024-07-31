using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class MagnifierScript : MonoBehaviour
{
    // Start is called before the first frame update
    // private bool activated =false;
    [SerializeField]
    public float time; 
    // private float multiplier; 
    // private float update;
    public allItems Item; 
    // private PixelPerfectCamera camera; 
    void Start()
    {
        time = 10; 
        // update =0.0f;
        Item = allItems.Magnifier;
        transform.gameObject.SetActive(false);
        // multiplier = 2;
        // camera = InventoryManager.Instance.Camera.GetComponent<PixelPerfectCamera>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(activated){
    //         update +=Time.deltaTime; 
    //         if(update>time){
    //             Finished(); 
    //         }
    //     }
    //     if(Input.GetKeyDown(KeyCode.Space)){
    //         Activate(); 
    //     }
    // }
    public void Activate(){
        GetComponent<BoostScript>().activated=true; 
        // activated =true;
        CameraController.Instance.Zoom(2);
        RunningBoosts.Instance.runningBoost.Add(transform.gameObject);
        RunningBoosts.Instance.boosts++; 
        // if(zoomScale!=0) 
        // Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize * 0.5f, 5, 100);
        // camera. 
        // camera.transform
        //GameObject.Find("Player").GetComponent<PlayerController>().moveSpeed*=multiplier; 
        // CameraController.Instance.Zoom(2);
    }
    public void Finished(){
        // activated =false;
        CameraController.Instance.Zoom(1);
        // camera.orthographicSize/=2; 
        //GameObject.Find("Player").GetComponent<PlayerController>().moveSpeed/=multiplier; 
        // CameraController.Instance.Zoom(0.5f);
        InventoryManager.Instance.useBoost(allItems.Magnifier, transform.gameObject);

    }
}
