using System;
using UnityEngine;

public class StartTrial : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    public Timer timer;
    public bool eventTriggered = false;
    public GameObject border;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eventTriggered && Input.GetButton("Fire1"))
        {
            timer.StartTimer();
            Destroy(border);
        }
        
        

    }
    
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            eventTriggered = true;
            
        }
    }
}
