// using System;
// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class LittleChestScript : ChestScript
{
    void Start()
    {
        // littleMedKit = InventoryManager.Instance.LittleMedKit; 
        // speedBoost = InventoryManager.Instance.SpeedBoost; 
        // invisibility = InventoryManager.Instance.Invisibility; 
        // magnifier = InventoryManager.Instance.Magnifier;
        littleMedKit = InventoryManager.Instance.LittleMedKit; 
        speedBoost = InventoryManager.Instance.SpeedBoost; 
        invisibility = InventoryManager.Instance.Invisibility; 
        magnifier = InventoryManager.Instance.Magnifier;
        Armor = InventoryManager.Instance.Armor; 
        MiddleArmor = InventoryManager.Instance.MiddleArmor; 
        SmallArmor = InventoryManager.Instance.SmallArmor; 
        BigArmor =InventoryManager.Instance.BigArmor;
        big=true;
        animator = GetComponent<Animator>();
        big = false;
        coordinates = new Vector2(transform.localPosition.x-3, transform.localPosition.y-3); coordinates = transform.position;
        // animator = GetComponent<Animator>();
        itemsList = new List<allItems>{
            allItems.LittleMedKit, 
            allItems.SpeedBoost, 
            allItems.LittleMedKit, 
            allItems.LittleMedKit, 
            allItems.Armor
        };
        count = Random.Range(2, itemsList.Count/2);
        addItems();
    }
    
}
