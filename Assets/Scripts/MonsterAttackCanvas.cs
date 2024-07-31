// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    MonsterAttackPanel [] panels;
    public int loaded =0;
    void Start()
    {
        panels= GetComponentsInChildren<MonsterAttackPanel>();
        gameObject.SetActive(false);
        for(int i=0; i<10; i++){
            panels[i].SwitchToUnloaded();
        }
    }
    public void SwitchOn(){
        loaded =0;
        Debug.Log(true);
        gameObject.SetActive(true);
    }
    public void SwitchOff(){
        gameObject.SetActive(false);
        for(int i=0; i<10; i++){
            panels[i].SwitchToUnloaded();
        }
    }
    // Update is called once per frame
    public void Load(){
        panels[loaded++].SwitchToLoaded();
        // Debug.Log("Running");
    }
    public void UnloadAll(){
        for(int i =0; i<10; i++){
            panels[i].SwitchToUnloaded();
        }
    }
}
