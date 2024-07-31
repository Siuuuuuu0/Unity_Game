using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;
    public float fire=0; 
    public int medKit=0; 
    public CellType type; 
    public bool isOpen=false;
    public bool key=false; 

    


    
}
