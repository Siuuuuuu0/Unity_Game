using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitController : MonoBehaviour
{
    // Start is called before the first frame update
    public allItems item = allItems.MedKit;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag=="Player"){
            // PlayerObject playerObject = collision.GetComponent<PlayerObject>();
            
            Transform parentObject = transform.parent; 

            // playerObject.MedKit=parentObject.GetComponent<KitValue>().MedKit;
            if(InventoryManager.Instance.Add(parentObject.gameObject, allItems.MedKit)){
                parentObject.gameObject.SetActive(false);
            }
            // Destroy(parentObject.gameObject);
            // parentObject.gameObject.SetActive(false);
            //Debug.Log("Running");
        }
    }
}
