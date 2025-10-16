using UnityEngine;

public class ShootPulse : MonoBehaviour
{
    public GameObject cannon;             // Prefab of the magic bomb
    public Transform pulseCrystal;        // Reference to wand transform
    public float speed = 50f;             // Projectile speed
    public float evaporateTime = 3f;

    public float attackRate = 0.5f;       // Minimum time between shots
    private float nextPulseTime = 0f;

    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] hitClip;

    public CooldownManager cooldownManager;  // Reference to cooldown manager

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        cooldownManager = GameObject.Find("CoolDownPanel").GetComponent<CooldownManager>();
    }

    void Update()
    {
        OnPunch();
    }

    void OnPunch()
    {
        if (Input.GetButtonDown("punch") && Time.time >= nextPulseTime)
        {
            // Ask cooldown manager if we can shoot
            if (cooldownManager != null && cooldownManager.TryShootPulse())
            {
                ShootPulseCannon();
                nextPulseTime = Time.time + attackRate;

                // Play animation & sound
                animator.SetTrigger("Punch");
                if (hitClip.Length > 0)
                {
                    audioSource.clip = hitClip[0];
                    audioSource.Play();
                }
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