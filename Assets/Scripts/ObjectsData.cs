using UnityEngine;
[System.Serializable]

public class ObjectsData 
{
    public float [][] objects; 
    public ObjectsData(){
        objects = Objects.Instance.ToSave(); 
    }
}
