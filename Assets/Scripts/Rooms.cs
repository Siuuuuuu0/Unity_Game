using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using Unity.VisualScripting;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Dictionary<int, List<GameObject>> N;
    public Dictionary<int, List<GameObject>> S;
    public Dictionary<int, List<GameObject>> E;
    public Dictionary<int, List<GameObject>> W;

    public List<GameObject> radius0;
    public List<GameObject> radius1;
    public List<GameObject> radius1L;
    public List<GameObject> radius1U;
    public List<GameObject> radius1D;
    public List<GameObject> radius2;
    public List<GameObject> radius2L;
    public List<GameObject> radius2U;
    public List<GameObject> radius2D;
    public List<GameObject> radius3;
    public List<GameObject> radius3L;
    public List<GameObject> radius3U;
    public List<GameObject> radius3D;
    public List<GameObject> radius4;
    public List<GameObject> radius4L;
    public List<GameObject> radius4U;
    public List<GameObject> radius4D;
    public List<GameObject> radius5Up;
    public List<GameObject> radius5UpL;
    public List<GameObject> radius5UpU;
    public List<GameObject> radius5UpD;
    public List<GameObject> radiusEnd;
    public List<GameObject> radiusEndL;
    public List<GameObject> radiusEndU;
    public List<GameObject> radiusEndD;
    
    //public List<GameObject> Test;
    // void Awake(){
    //     Instance=this;
    // }
    void Awake()
    {
        S=new Dictionary<int, List<GameObject>>();
        N= new Dictionary<int, List<GameObject>>();
        W=new Dictionary<int, List<GameObject>>();
        E = new Dictionary<int, List<GameObject>>();
        //Instance = this;
        S.Add(0, radius0);
        S.Add(1, radius1D);
        S.Add(2, radius2D);
        S.Add(3, radius3D);
        S.Add(4, radius4D);
        S.Add(5, radius5UpD);
        S.Add(100, radiusEndD);
        N.Add(0, radius0);
        N.Add(1, radius1U);
        N.Add(2, radius2U);
        N.Add(3, radius3U);
        N.Add(4, radius4U);
        N.Add(5, radius5UpU);
        N.Add(100, radiusEndU);
        W.Add(0, radius0);
        W.Add(1, radius1);
        W.Add(2, radius2);
        W.Add(3, radius3);
        W.Add(4, radius4);
        W.Add(5, radius5Up);
        W.Add(100, radiusEnd);
        E.Add(0, radius0);
        E.Add(1, radius1L);
        E.Add(2, radius2L);
        E.Add(3, radius3L);
        E.Add(4, radius4L);
        E.Add(5, radius5UpL);
        E.Add(100, radiusEndL);
        // foreach(var pair in S){
        //     foreach(var value in pair.Value){
        //         Debug.Log("running");
        //     }
        // }
        
    }

    // Update is called once per frame
    
}
