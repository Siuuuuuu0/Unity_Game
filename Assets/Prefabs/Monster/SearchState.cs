using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEditor.PackageManager.Requests;


//using System.Numerics;
using UnityEngine;
using UnityEngine.AI; 

public class SearchState : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent; 
    private FieldOfViewChild fieldOfView; 
    public bool running=false;
    public float rotationSpeed; 
    private Transform fovChild;
    private MonsterController monsterController; 
    private MonsterChildController monsterChildController;
    private AttackController attackController; 

    
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponentInChildren<FieldOfViewChild>();
        rotationSpeed = 2160f;
        fovChild =transform.Find("FOVController");
        monsterController = GetComponent<MonsterController>();
        attackController = GetComponent<AttackController>();
        monsterChildController = GetComponentInChildren<MonsterChildController>();
    }
    public void Search(GameObject player){
        //agent.angularSpeed = 0f; 
        running =true;
        StartCoroutine(SRoutine(player));
    } 
    private IEnumerator SRoutine(GameObject player){
        //running =true;
        float delay = 0.1f; 
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        //int rotations = 0; 
        WaitForSeconds wait = new WaitForSeconds(delay); 
        
        
        while(true){
            // count ++;
            if(!running) yield break;
            float newRotation = fovChild.transform.eulerAngles.z + rotationSpeed * Time.deltaTime;

        // Apply the new rotation to the agent's transform
            
            Vector2 dir = agent.transform.position - player.transform.position;
            if(Mathf.Sqrt(Mathf.Pow(dir.x, 2)+Mathf.Pow(dir.y, 2))<fieldOfView.radius){
                // float newRotation = agent.transform.eulerAngles.z + 10 * Time.deltaTime;
                // // Apply the new rotation to the agent's transform
                // agent.velocity = new Vector3(0, 0, newRotation);
                // float angularSpeed = 30*Mathf.Rad2Deg;
                // agent.angularSpeed += angularSpeed;
                // agent.transform.position
                
                fovChild.rotation = Quaternion.Euler(0f, 0f, newRotation);
                if (newRotation>= 360f){
                    agent.isStopped = false;
                    running=false;
                    monsterChildController.search=false;
                    monsterChildController.inside=false;
                    monsterController.monsterState = MonsterGameState.Patrolling;
                    yield break;

                }
                else if(fieldOfView.canSeePlayer){
                    //monsterController.monsterState = MonsterGameState.Attack;
                    // agent.isStopped = false;
                    // running=false;
                    //monsterChildController.search =false;
                    running =false;
                    //monsterChildController.search=false;
                    yield break;
                }
                
                // else if(!fieldOfView.canSeePlayer&&attackController.attacked){
                //     attackController.attacked=false;
                //     running =false;
                //     monsterChildController.search=false;
                //     monsterChildController.inside =false;
                //     yield break;
                // }
                // else if(!fieldOfView.canSeePlayer){
                //     running = false; 
                //     monsterChildController.search=false;
                //     monsterChildController.inside=false;
                //     monsterController.monsterState = MonsterGameState.Patrolling;
                //     yield break;
                // }
                
                yield return wait;
            }
            else {
                running=false;
                agent.isStopped=false;
                yield break;
            }
             
            
        }
        
        
    }
}
