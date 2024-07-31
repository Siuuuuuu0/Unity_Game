
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI; 

public class SlotEquippedScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image; 
    public new GameObject gameObject; 
    InventoryEquippedScript inventoryEquipped;
    int order; 
    public allItems item; 
    void Start()
    {
        inventoryEquipped = transform.parent.GetComponent<InventoryEquippedScript>();
    }
    public void Add(Sprite sprite, GameObject gameObject, int order, allItems item){
        image.sprite = sprite;  
        image.enabled = true; 
        this.gameObject=gameObject;
        this.order=order;
        this.item = item; 
    }
    // Update is called once per frame
    public void Remove(){
        image.enabled=false;
        inventoryEquipped.isFull[order] =false;
        gameObject = null;
        // item = null; 
    }
}
