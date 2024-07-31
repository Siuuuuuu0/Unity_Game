using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class ArrowCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage; 
    void Start(){
        damage = 50; 
    }
    
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            GameObject.Find("Player").GetComponent<PlayerObject>().takeDamage(damage);
            Destroy(transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    
}
