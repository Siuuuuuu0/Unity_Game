using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class HealthbarSlotScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;  

    // void Start()
    // {
    //     image = transform.GetComponentInChildren<Image>();
    //     // if(image!=null) Debug.Log(true);

    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void setImage(Sprite sprite){
        image.sprite = sprite; 
        // Debug.Log("running");
    }
}
