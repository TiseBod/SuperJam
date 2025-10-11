using UnityEngine;

public class barrierCreate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Barrier;
    public Transform pulseCrystal;
    public float evaporateTime = 1f;
    public float attackRate = 0.1f;
    public float nextPulseTime = 0f;
    
    
    
    


    void onMake()
    {
        
        if(Input.GetKeyDown(KeyCode.E)){
            if (Time.time >= nextPulseTime)
            {
                //animator.SetBool(isPunchingHash, true);
                
                barrierMake();
                nextPulseTime = Time.time + 1f/attackRate;

                
            }
        }
    }


    void barrierMake()
    {
        GameObject wall = Instantiate(Barrier, pulseCrystal.position, pulseCrystal.rotation);
        Destroy(wall, evaporateTime);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onMake();
    }
}
