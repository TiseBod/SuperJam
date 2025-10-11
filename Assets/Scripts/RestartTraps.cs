using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartTraps : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    Scene currentScene;
   
    public GameOver gameOver;
    public bool gameOverOpen;
    void Start()
    {
        //currentScene = SceneManager.GetActiveScene();
     //   gameOverPanel = GameObject.Find("gameOverPanel");
        gameOver = GameObject.Find("GameOverCanvas").GetComponent<GameOver>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameOverOpen = true;
            gameOver.gameOverOpen = true;
            //SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}