using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public abstract class Cell
{
    protected int x; 
    protected int y;
    protected bool key;
    protected float fire=0.0f;
    protected int medKit = 0;
    public Cell(int x, int y){
        X=x; 
        Y=y;
    }
    public Cell(int x, int y, bool key){
        X =x;
        Y=y;
        Key=key;
    }
    public Cell(int x, int y, float fire){
        X=x; 
        Y=y;
        Fire=fire;
    }
    public Cell(int x, int y, float fire, bool key){
        X=x; 
        Y=y;
        Fire=fire;
        Key =key;
    }

    public int X{
        get=> x;
        set {
            x=value;
        }
    }
    public int Y{
        get=> y;
        set {
            y=value;
        }
    }
    public bool Key{
        get=>key;
        set{
            key=value;
        }
    }
    public float Fire{
        get=>fire;
        set{
            if(value>=10.0f) fire=10.0f;
            else if(value<=0.0f) fire = 0.0f;
            else fire= value;
            // if(fire>0.0f) Debug.Log(fire);
        }
        
    }
    public int MedKit{
        get=>medKit;
        set{
            medKit=value;
        }
    }
    public void AddKit(int hp){
        medKit+=hp;
    }
    
    
    
}
