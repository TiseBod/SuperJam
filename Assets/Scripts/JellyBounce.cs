using UnityEngine;
[DefaultExecutionOrder(1000)]
public class JellyBounce : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public MovementScript movementScript;
    public float jellyBounceForce;
    public float standardJumpHeight;
    public float standardJumpTime;
    public float standardGravity;
    public bool isJellyBouncing;
    
    
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (player.activeInHierarchy) // only the “real” one stays active
            {
                movementScript = player.GetComponent<MovementScript>();
                break;
            }
        }

        standardJumpHeight = movementScript.maxJumpHeight;
        standardJumpTime = movementScript.maxJumptime;
        standardGravity = movementScript.gravity;

    }

    // Update is called once per frame
    void Update()
    {
        if (isJellyBouncing)
        {
            movementScript.maxJumpHeight = 20;
            Debug.Log(movementScript.maxJumpHeight);

          //  movementScript.jellyMultiplier = 3f;
            Debug.Log($"jelly multiplier at {movementScript.jellyMultiplier}");
        }
        else
        {
            movementScript.maxJumpHeight = standardJumpHeight;
            // movementScript.maxJumptime = standardJumpTime;
            movementScript.setUpJumpVariables();
           // movementScript.jellyMultiplier = 1f;
        }

    }
    
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           isJellyBouncing = true;
           Debug.Log($"the gravity is {movementScript.gravity}");
           movementScript.jellyMultiplier = jellyBounceForce;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          isJellyBouncing = false;
          movementScript.jellyMultiplier = 1f;
        }
    }
}
