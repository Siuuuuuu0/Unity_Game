using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    // private GameObject monster; 
    // private MonsterController controller;
    private new Collider2D collider; 
    private GameObject player; 
    public float Xsize; 
    public float Ysize;
    private float colliderArea; 
    public float length; 
    public float height; 
    private Collider2D playerCollider; 
    public float baseDamage=2; 
    private FieldOfViewChild fieldOfViewChild; 
     
    void Start(){
        // monster = transform.parent.gameObject; 
        // controller= monster.GetComponent<MonsterController>();
        collider = GetComponent<Collider2D>();
        player = GameObject.Find("Player");
        Xsize = collider.bounds.size.x; 
        Ysize= collider.bounds.size.y; 
        // Debug.Log(Xsize+" "+ Ysize);
        colliderArea = Xsize*Ysize; 
        // playerCollider = player.GetComponentInChildren<CapsuleCollider2D>();
        
        fieldOfViewChild = transform.parent.GetComponentInChildren<FieldOfViewChild>();
        playerCollider = player.transform.GetChild(0).GetComponent<BoxCollider2D>();
        length = playerCollider.bounds.size.x; 
        height = playerCollider.bounds.size.y;
        
        

    }
    void Update(){
         
        Quaternion rotation = fieldOfViewChild.lastRotation;
        Vector3  curentEulerAngles = rotation.eulerAngles; 
        curentEulerAngles.z+=80; 
        rotation.eulerAngles = curentEulerAngles; 
        transform.rotation=rotation; 
        
    }
    public void Damage(){
        float damage = baseDamage* CalculateAreaPercentage(); 
        player.GetComponent<PlayerObject>().takeDamage(damage);
        
        // Debug.Log(damage);
        //Debug.Log(damage);
    }
    // private float CalculateAreaPercentage(){
    //     //if(LayerMask.NameToLayer("PlayerCollider")!=-1) Debug.Log("exists");
    //     //Collider2D
    //     // Bounds boundsA = a.bounds; 
    //     // Bounds boundsB = b.bounds; 
    //     //Debug.Log(collider.bounds.center.x +" "+collider.bounds.center.y);
    //     Collider2D overlappingArea = Physics2D.OverlapArea(new Vector2(collider.bounds.center.x - Xsize/2, collider.bounds.center.y-Ysize/2), 
    //     new Vector2(collider.bounds.center.x + Xsize/2, collider.bounds.center.y+Ysize/2), LayerMask.GetMask("PlayerCollider"));
    //     // 
    //     //float overlapArea = GeometryUtility.CalculateOverlapArea(capsuleCollider, childCollider);
    //     Vector2 size; 
    //     if(overlappingArea!=null)
    //         size = overlappingArea.bounds.size;
    //     else 
    //         size = Vector2.zero; 
    //     return size.x*size.y/colliderArea;
    // }
    private float CalculateAreaPercentage(){
        Vector2 l1 = new Vector2( playerCollider.bounds.center.x - length/2, playerCollider.bounds.center.y - height/2); 
        Vector2 r1 = new Vector2( playerCollider.bounds.center.x + length/2, playerCollider.bounds.center.y + height/2);
        Vector2 l2 = new Vector2( collider.bounds.center.x - Xsize/2, collider.bounds.center.y - Ysize/2); 
        Vector2 r2 = new Vector2( collider.bounds.center.x + Xsize/2, collider.bounds.center.y + Ysize/2);
        return OverlapArea(l1, r1, l2, r2)/colliderArea; 
    }
    // private Vector2 OverlapArea(Collider2D a, Collider2D b)
    // {
    // // get the bounds of both colliders
    //     var boundsA = a.bounds;
    //     var boundsB = b.bounds;

    //     // first heck whether the two objects are even overlapping at all
    //     if(!boundsA.Intersects(boundsB))
    //     {
    //         return Vector2.zero;
    //     }

    //     // now that we know they at least overlap somehow we can calculate

    //     // get the bounds of both colliders

    //     // get min and max point of both
    //     var minA = boundsA.min; 
    //     var maxA = boundsA.max; 

    //     var minB = boundsB.min;
    //     var maxB = boundsB.max;

    //     // we want the smaller of the max and the higher of the min points
    //     var lowerMax = Vector2.Min(maxA, maxB);
    //     var higherMin = Vector2.Max(minA, minB);

    //     // the delta between those is now your overlapping area
    //     return lowerMax - higherMin;
    // }
    // private float CalculateDamagePercentage(){
    //     Vector2 area = OverlapArea(collider, playerCollider); 
    //     return area.x*area.y/colliderArea; 
    // }
    private float OverlapArea(Vector2 l1, Vector2 r1, Vector2 l2,
                               Vector2 r2){
        float x_dist
            = Mathf.Min(r1.x, r2.x) - Mathf.Max(l1.x, l2.x);
        float y_dist
            = Mathf.Min(r1.y, r2.y) - Mathf.Max(l1.y, l2.y);
        float areaI = 0;
        if (x_dist > 0 && y_dist > 0) {
            areaI = x_dist * y_dist;
        }
        return areaI; 
    }
}
