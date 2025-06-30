    using System;
using UnityEngine;

public class JamoCombatAnimations : MonoBehaviour
{
    Animator animator;
    
    [SerializeField] private AudioClip[] hitClip;
    private int isPunchingHash;
    
    private AudioSource audioSource;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isPunchingHash = Animator.StringToHash("isPunching");
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        OnInputEntered();
    }

    void OnInputEntered()
    {
       // OnPunch();
        OnDance();
      // JumpOver();
        //Fire();
    }

    void OnPunch()
    {
        bool isPunching = animator.GetBool(isPunchingHash);
        if(!isPunching && Input.GetButton("punch")){
            animator.SetBool(isPunchingHash, true);
            audioSource.clip = hitClip[0];
            audioSource.Play();
        }else if(isPunching && !Input.GetButton("punch")){
            animator.SetBool(isPunchingHash, false); }
        
       
    }

    void OnDance()
    {
        bool isDancing = animator.GetBool("isDancing");
        if(!isDancing && Input.GetButton("Dance")){
            animator.SetBool("isDancing", true);
            audioSource.clip = hitClip[2];
            audioSource.PlayDelayed(0f);
        }else if(isDancing && !Input.GetButton("Dance")){
            animator.SetBool("isDancing", false);
            audioSource.Stop();
        }
    }

   

    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Fire");
            audioSource.clip = hitClip[1];
            audioSource.PlayDelayed(0f);
            //audioSource.Play();
        }
    }

}
