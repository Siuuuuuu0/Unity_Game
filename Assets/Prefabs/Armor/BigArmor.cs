using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigArmor : ArmorScript
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerObject>();
        resistance = 20f; 
        multiplier = 2f;
        order =2; 
        type = ArmorType.Big;
        transform.gameObject.SetActive(false);

    }

    // Update is called once per frame
    
}
