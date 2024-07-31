using System;
using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// using UnityEngine.U2D;

public class FieldOfViewChild : MonoBehaviour
{
    public float radius; 
    [Range(0, 360)]
    public float angle; 
    public GameObject playerRef; 
    public LayerMask targetMask ; 
    public LayerMask obstructionMask; 
    private NavMeshAgent agent; 
    public Quaternion lastRotation;
    private MonsterController monsterController;
    public bool canSeePlayer {get; private set;}
    private void Start(){
        playerRef = GameObject.FindGameObjectWithTag("Player");
        targetMask = LayerMask.GetMask("Player");
        obstructionMask = LayerMask.GetMask("Collisions");
        radius = 7;
        angle = 160;
        agent = transform.parent.gameObject.GetComponent<NavMeshAgent>();
        monsterController = transform.parent.gameObject.GetComponent<MonsterController>();;
        StartCoroutine(FOVRoutine());
        
    }
    private IEnumerator FOVRoutine(){
        float delay = 0.2f; 
        WaitForSeconds wait = new WaitForSeconds(delay); 
        while(true){
            yield return wait; 
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {   
        
            Quaternion rotation;
            if(monsterController.monsterState==MonsterGameState.Search){
                rotation = transform.rotation;
                lastRotation = rotation;
            }
            else if(agent.velocity == Vector3.zero){
                rotation = lastRotation;
                transform.rotation = rotation;
            }
            else{
                rotation = AgentRotation();
                lastRotation=rotation;
                transform.rotation = rotation;
            }
            if(playerRef.GetComponent<PlayerObject>().invisible){
            canSeePlayer = false;
            }
            else{
                Collider2D [] rangeChecks = 
                    Physics2D.OverlapCircleAll(transform.position, radius, targetMask); 
                if(rangeChecks.Length!=0){
                    Transform target = rangeChecks[0].transform;
                    Vector2 directionToTarget = (target.position - transform.position).normalized;
                    if(Vector2.Angle(transform.up, directionToTarget)< angle/2) {
                    // if(Vector2.Angle(agent.velocity, directionToTarget)< angle/2) {
                        float distanceToTarget = Vector2.Distance(transform.position, target.position);
                        if(!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) 
                            {
                            canSeePlayer= true; 
                            // Debug.Log(true);
                            }
                        else 
                            canSeePlayer = false;
                    }
                    else 
                        canSeePlayer = false; 
                }
                else if(canSeePlayer) 
                    canSeePlayer = false;
            }
        

    }
    
    private void OnDrawGizmos(){
        Gizmos.color = Color.white; 
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
        try {
            Quaternion rotation = transform.rotation;
            // Vector3 angle1 = DirectionFromAngle(-agent.velocity.z, -angle/2);
            Vector3 angle1 = DirectionFromAngle(-rotation.eulerAngles.z, -angle/2);
            Vector3 angle2 = DirectionFromAngle(-rotation.eulerAngles.z, angle/2);
            // Vector3 angle2 = DirectionFromAngle(-agent.velocity.z, angle/2);
            Gizmos.color= Color.yellow; 
            Gizmos.DrawLine(transform.position, transform.position+angle1*radius);
            Gizmos.DrawLine(transform.position, transform.position+angle2*radius);
            if(canSeePlayer){
                Gizmos.color= Color.green; 
                Gizmos.DrawLine(transform.position, playerRef.transform.position);
            }
        } catch (Exception){}
    }
    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees){
        angleInDegrees+=eulerY; 
        return new Vector2(Mathf.Sin(angleInDegrees*Mathf.Deg2Rad), Mathf.Cos(angleInDegrees*Mathf.Deg2Rad));
    }
    private Quaternion AgentRotation(){
        
            var vel = agent.velocity; 
            vel.z = 0; 
            Quaternion res; 
            //if(vel!=Vector3.zero){
            res = Quaternion.LookRotation(Vector3.forward, vel);
            return res;
        
            //if I need to transform.rotation = ....;
        //}
        
        
    }
}
