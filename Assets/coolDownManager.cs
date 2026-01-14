using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[DefaultExecutionOrder(1000)]
public class CooldownManager : MonoBehaviour
{
    public ShootPulse shootPulse;        // Reference to ShootPulse
    public Image[] coolDownImages;       // Spark UI images

    public int maxSparks = 3;
    public MovementScript movementScript;
    public int cooldownSparks;           // Current available sparks
    public int shootPulseCost = 1;
    public int barrierCost = 3;
    public int slowfallCost = 1;
    public int dashCost = 1;
    public float coolDownRate = 1f;      // Time per spark recovery
    public bool allowSlowFall = false;
    public bool allowDash = false;
    private bool[] sparkActiveStates;

    void Start()
    {
        shootPulse = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootPulse>();
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>();
        sparkActiveStates = new bool[maxSparks];

        // Initialize all sparks active
        for (int i = 0; i < maxSparks; i++)
        {
            sparkActiveStates[i] = true;
            coolDownImages[i].canvasRenderer.SetAlpha(1f);
            coolDownImages[i].CrossFadeAlpha(1f, 0f, false);
        }

        cooldownSparks = maxSparks;
    }

    /// <summary>
    /// Called by ShootPulse when player presses punch
    /// Returns true if pulse is allowed to fire
    /// </summary>
    public bool TryShootPulse()
    {
        if (cooldownSparks >= shootPulseCost)
        {
            UseSparks(shootPulseCost);
            return true;
        }
        return false; // not enough sparks
    }

    public bool barrierCreated()
    {
        if (cooldownSparks >= barrierCost)
        {
            UseSparks(barrierCost);
            return true;
        }
        return false; // not enough sparks
        
    }

    public bool SlowfallActivated()
    {
        if (cooldownSparks >= slowfallCost || allowSlowFall)
        {
            
            if (Input.GetKeyDown(KeyCode.C))
            { 
                UseSparks(slowfallCost);
                allowSlowFall = true;
                Debug.Log( "is player grounded: "+ movementScript.characterController.isGrounded);
               
            }
            if (movementScript.characterController.isGrounded)
            {
                allowSlowFall = false;
            }
            return true;
            
        }
        
        
        return false;
    }


    public bool DashActivated()
    {
        if (cooldownSparks >= dashCost)
        {
            if (!allowDash)
            {
                UseSparks(dashCost);
                StartCoroutine(DashDuration());
            }
           
            return true;
        }
        
        if(allowDash)
        {
            return true;
        }
        return false; // not enough sparks
    }

    IEnumerator DashDuration()
    {
        allowDash = true;
        yield return new WaitForSecondsRealtime(movementScript.dashDuration);
        allowDash = false;
    }

    void UseSparks(int cost)
    {
        int used = 0;
        for (int i = 0; i < maxSparks && used < cost; i++)
        {
            if (sparkActiveStates[i])
            {
                sparkActiveStates[i] = false;
                cooldownSparks--;

                // Fade out spark
                coolDownImages[i].canvasRenderer.SetAlpha(1f);
                coolDownImages[i].CrossFadeAlpha(0f, 0.2f, false);

                used++;
            }
        }

        StartCoroutine(RecoverSparks(used));
    }

    private IEnumerator RecoverSparks(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(coolDownRate);

            for (int j = 0; j < maxSparks; j++)
            {
                if (!sparkActiveStates[j])
                {
                    sparkActiveStates[j] = true;
                    cooldownSparks++;

                    coolDownImages[j].canvasRenderer.SetAlpha(0f);
                    coolDownImages[j].CrossFadeAlpha(1f, 0.2f, false);
                    break;
                }
            }
        }
    }

    void Update()
    {
        if (movementScript.characterController.isGrounded)
        {
            allowSlowFall = false;
        }
    }
}
