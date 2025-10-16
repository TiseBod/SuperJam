using System;
using UnityEngine;

public class pauseScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public GameObject pauseMenu;
    public GameObject controlMenu;
   
    private void Awake()
    {
        pauseMenu = GameObject.Find("PausePanel");
        controlMenu = GameObject.Find("controlMenuPanel");
    }
    void Start()
    {
        pauseMenu.SetActive(false);
        controlMenu.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P) && pauseMenu.activeSelf)
        {
            //Time.timeScale = 0;
            pauseMenu.SetActive(false);
        }else if (Input.GetKeyDown(KeyCode.P) && !pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
           
        }

       
    }


    public void openControlMenu()
    {
     controlMenu.SetActive(true);   
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        controlMenu.SetActive(false);
    }


    public void GoBacktoPauseMenu()
    {
        controlMenu.SetActive(false);
    }

}
