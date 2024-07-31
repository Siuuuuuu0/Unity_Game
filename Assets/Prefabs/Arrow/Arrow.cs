
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    private bool launch = false; 
    private Vector2 vector; 
    public float speed=7.5f;
    int coord;  
    CDirection dir; 
    public void Launch(Vector2 vector, CDirection dir, int coord){
        switch(dir){
            case CDirection.NORTH : transform.eulerAngles = transform.eulerAngles-new Vector3(0, 0, 45); break;
            case CDirection.EAST : transform.eulerAngles = transform.eulerAngles+new Vector3(0, 0, 45); break;
            case CDirection.SOUTH : transform.eulerAngles = transform.eulerAngles+new Vector3(0, 0, 135); break;
            default :transform.eulerAngles = transform.eulerAngles-new Vector3(0, 0, 135); break;

        }
        this.dir=dir; 
        launch = true; 
        this.vector = vector; 
        this.coord = coord; 
    }

    // Update is called once per frame
    void Update()
    {
        if(launch)
            transform.position = new Vector2(transform.position.x, transform.position.y)+vector*(Time.deltaTime*speed); 
        switch(dir){
            case CDirection.WEST: if(transform.position.x>coord*11+11) Destroy(transform.gameObject);break;
            case CDirection.EAST: if(transform.position.x<coord*11) Destroy(transform.gameObject);break;
            case CDirection.NORTH: if(transform.position.y>coord*11+11) Destroy(transform.gameObject);break;
            case CDirection.SOUTH: if(transform.position.y<coord*11) Destroy(transform.gameObject);break;

        }
    }
}
