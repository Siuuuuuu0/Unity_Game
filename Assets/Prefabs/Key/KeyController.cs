// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Animations;

public class KeyController : MonoBehaviour
{
    // Start is called before the first frame update
    public allItems item = allItems.Keys;
    public bool added=false; 
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag=="Player"){
            // PlayerObject playerObject = collision.GetComponent<PlayerObject>();
            // playerObject.Key++;
            if(InventoryManager.Instance.Add(transform.gameObject, allItems.Keys)){
                Transform parentObject = transform.parent; 
                // Destroy(parentObject.gameObject);
                parentObject.gameObject.SetActive(false);
            }
            //Debug.Log("Running");
        }
    }
    void Update(){
        if(gameObject.activeSelf&&!added){
            // Debug.Log(true); 
            added = true; 
            Objects.Instance.Add(allItems.Keys, gameObject); 
            
        }
    }
}
