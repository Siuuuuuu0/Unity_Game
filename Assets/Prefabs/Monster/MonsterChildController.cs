// using System;
// using System.Collections;
// using System.Collections.Generic;
// using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class MonsterChildController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject monster;
    private MonsterController Controller;
    public bool inside;
    NavMeshAgent agent;
    FieldOfViewChild fieldOfView; 
    public bool search;
    public bool attacked;
    private AttackController attackController; 

    public bool Liberated = false;
    

    void Start()
    {

        monster = transform.parent.gameObject;
        Controller = transform.parent.gameObject.GetComponent<MonsterController>();
        agent = transform.parent.gameObject.GetComponent<NavMeshAgent>();
        fieldOfView = transform.parent.gameObject.GetComponentInChildren<FieldOfViewChild>();
        attackController = transform.parent.GetComponent<AttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(inside&&fieldOfView.canSeePlayer){
            search =false;
            if(Controller.WaitUntilNextAttack<=0||!Liberated)
                Controller.monsterState = MonsterGameState.InitialAttack;
            else 
                Controller.monsterState = MonsterGameState.Attack;
        }
        else if(inside){
            search =true;
            Controller.monsterState = MonsterGameState.Search;
        }

        // else
        //     Controller.monsterState = MonsterGameState.Chase;
        
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag=="Player"){
            //attacked =false;
            search =false;
            inside = true;
            Vector2 dir = agent.transform.position - collider.transform.position; 
            if(Mathf.Sqrt(Mathf.Pow(dir.x, 2)+Mathf.Pow(dir.y, 2))<1.5&&fieldOfView.canSeePlayer){
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
                search =false;
                //attacked =true;
                //search =false;
            }
            else if(Mathf.Sqrt(Mathf.Pow(dir.x, 2)+Mathf.Pow(dir.y, 2))<3){
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
                search = true;
            }
            //agent.isStopped = true;
            //CalculateCirclePosition(collider);
            //monster.transform.position = 
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        if(collider.tag=="Player"){
            inside = false;
            search =false;
            Controller.monsterState= MonsterGameState.Chase;
            agent.isStopped = false;
            attackController.attacked = false;

            
        }
    }
    // private void CalculateCirclePosition(Collider2D collider){
    //     Vector3 movementDirection = agent.velocity.normalized;
    //     float angle = Mathf.Atan2(movementDirection.z, movementDirection.x) * Mathf.Rad2Deg;
    //     //agent.transform.position = collider.transform.position + new Vector3(1,1,1) *angle/180;
    //     //Debug.DrawLine(collider.transform.position + new Vector3(1,1,1) *angle/180, agent.transform.position);
    //     // Vector2 directionToTarget = (collider.transform.position - transform.position).normalized;
    //     // float angleRad = Mathf.Deg2Rad*angleInDegrees; 
    //     // float x = collider.transform.position.x+2*Mathf.Cos(angleRad); 
    //     // float y = collider.transform.position.y +2* Mathf.Sin(angleRad);
    //     // return new Vector3(x, y, agent.transform.position.z );
    // }
    
}
