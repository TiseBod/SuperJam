using System;
using UnityEngine;
[DefaultExecutionOrder(1000)]

public class OneTouchDelete : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Destroy(gameObject);
    }
}
