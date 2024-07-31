using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : Cell
{
    protected bool open;
    public Door(int y, int x, bool open) : base(x, y){
        Open=open;
    }
    public bool Open{
        get=>open;
        set{
            open=value;
        }
    }


}
    
    

