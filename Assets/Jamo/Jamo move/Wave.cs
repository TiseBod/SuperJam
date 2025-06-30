using UnityEngine;

public class Wave : MonoBehaviour
{
    
   Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       onWave();

    }


    void onWave()
    {

        bool isWaving = animator.GetBool("isWaving");

        if (!isWaving && Input.GetKey(KeyCode.T))
        {
            animator.SetBool("isWaving", true);
        }
        else if (isWaving && !Input.GetKey(KeyCode.T))
        {
            animator.SetBool("isWaving", false);
        }





    }
}
