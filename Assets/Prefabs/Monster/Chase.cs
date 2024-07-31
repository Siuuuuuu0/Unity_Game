using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    
    
    public void ChasePlayer(NavMeshAgent agent, Transform target){
        //agent.isStopped=false;
        StartCoroutine(DelayBeforeMoving());
        agent.SetDestination(target.position);
    }

        IEnumerator DelayBeforeMoving()
    {
        // Wait for 1 second before starting to move
        yield return new WaitForSeconds(1f);
    }
    
        


    // Update is called once per frame
      
    
}
