using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject gameOverPanel;
    public bool gameOverOpen;
    public Scene currentScene;
    public GameObject tracker;
    public double border = -10;
    public bool timerActive;
    public GameObject timerObject;
    void Awake()
    {
        // menu = GameObject.Find("Menu");
        currentScene = SceneManager.GetActiveScene();
        tracker = GameObject.FindGameObjectWithTag("tracker");
        timerObject = GameObject.FindGameObjectWithTag("timer");
       
       Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (tracker.transform.position.y < border)
        {
            //  gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            gameOverOpen = true;
            
            //SceneManager.LoadScene(currentScene.buildIndex);
        }

        if (gameOverOpen)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            if(timerActive)timerObject.SetActive(false);
        }else if (!gameOverOpen)
        {
            Time.timeScale = 1;
            gameOverPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            
        }
    }

    public void RestartGame()
    {
        gameOverOpen = false;
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void goHome()
    {
       gameOverOpen = false;
        SceneManager.LoadScene("Home-Interior");
    }
}
