// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class MonsterAttackPanel : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void SwitchToLoaded(){
        GetComponent<Image>().color = Color.cyan;
    }
    public void SwitchToUnloaded(){
        GetComponent<Image>().color = new Color(17, 75, 20);
    }
}
