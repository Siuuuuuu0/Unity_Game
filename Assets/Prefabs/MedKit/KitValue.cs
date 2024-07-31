// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class KitValue : MonoBehaviour
{
    // Start is called before the first frame update
    private int Value;
    public bool added = false; 
    void Start()
    {
        Value = Random.Range(50, 250);
    }
    public int MedKit{
        get=>Value;
        set => Value = value; 
    }
    public void useKit(){
        GameObject.Find("Player").GetComponent<PlayerObject>().health+=Value; 
    }
    // Update is called once per frame
    void Update(){
        if(gameObject.activeSelf&&!added){
            // Debug.Log(true); 
            added = true; 
            Objects.Instance.Add(allItems.MedKit, gameObject); 
            
        }
    }
    
}
