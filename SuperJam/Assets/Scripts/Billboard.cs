using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
        
    }

    // Update is called once per frame.
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
        
    }
}
