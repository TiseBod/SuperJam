using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
[DefaultExecutionOrder(1200)]

public class VictoryMessage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
   // public GameOver gameOver;
    public GameObject victoryPanel;
    public string nextScene;
  //  private Scene currentScene;
    private bool levelCleared = false;
    void Start()
    {
       // gameOver = GameObject.Find("GameOverCanvas").GetComponent<GameOver>();
        victoryPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (levelCleared)
        {
           
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            levelCleared = true;
            StartCoroutine(LoadPanel());
        }
    }


    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator LoadPanel()
    {
        yield return new WaitForSeconds(1f);
        victoryPanel.SetActive(true);
        
    }
}
