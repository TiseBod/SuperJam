using UnityEngine;

public class DoorwayCanvasScript : MonoBehaviour
{
    bool player_detection = false;
    public GameObject doorwayPanel;

    public Transform player;
   
    
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player_detection /*&& Input.GetButton("Fire1")*/)
        {
            Debug.Log("Player detection");
            doorwayPanel.SetActive(true);
            
            
            
        }

        {
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player_detection = true;
           // dialogText.text = dialog;
           Cursor.lockState = CursorLockMode.None; // Unlock the cursor
           Cursor.visible = true;                  // Make the cursor visible
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
        //dialogText.text = "";
        doorwayPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Unlock the cursor
        Cursor.visible = false;                  // Make the cursor visible
    }

}