using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deathScreen;
    void Start(){
        deathScreen.SetActive(false);
    }
    public void Dead(){
        Time.timeScale=0f;
        deathScreen.SetActive(true);
    }
    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void RestartGame(){
        Time.timeScale=1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
