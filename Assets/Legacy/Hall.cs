using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Hall : Cell
{
    
    // Start is called before the first frame update
    public Hall(int y, int x, float fire, bool key) : base(x, y, fire, key){

    }
    public Hall(int y, int x, float fire) : base(x, y, fire){
    }
    
}
