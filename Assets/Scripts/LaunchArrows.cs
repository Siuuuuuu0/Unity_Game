using UnityEngine;

public class LaunchArrows : MonoBehaviour
{
    // Start is called before the first frame update
    private bool launched = false; 
    float update=0.0f; 
    public GameObject Arrow; 
    public int X; 
    public int Y; 

    void Start()
    {
        X = transform.parent.GetComponent<DoorLocation>().X; 
        Y = transform.parent.GetComponent<DoorLocation>().Y; 
    }

    // Update is called once per frame
    void Update()
    {
        if(launched){
            update+=Time.deltaTime; 
            if(update>60){
                launched = false;
            }
        }
    }
    public void Launch(){
        if(!launched){
            update=0.0f;
            launched = true;
            LaunchCoords();
            // update=0.0f;
            
        }
    }
    private void LaunchCoords(){
        Vector2 right = new Vector2(1, 0);
        Vector2 left = new Vector2(-1, 0);
        Vector2 up = new Vector2(0, 1);
        Vector2 down = new Vector2(0, -1);
        for(int i =1; i<9; i++){
            if(i!=4){
                Instantiate(Arrow, new Vector2(X*11+1.5f, Y*11+i+0.5f), Quaternion.identity).GetComponent<Arrow>().Launch(right, CDirection.WEST, X);
                Instantiate(Arrow, new Vector2(X*11+8.5f, Y*11+i+0.5f), Quaternion.identity).GetComponent<Arrow>().Launch(left, CDirection.EAST, X);
                Instantiate(Arrow, new Vector2(X*11+i+0.5f, Y*11+1.5f), Quaternion.identity).GetComponent<Arrow>().Launch(up, CDirection.NORTH, Y);
                Instantiate(Arrow, new Vector2(X*11+i+0.5f, Y*11+8.5f), Quaternion.identity).GetComponent<Arrow>().Launch(down, CDirection.SOUTH, Y);
            }
        }
    }
}
