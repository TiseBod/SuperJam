using UnityEngine;

public class lookAt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform target;
    void Awake()
    {
       // target = GameObject.Find("HeroesMotion").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        
    }
}
