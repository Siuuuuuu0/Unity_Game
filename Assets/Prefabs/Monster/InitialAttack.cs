// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine;

public class InitialAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float time =3;
    private bool run = false;
    private float update=0;
    private int ClickedCount =0;
    [SerializeField]
    private int NecessaryClicks = 10;
    private MonsterChildController monsterController;
    private MonsterAttackCanvas attackCanvas;
    private float AfterAttackCountdown = 5;
    private bool runCountdown;
    void Start()
    {
        monsterController = GetComponentInChildren<MonsterChildController>();
        
        attackCanvas = InventoryManager.Instance.monsterAttackCanvas;
    }

    // Update is called once per frame
    void Update(){
        if(run){
            update += Time.deltaTime;
            if(update>time)
                Kill();
            if(Input.GetKeyDown(KeyCode.Q)) {
                ClickedCount++;
                attackCanvas.Load();

            }
            if(ClickedCount >=NecessaryClicks) Liberated();
        }
        if(runCountdown) {
            AfterAttackCountdown-=Time.deltaTime;
            if(AfterAttackCountdown<=0) {
                runCountdown=false;
                AfterAttackCountdown=5;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().moveSpeed*=2f;
            }
        }
    }
    
        
    // }
    public void InitialMonsterAttack(){

        run = true;
        // Debug.Log("actiavted");
        monsterController.Liberated=false;
        attackCanvas.SwitchOn();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().cantMove();
        
    }
    private void Kill(){
        GameObject.FindWithTag("Player").GetComponent<PlayerObject>().health=0;
        attackCanvas.SwitchOff();
    }
    private void Liberated(){
        run=false;
        ClickedCount =0;
        update=0;
        // Debug.Log("Liberated");
        monsterController.Liberated=true;
        attackCanvas.SwitchOff();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().moveSpeed/=2f;
        runCountdown=true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().CanMove();
        InventoryManager.Instance.AddToEquipped(Instantiate(InventoryManager.Instance.Poison), allItems.Poison);
        
    }
}
