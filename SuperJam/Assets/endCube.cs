using System;
using UnityEngine;

public class endCube : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Timer timer;
    public StartTrial startTrial;
    public bool eventTriggered = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eventTriggered)
        {
            timer.StopTimer();
            startTrial.eventTriggered = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //timer.StopTimer();
            eventTriggered = true;
        }
    }
     
    
    
}
