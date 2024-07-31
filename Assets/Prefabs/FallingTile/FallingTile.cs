using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class FallingTile : MonoBehaviour
{
    // Start is called before the first frame update
    private float resistance;
    private Animator animator;
    void Start(){
        resistance= 10.0f;
        animator = GetComponent<Animator>();
    }
    public float Resistance{
        get=>resistance; 
        set{
            resistance=value; 
            if(resistance<10&&resistance>8) {
                animator.SetTrigger("anim1"); 
                
            }
            else if(resistance>4){
                animator.SetTrigger("anim2"); 
            }
            else if(resistance>0){
                animator.SetTrigger("anim3"); 
            }
            else {
                animator.SetTrigger("anim4");
            }

        }
    }
    
}
