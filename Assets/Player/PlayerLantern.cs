
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLantern : MonoBehaviour
{
    // Start is called before the first frame update
    // public bool running=false; 
    public GameObject[] lights; 
    public bool right; 
    public bool left; 
    public bool up; 
    public bool down; 
    //NWSE
    void Start(){
        foreach(var light in lights){
            light.GetComponent<Light2D>().enabled=false;
        }
        // GetComponent<Light2D>().enabled=false;
        // lights[0].GetComponent<Light2D>().enabled=false;
        // lights[1].GetComponent<Light2D>().enabled=false;
        // lights[2].GetComponent<Light2D>().enabled=false;
        // lights[3].GetComponent<Light2D>().enabled=false;
    }
    // void Update(){
    //     Activate();
    //     if(right)
    //         LightRight(); 
    //     if(left)
    //         LightLeft();
    //     if(up)
    //         LightUp(); 
    //     if(down)
    //         LightDown();
        
    // }
    public void Activate(){
        // running=true; 
        GetComponentInParent<PlayerObject>().light=true; 
    }
    public void Deactivate(){
        // running=false; 
        lights[0].GetComponent<Light2D>().enabled=false;
        lights[2].GetComponent<Light2D>().enabled=false;
        lights[3].GetComponent<Light2D>().enabled=false;
        lights[1].GetComponent<Light2D>().enabled=false;
        GetComponentInParent<PlayerObject>().light=false; 
    }
    public void LightRight(){
        // if(running){
            lights[0].GetComponent<Light2D>().enabled=false;
            lights[2].GetComponent<Light2D>().enabled=false;
            lights[3].GetComponent<Light2D>().enabled=false;
            lights[1].GetComponent<Light2D>().enabled=true;
        // }
    }
    public void LightLeft(){
        // if(running){
            lights[0].GetComponent<Light2D>().enabled=false;
            lights[2].GetComponent<Light2D>().enabled=false;
            lights[1].GetComponent<Light2D>().enabled=false;
            lights[3].GetComponent<Light2D>().enabled=true;
        // }
    }
    public void LightUp(){
        // if(running){
            lights[1].GetComponent<Light2D>().enabled=false;
            lights[2].GetComponent<Light2D>().enabled=false;
            lights[3].GetComponent<Light2D>().enabled=false;
            lights[0].GetComponent<Light2D>().enabled=true;
        // }
    }
    public void LightDown(){
        // if(running){
            lights[0].GetComponent<Light2D>().enabled=false;
            lights[1].GetComponent<Light2D>().enabled=false;
            lights[3].GetComponent<Light2D>().enabled=false;
            lights[2].GetComponent<Light2D>().enabled=true;
        // }
    }
}
