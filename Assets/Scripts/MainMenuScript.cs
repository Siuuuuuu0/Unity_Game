
using UnityEngine;
using UnityEngine.SceneManagement; 
public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelLoader levelLoader; 
    public void PlayLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        // levelLoader.LoadNextLevel();
    } 
}
