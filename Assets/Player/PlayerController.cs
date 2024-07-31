using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public float collisionOffset = 0.02f;
    Vector2 movementInput;
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Rigidbody2D rb;
    Animator animator;
    private bool CantMove;
    // SpriteRenderer spriteRenderer;
    

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    
    private void FixedUpdate(){
        //Debug.Log(Player.transform.position.x);
        if(movementInput!= Vector2.zero){
            bool success = TryMove(movementInput);
            if(!success) {
                success =TryMove(new Vector2(movementInput.x, 0));
            }
            if(!success) {
                success =TryMove(new Vector2(0, movementInput.y));
            }
            if(movementInput.x>0){
                animator.SetBool("isMovingRight", success);
                animator.SetBool("isMovingLeft", false);
            }
            else if(movementInput.x<0){
                animator.SetBool("isMovingRight", false);
                animator.SetBool("isMovingLeft", success);
            }
            else{
                animator.SetBool("isMovingRight", false);
                animator.SetBool("isMovingLeft", false);
            }
            if(movementInput.y>0){
                animator.SetBool("isMovingUp", success);
                animator.SetBool("isMovingDown", false);
            }
            else if(movementInput.y<0){
                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingDown", success);
            }
            else{
                animator.SetBool("isMovingDown", false);
                animator.SetBool("isMovingUp", false);
            }
            
        }
        else{
            animator.SetBool("isMovingDown", false);
            animator.SetBool("isMovingRight", false);
            animator.SetBool("isMovingLeft", false);
            animator.SetBool("isMovingUp", false);
        }
        
    }
    private bool TryMove(Vector2 direction){
        if(CantMove) return false;
        if(direction!=Vector2.zero) {
            int count = rb.Cast(
                    direction,
                    movementFilter, 
                    castCollisions, 
                    moveSpeed * Time.fixedDeltaTime + collisionOffset
                );
            if(count==0) {
                rb.MovePosition(rb.position+moveSpeed * Time.fixedDeltaTime * direction);
                return true;
            }
            return false;
        }
        else return false;
    }
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }
    public void cantMove(){
        CantMove=true;
    }
    public void CanMove(){
        CantMove=false;
    }
    
}
