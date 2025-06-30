using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
[SerializeField] TextMeshProUGUI timerText;
public float elaspedTime;
public bool timerStart= false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStart)
        {
            elaspedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elaspedTime / 60);
            int seconds = Mathf.FloorToInt(elaspedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }

    public void StartTimer()
    {
        timerStart = true;
    }

    public void StopTimer()
    {
        timerStart = false;
    }
}
