// using System.Collections;
// using System.Collections.Generic;
// using System.Numerics;
// using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
// using UnityEngine.InputSystem;

public class MonsterController : MonoBehaviour
{
    //Start is called before the first frame update
    
    [SerializeField]
    int number;
    private GameObject target;
    private NavMeshAgent agent;
    [SerializeField]
    public MonsterGameState monsterState;
    private Chase chase;
    private RandomMovement randomMovement;
    private Rigidbody2D rb; 
    private Animator animator;
    private new SpriteRenderer renderer;
    private UnityEngine.Vector3 movementDirection;
    private AttackController attackController;
    private FieldOfViewChild fieldOfView;
    private SearchState searchState;
    private Transform FOVchild;
    private float monsterBaseSpeed;
    private float updatedSpeed;
    private UnityEngine.Vector3 originalPosition;
    // private float update; 
    // public float updateOffset;
    private InitialAttack initialAttack;
    public float WaitUntilNextAttack = 0;
    private bool continueChasing=false;
    float chasingCountdown =3;
    // public MonsterAttackPanel attackPanel;
    void Start(){

        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        agent.updateRotation=false;
        agent.updateUpAxis=false;
        monsterState = MonsterGameState.Patrolling;
        chase = GetComponent<Chase>();
        randomMovement = GetComponent<RandomMovement>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        attackController = GetComponent<AttackController>();
        // fieldOfView = GetComponent<FieldOfView>();
        fieldOfView = transform.GetComponentInChildren<FieldOfViewChild>();
        searchState = GetComponent<SearchState>();
        FOVchild = transform.Find("FOVController");

        monsterBaseSpeed = agent.speed;
        updatedSpeed = monsterBaseSpeed * 5;

        originalPosition = transform.position;
        initialAttack = GetComponent<InitialAttack>();
        // update = 0.0f;
        // updateOffset = 1;


    }
    void Update(){
        if(monsterState!=MonsterGameState.InitialAttack) WaitUntilNextAttack -=Time.deltaTime;
        if(WaitUntilNextAttack<0) WaitUntilNextAttack=0;
        // if(Input.GetKeyDown(KeyCode.H)){
        //     monsterState = MonsterGameState.Search;
        // }

        // if(Input.GetKeyDown(KeyCode.H)){
        //     monsterState = MonsterGameState.Attack; 
        // }
        //update +=Time.deltaTime;

        switch(monsterState){
            case MonsterGameState.Patrolling: 
            {
                //update = 0.0f;
                agent.speed = monsterBaseSpeed;
                randomMovement.Move(agent, agent.transform, 30); 
                if(fieldOfView.canSeePlayer)
                    monsterState = MonsterGameState.Chase;
                break;
            }
            case MonsterGameState.Chase : 
            {
                // Debug.Log("chasing");
                if(continueChasing){
                    if(chasingCountdown<0){
                        continueChasing=false; 
                        chasingCountdown=3;
                        break;
                    }
                    chase.ChasePlayer(agent, target.transform);
                    agent.speed = updatedSpeed;
                    chasingCountdown-=Time.deltaTime;
                    break;
                }
                if(!fieldOfView.canSeePlayer)
                    //randomMovement.Move(agent, agent.transform, 30);
                    {   
                    continueChasing=true;
                    agent.speed = monsterBaseSpeed;
                    // SearchForPlayer();
                    agent.ResetPath();
                    monsterState = MonsterGameState.Patrolling;
                    }
                else 
                {
                    chase.ChasePlayer(agent, target.transform);
                    agent.speed = updatedSpeed;
                }
                
                // else if(fieldOfView.canSeePlayer){
                //     update = 0.0f;
                // }
                break;
            }
            case MonsterGameState.Attack: 
            {
                // update = 0.0f;
                attackController.Attack(); 
                //monsterState = MonsterGameState.Chase; 
                break;
            }
            case MonsterGameState.Search:
            {
                if(searchState.running)
                    break;
                else{
                    searchState.Search(target);
                    break;
                }
            }
            case MonsterGameState.InitialAttack :
                WaitUntilNextAttack =10;
                if(GetComponentInChildren<MonsterChildController>().Liberated)
                    initialAttack.InitialMonsterAttack();
                // WaitUntilNextAttack -=Time.deltaTime;
                
            break;
        }



        movementDirection = agent.velocity.normalized;
        if (movementDirection != UnityEngine.Vector3.zero)
        {
            float angle = Mathf.Atan2(movementDirection.z, movementDirection.x) * Mathf.Rad2Deg;
            //Debug.Log(angle);
            if(angle>180) angle-=180;
            if(angle>-90&&angle<90) {
                renderer.flipX = false;

            }
            else{
                renderer.flipX=true;
            }
            

            //Debug.Log("Prefab is moving in the direction: " + angle);
        }
        // else if(monsterState==MonsterGameState.Attack ||monsterState == MonsterGameState.Search){
        //     float angle = FOVchild.eulerAngles.z*Mathf.Rad2Deg;
        //     if(angle>180) angle-=180;
        //     if(angle>-90&&angle<90) {
        //         renderer.flipX = false;

        //     }
        //     else{
        //         renderer.flipX=true;
        //     }
        // }
        
        void SearchForPlayer()
        {

            float searchRadius = 50f; 
            UnityEngine.Vector3 randomDirection = Random.insideUnitSphere * searchRadius;
            randomDirection += originalPosition;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, searchRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            else
            {
                Debug.LogError("Failed to find a valid search position on NavMesh!");
            }
        }
    
    }
    
    
}
public enum MonsterGameState{
    Chase, 
    Patrolling, 
    Attack, 
    Search, 
    InitialAttack
}
