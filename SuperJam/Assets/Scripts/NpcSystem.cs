using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class NpcSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool player_detection = false;
    public TextMeshProUGUI dialogText;
    public GameObject dialogPanel;
    public bool ForceInteract = false;

    public Transform player;
   
    
    [TextArea]
    public String dialog;
    void Awake()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (ForceInteract && player_detection)
        {
            Debug.Log("Player detection");
            dialogPanel.SetActive(true);
        }

        if (player_detection && Input.GetButton("Fire1"))
        {
            Debug.Log("Player detection");
            dialogPanel.SetActive(true);
            
            
            
        }

        {
        }

    }

    private  void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            player_detection = true;
            dialogText.text = dialog;
            
        }
    }

    private  void OnTriggerExit(Collider other)
    {
        player_detection = false;
        dialogText.text = "";
        dialogPanel.SetActive(false);
    }

}
