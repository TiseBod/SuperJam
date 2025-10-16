using System;
using UnityEngine;
[DefaultExecutionOrder(1200)]
public class bonusTime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float bonus;
    public Timer timer;
    public bool collisionHappened = false;
    void Start()
    {
        timer = GameObject.FindWithTag("timerCanvas").GetComponent<Timer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (collisionHappened)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Debug.Log("collision happens with clock");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("collision happens with clock");
            timer.countDownTime += bonus;
            collisionHappened = true;
        }
        
        
    }
}
