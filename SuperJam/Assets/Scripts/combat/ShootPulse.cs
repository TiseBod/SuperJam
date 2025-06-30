using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    
    public GameObject cannon; // Prefab of the magic bomb
    public Transform pulseCrystal;       // Reference to the wand transform
    public float speed = 50f;   // Speed of the magic bomb
    public float evaporateTime = 3f;

    public float attackRate = 0.1f;
    public float nextPulseTime = 0f;
    Animator animator;
    private AudioSource audioSource;
    //private int isPunchingHash;
    [SerializeField] private AudioClip[] hitClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
       // isPunchingHash = Animator.StringToHash("isPunching");
    }

    // Update is called once per frame
    void Update()
    {
       OnPunch();

    }
    
    void OnPunch()
    {
        //bool isPunching = animator.GetBool(isPunchingHash);
        if(Input.GetButton("punch")){
            if (Time.time >= nextPulseTime)
            {
                //animator.SetBool(isPunchingHash, true);
                animator.SetTrigger("Punch");
                audioSource.clip = hitClip[0];
                audioSource.Play();
                ShootPulseCannon();
                nextPulseTime = Time.time + 1f/attackRate;

                
            }
        }
        
       
    }


    void ShootPulseCannon()
    {
        GameObject pulse = Instantiate(cannon, pulseCrystal.position, pulseCrystal.rotation);
        
        Rigidbody rb = pulse.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(pulseCrystal.forward * speed, ForceMode.Impulse); 
        }
        
        Destroy(pulse, evaporateTime);
    }
}
