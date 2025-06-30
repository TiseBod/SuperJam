using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartTraps : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    Scene currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}