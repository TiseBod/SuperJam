using UnityEngine;
using UnityEngine.SceneManagement;

public class BorderRestart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private GameObject gameObject;
    public double border = -10;
    Scene currentScene;

    
    void Start()
    { Cursor.lockState = CursorLockMode.Locked;
        gameObject = GameObject.FindWithTag("tracker");
        currentScene = SceneManager.GetActiveScene();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < border)
        {
          //  gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            //SceneManager.LoadScene(currentScene.buildIndex);
        }

    }
}
