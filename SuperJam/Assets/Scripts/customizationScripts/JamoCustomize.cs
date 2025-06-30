using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JamoCustomize : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<GameObject> Jamos;
    private int selectionIndex;
    
    
    
    
    
    void Start()
    {
        selectionIndex = PlayerPrefs.GetInt("JamoSelection");
        
        Jamos = new List<GameObject>();
        foreach ( Transform t in transform)
        {
            Jamos.Add(t.gameObject);
            t.gameObject.SetActive(false);  
        }
        Jamos[selectionIndex].SetActive(true);
        
        // Get spawn point
        string entryPointName = PlayerPrefs.GetString("EntryPoint", "DefaultSpawnPoint");
        Transform spawnPoint = GameObject.Find(entryPointName)?.transform;

        if (spawnPoint != null)
        {
            Jamos[selectionIndex].transform.position = spawnPoint.position;
        }
        else
        {
            Debug.LogWarning("Spawn point not found: " + entryPointName);
        }
        
    }

    public void SelectJamo(int index)
    {
        if (index == selectionIndex)
        {
            return;
        }
        
        if(index < 0 || index >= Jamos.Count)
            return;
        
        Jamos[selectionIndex].gameObject.SetActive(false);
        selectionIndex = index;
        Jamos[index].gameObject.SetActive(true);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    public void Confirm(string entryPointName)
    {
        PlayerPrefs.SetInt("JamoSelection", selectionIndex);
        PlayerPrefs.SetString("EntryPoint", entryPointName);
        SceneManager.LoadScene("Home-Interior");
    }

    public void ConfirmSpawnpoint()
    {
        Confirm("DefaultSpawnPoint");
    }

  /*  public void ConfirmEntryPoint2(string entryPointName)
    {
        PlayerPrefs.SetInt("JamoSelection", selectionIndex);
        PlayerPrefs.SetString("EntryPoint", entryPointName);
        SceneManager.LoadScene("Home-Interior");
        
    }*/ 

    public void SecondConfirmEntryPoint()
    {
        Confirm("EntryPoint2");
    }
}
