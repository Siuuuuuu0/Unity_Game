using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using NavMeshPlus.Components;
using UnityEngine.AI;
//using Unity.AI.Navigation;
public class DoorController : MonoBehaviour

{
    // Start is called before the first frame update
    Animator animator;
    BoxCollider2D DoorCollider;
    GameObject NavMesh; 
    int idx;
    public CDirection cDirection;
    void Start(){
        DoorCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        NavMesh = GameObject.FindWithTag("Player").GetComponent<PlayerObject>().NavMesh;  
        idx = MapData.Add(gameObject, cDirection);

        //DoorCollider.enabled=false;
    }
    public void Open(){
        animator.SetTrigger("Open");
        MapData.doors[idx][2] = 1;
    }
    public void OpenAfterSave(){
        animator.SetTrigger("OpenSave");
    }
    public void Opened(){
        animator.SetTrigger("Opened");
        makeTraversable();
        setTraversable();
    }
    public void makeTraversable(){
        DoorCollider.enabled = false;
    }
    public void setTraversable(){
        //GetComponent<NavMeshObstacle>().enabled=false;
        GetComponent<NavMeshModifier>().overrideArea=false;
        // NavMesh.GetComponent<NavMeshGenerator>().UpdateNavMeshWithinBounds(new Bounds(transform.position, new Vector3(5f, 5f, 5f)));
        //NavMesh.GetComponent<NavMeshGenerator>().Generate();
        //Bounds recalcBounds = new Bounds(transform.position, new Vector3(5f, 5f, 5f));
        //NavMesh.GetComponent<NavMeshGenerator>().GenerateAsync(recalcBounds);

    }
}