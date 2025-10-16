using System;
using UnityEngine;

public class barrierCreate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Barrier;
    public Transform pulseCrystal;
    public float evaporateTime = 1f;
    public float attackRate = 0.1f;
    public float nextPulseTime = 0f;
    
    
    
    public CooldownManager cooldownManager;


    private void Awake()
    {
        cooldownManager = GameObject.Find("CoolDownPanel").GetComponent<CooldownManager>();
    }

    void onMake()
    {
        
        if(Input.GetKeyDown(KeyCode.E) && Time.time >= nextPulseTime){

            if (cooldownManager != null && cooldownManager.barrierCreated())
            {
                barrierMake();
                nextPulseTime = Time.time + 1f/attackRate;
            }



            //animator.SetBool(isPunchingHash, true);
            
                
                
            
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
