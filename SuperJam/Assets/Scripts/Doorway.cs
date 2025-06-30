using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour
{
    public String nextScene;
    private bool playerEntered = false;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerEntered && Input.GetButton("Fire1"))
        {
            SceneManager.LoadScene(nextScene);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" )
        { 
            playerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerEntered = false;
            
        }
    }



}
