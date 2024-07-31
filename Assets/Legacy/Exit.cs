using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Cell
{
    // Start is called before the first frame update
    public Exit(int y, int x) : base(x, y){}
    public Exit(int y, int x, int fire) : base(x, y, fire){}

}
