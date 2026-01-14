using UnityEngine;
using UnityEngine.VFX;

public class pulseRocket : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public VisualEffect pulseEffect;
    void Start()
    {
        if (pulseEffect != null)
        {
            pulseEffect.Play();
        }

    }



    // Update is called once per frame
    void Update()
    {
       
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "tracker" && collision.gameObject.tag != "Player")
        Destroy(gameObject);
    }

}
