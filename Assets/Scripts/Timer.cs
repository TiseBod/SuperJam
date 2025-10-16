using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
[SerializeField] TextMeshProUGUI timerText;
public float elaspedTime;
public bool timerStart= false;
public bool countDown = false;
public float countDownTime = 40;
public GameOver gameOver;


    void Start()
    {
        gameOver = GameObject.Find("GameOverCanvas").GetComponent<GameOver>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStart && !countDown)
        {
            elaspedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elaspedTime / 60);
            int seconds = Mathf.FloorToInt(elaspedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }else if (timerStart && countDown)
        {
            countDownTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(countDownTime / 60);
            int seconds = Mathf.FloorToInt(countDownTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }


        if (countDownTime < 0f && countDown)
        {
            gameOver.gameOverOpen = true;
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
