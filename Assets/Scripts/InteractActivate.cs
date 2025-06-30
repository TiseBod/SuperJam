using TMPro;
using UnityEngine;

public class InteractActivate : MonoBehaviour
{
    
    bool player_detection = false;
    public TextMeshPro interact_text;
    public bool alternateTextSelect = false;

    [TextArea] public string alternateText = "";
    
    
  
    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        interact_text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player_detection = true;
            if (alternateTextSelect)
            {
                interact_text.text = alternateText;
            }
            else
            {
                interact_text.text = "Interact:";
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
        interact_text.text = "";
 
    }
}
