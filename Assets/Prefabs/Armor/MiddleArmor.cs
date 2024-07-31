using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleArmor : ArmorScript
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerObject>();
        resistance = 15f; 
        multiplier = 1.5f;
        order =1; 
        type = ArmorType.Medium;
        transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    
}