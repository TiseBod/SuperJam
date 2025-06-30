using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    private bool menuOpen;
    private Scene currentScene;
    
    
    
    void Awake()
    {
       // menu = GameObject.Find("Menu");
        currentScene = SceneManager.GetActiveScene();
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && menuOpen)
        {
            Time.timeScale = 1;
            menu.SetActive(false);
            menuOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            
        }else if (Input.GetKeyDown(KeyCode.U) && !menuOpen)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
            menuOpen = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }


     public void Restart()
    {
        
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
