using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum  CDirection{
    NORTH=1,
    SOUTH=3,
    EAST=4,
    WEST=2
}
public static class CardinalDirection {
    
    
    public static string ToString(CDirection d) {
        switch (d) {
            case CDirection.NORTH:
                return "north";
            case CDirection.SOUTH:
                return "south";
            case CDirection.EAST:
                return "east";
            case CDirection.WEST:
                return "west";
            default:
                throw new Exception("Unexpected value" );
        }
    }
    public static CDirection getOposite(CDirection d){
        switch(d){

        case CDirection.NORTH: return CDirection.SOUTH; 
        case CDirection.SOUTH: return CDirection.NORTH; 
        case CDirection.EAST: return CDirection.WEST; 
        case CDirection.WEST: return CDirection.EAST;
        default : throw new Exception("Unexpected value");
        }
    }
    

}
