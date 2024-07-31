
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator transition; 
    public float transitionTime = 1f; 

    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
    }
    IEnumerator LoadLevel(int level){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime); 
        SceneManager.LoadScene(level);
    }
}
