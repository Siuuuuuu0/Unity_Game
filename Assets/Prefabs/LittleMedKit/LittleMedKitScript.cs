// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class LittleMedKitScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int littleMedKit; 
    public bool added=false; 
    public allItems item = allItems.LittleMedKit;
    void Start()
    {
        LittleMedKit = Random.Range(50, 100);
        transform.gameObject.SetActive(false);
    }
    public int LittleMedKit{
        set=>littleMedKit = value; 
        get =>littleMedKit;
    }
    public void useKit(){
        GameObject.Find("Player").GetComponent<PlayerObject>().health+=littleMedKit; 
    }
    void Update(){
        if(gameObject.activeSelf&&!added){
            // Debug.Log(true); 
            added = true; 
            Objects.Instance.Add(allItems.LittleMedKit, gameObject); 
            
        }
    }

    // Update is called once per frame
    
}
