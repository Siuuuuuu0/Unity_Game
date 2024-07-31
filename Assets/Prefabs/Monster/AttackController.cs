// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AI; 

public class AttackController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private NavMeshAgent agent;
    public bool attacked;  
    private MonsterHitbox monsterHitbox; 
    private float update=0;
    private bool canAttack=true;
    void Start(){
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        attacked=false;
        monsterHitbox = GetComponentInChildren<MonsterHitbox>();
    }
    public void Attack(){
        if(canAttack){
            animator.SetBool("Attack", true);
            monsterHitbox.Damage();
            attacked=true;
            canAttack=false;
            // Debug.Log("Attacking");
        }
        

    }
    private void FinishAttack(){
        animator.SetBool("Attack", false);
        attacked=false;
    }
    void Update(){
        update-=Time.deltaTime;
        if(update<=0){
            update =2; canAttack =true;
        }
    }
    
}
