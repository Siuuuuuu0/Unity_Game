using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallArmor : ArmorScript
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerObject>();
        resistance = 10f; 
        multiplier = 1.25f;
        order =0; 
        type = ArmorType.Small;
        transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    
}
