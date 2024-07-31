using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrow : MonoBehaviour
{
    // Start is called before the first frame update
    // public GameObject poison;
    public float damage; 
    void Start(){
        damage = 50; 
    }
    
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            GameObject.Find("Player").GetComponent<PlayerObject>().takeDamage(damage);
            Instantiate(InventoryManager.Instance.Poison, new Vector2(-10, -10), Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }
}
