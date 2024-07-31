// using System;
// using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
public class ChestScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<allItems, List<GameObject>> items = new Dictionary<allItems, List<GameObject>>();
    // GameObject key; 
    // GameObject medKit; 
    protected GameObject littleMedKit; 
    protected GameObject speedBoost; 
    protected Animator animator; 
    public bool opened=false; 
    public List<allItems> itemsList;
    protected Vector2 coordinates; 
    protected GameObject invisibility ; 
    protected GameObject magnifier ; 
    protected GameObject Armor; 
    protected GameObject MiddleArmor; 
    protected GameObject BigArmor; 
    protected GameObject SmallArmor; 
    protected GameObject strength; 
    [SerializeField]
    public int count;
    // float count2 = 49f;
    // float timer=5f;
    protected bool big; 
    // private bool added=false; 
    void Awake(){
        animator = GetComponent<Animator>(); 
    }
    void Start()
    {
        littleMedKit = InventoryManager.Instance.LittleMedKit; 
        speedBoost = InventoryManager.Instance.SpeedBoost; 
        invisibility = InventoryManager.Instance.Invisibility; 
        magnifier = InventoryManager.Instance.Magnifier;
        Armor = InventoryManager.Instance.Armor; 
        MiddleArmor = InventoryManager.Instance.MiddleArmor; 
        SmallArmor = InventoryManager.Instance.SmallArmor; 
        BigArmor =InventoryManager.Instance.BigArmor;
        strength = InventoryManager.Instance.Strength; 
        big=true;
        // animator = GetComponent<Animator>();
        coordinates = transform.position; 
        // Debug.Log(coordinates.ToString());
        
        itemsList = new List<allItems>{
            allItems.LittleMedKit, 
            allItems.SpeedBoost, 
            allItems.Invisibility, 
            allItems.Magnifier, 
            allItems.Strength, 
            // allItems.Armor, 
            // allItems.Armor, 
            // allItems.Armor, 
            // allItems.Armor, 
            allItems.Armor
        };
        Objects.Instance.Add(allItems.Chest, gameObject); 
        if(!LoadedData.loadOccured){
            count = Random.Range(2, itemsList.Count/2);
            addItems();

            foreach(var pair in items){
                foreach(var item in pair.Value){
                    switch(pair.Key){
                        case allItems.Keys : item.GetComponent<KeyController>().added=true; break; 
                        case allItems.MedKit : item.GetComponent<KitValue>().added=true; break; 
                        case allItems.LittleMedKit : item.GetComponent<LittleMedKitScript>().added=true; break; 
                        case allItems.Armor : item.GetComponent<ArmorScript>().added=true; break; 
                        case allItems.SpeedBoost : case allItems.Invisibility:  case allItems.Magnifier: case allItems.Strength:
                        case allItems.Lantern : item.GetComponent<BoostScript>().added = true; break; 
                        default : break; 
                        
                    }
                    
                    Objects.Instance.Add(pair.Key, item); 

                }
            }
        }

        
    }
    // private void addKits(){
    //     if(Random.Range(0, 2)==0){
    //         items.Add(allItems.LittleMedKit, new List<GameObject>
    //         {Instantiate(littleMedKit, transform.position, Quaternion.identity)}
    //         );
    //     }
    // }
    public void addItemAfterSave(allItems item, GameObject gameObject){
        // Debug.Log(item); 
        if(items.ContainsKey(item)){
            items[item].Add(gameObject); 
        }
        else{
            items.Add(item, new List<GameObject>{
                gameObject
            });
        }
        gameObject.SetActive(false); 
        switch(item){
            case allItems.Keys : gameObject.GetComponent<KeyController>().added=true; break; 
            case allItems.MedKit : gameObject.GetComponent<KitValue>().added=true; break; 
            case allItems.LittleMedKit : gameObject.GetComponent<LittleMedKitScript>().added=true; break; 
            case allItems.Armor : gameObject.GetComponent<ArmorScript>().added=true; break; 
            case allItems.SpeedBoost : case allItems.Invisibility:  case allItems.Magnifier: case allItems.Strength:
            case allItems.Lantern : gameObject.GetComponent<BoostScript>().added = true; break; 
            default : break; 
            
        }
        Objects.Instance.Add(item, gameObject); 
        count++; 
        // int i =0; 
        // foreach(var pair in items){
        //     i+=pair.Value.Count; 
        // }
        // Debug.Log(i); 
        
    }
    protected void addItems(){
        // Instantiate(InventoryManager.Instance.Poison);
        
        for(int i= 0; i<count; i++){
            int idx = Random.Range(0, itemsList.Count);
            switch(itemsList[idx]){
                case allItems.LittleMedKit : 
                    if(!items.ContainsKey(allItems.LittleMedKit)) 
                    {
                        items.Add(allItems.LittleMedKit, new List<GameObject> {Instantiate(littleMedKit, coordinates, Quaternion.identity)}); 
                        
                    }
                    else{
                        items[allItems.LittleMedKit].Add(Instantiate(littleMedKit, coordinates, Quaternion.identity));
                    }
                    break;
                case allItems.SpeedBoost : 
                    if(!items.ContainsKey(allItems.SpeedBoost)) 
                    {
                        items.Add(allItems.SpeedBoost, new List<GameObject> {Instantiate(speedBoost, coordinates, Quaternion.identity)}); 
                        
                    }
                    else{
                        items[allItems.SpeedBoost].Add(Instantiate(speedBoost, coordinates, Quaternion.identity));
                    }
                    break;
                case allItems.Invisibility : 
                    if(!items.ContainsKey(allItems.Invisibility)) 
                    {
                        items.Add(allItems.Invisibility, new List<GameObject> {Instantiate(invisibility, coordinates, Quaternion.identity)}); 
                        
                    }
                    else{
                        items[allItems.Invisibility].Add(Instantiate(invisibility, coordinates, Quaternion.identity));
                    }
                    break;
                case allItems.Magnifier:
                    if(!items.ContainsKey(allItems.Magnifier)) 
                    {
                        items.Add(allItems.Magnifier, new List<GameObject> {Instantiate(magnifier, coordinates, Quaternion.identity)}); 
                        
                    }
                    else{
                        items[allItems.Magnifier].Add(Instantiate(magnifier, coordinates, Quaternion.identity));
                    }
                    break;
                case allItems.Strength:
                    if(!items.ContainsKey(allItems.Strength)) 
                    {
                        items.Add(allItems.Strength, new List<GameObject> {Instantiate(strength, coordinates, Quaternion.identity)}); 
                        
                    }
                    else{
                        items[allItems.Strength].Add(Instantiate(strength, coordinates, Quaternion.identity));
                    }
                    break;
                case allItems.Armor:

                    int random = Random.Range(0, 4);
                    if(big){
                        if(!items.ContainsKey(allItems.Armor)) 
                        {
                            if(random==0)
                                items.Add(allItems.Armor, new List<GameObject> {Instantiate(BigArmor, coordinates, Quaternion.identity)}); 
                            else 
                                items.Add(allItems.Armor, new List<GameObject> {Instantiate(MiddleArmor, coordinates, Quaternion.identity)});
                            
                        }
                        else{
                            if(random==0)
                                items[allItems.Armor].Add(Instantiate(BigArmor, coordinates, Quaternion.identity));
                            else 
                                items[allItems.Armor].Add(Instantiate(MiddleArmor, coordinates, Quaternion.identity));
                        }
                        break;
                    }
                    else{
                        if(!items.ContainsKey(allItems.Armor)) 
                        {
                            if(random==0)
                                items.Add(allItems.Armor, new List<GameObject> {Instantiate(MiddleArmor, coordinates, Quaternion.identity)}); 
                            else 
                                items.Add(allItems.Armor, new List<GameObject> {Instantiate(SmallArmor, coordinates, Quaternion.identity)});
                            
                        }
                        else{
                            if(random==0)
                                items[allItems.Armor].Add(Instantiate(MiddleArmor, coordinates, Quaternion.identity));
                            else 
                                items[allItems.Armor].Add(Instantiate(SmallArmor, coordinates, Quaternion.identity));
                        }
                        break;
                    }
                default: break; 
            }
            itemsList.RemoveAt(idx);
        }
        itemsList=null; 
    }
    public void Open(){
        // foreach(var pair in InventoryManager.Instance.Items){
        //     Debug.Log(pair.Value.Count); 
        //     // foreach(var item in pair.Value) Debug.Log(item);  
        // }
        // Debug.Log(InventoryManager.Instance.Items.Values); 
        if(!opened){
            animator.SetTrigger("open");
            foreach(var pair in items)
                foreach(var value in pair.Value)
                    {
                        // Debug.Log(pair.Key); 
                        if(!InventoryManager.Instance.Add(value, pair.Key)){
                            value.SetActive(true);
                        } //Debug.Log("running");
                    }
        }
        items = null; 
        opened = true; 
        
    }
    public void Opened(){
        animator.SetTrigger("opened");
    }
    public void OpenAtStart(){
        opened = true; 
        animator.SetTrigger("openStart");
    }
    // void Update(){
    //     if(gameObject.activeSelf&&!added){
    //         // Debug.Log(true); 
    //         added = true; 
    //         Objects.Instance.Add(allItems.Chest, gameObject); 
    //         foreach(var pair in items){
    //             foreach(var item in pair.Value){
    //                 switch(pair.Key){
    //                     case allItems.Keys : item.GetComponent<KeyController>().added=true; break; 
    //                     case allItems.MedKit : item.GetComponent<KitValue>().added=true; break; 
    //                     case allItems.LittleMedKit : item.GetComponent<LittleMedKitScript>().added=true; break; 
    //                     case allItems.Armor : item.GetComponent<ArmorScript>().added=true; break; 
    //                     case allItems.SpeedBoost : case allItems.Invisibility:  case allItems.Magnifier: case allItems.Strength:
    //                     case allItems.Lantern : item.GetComponent<BoostScript>().added = true; break; 
    //                     default : break; 
                        
    //                 }
                    
    //                 Objects.Instance.Add(pair.Key, item); 

    //             }
    //         }
            
    //     }
    // }
    // Update is called once per frame
    // void Update(){
        
    //     if(timer>=5) {
    //         Debug.Log(transform.position.ToString());
    //         timer = 0;
    //         Instantiate(gameObject, new Vector2(count2, count2), Quaternion.identity);
    //     }
    //     timer+=Time.deltaTime;
    //     count2+=timer;
        
    // }
}
