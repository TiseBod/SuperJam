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
        timer = GameObject.FindWithTag("timerCanvas").GetComponent<Timer>();
        startTrial = GameObject.FindWithTag("timeStarter").GetComponent<StartTrial>();
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
        if (other.gameObject.tag == "Player")
        {
            //timer.StopTimer();
            eventTriggered = true;
        }
    }
     
    
    
}
