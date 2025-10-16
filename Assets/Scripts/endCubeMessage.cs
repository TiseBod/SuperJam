using UnityEngine;

public class endCubeMessage : NpcSystem
{
    public Timer timer;
    public bool alternateText;

    void Start()
    {
        timer = GameObject.FindWithTag("timerCanvas").GetComponent<Timer>();
    }

    void SetDialogBasedOnTime()
    {
        float time = timer.elaspedTime/100;

        if (time < 0.40f)
        {
            if (!alternateText)
            {
                lines[0] = $"Wow you're pretty quick aren't you? Thank you for playing this demo! Time: {time:F2}s";
            }
            
        }
        else if (time < 0.60f)
        {
            if (!alternateText)
             lines[0] = $"Thank you for playing this demo! Time: {time:F2}s";
        }
        else
        {
            if (!alternateText)
             lines[0] = $"Thank you for playing this demo! You can go even faster: {time:F2}s";
        }
    }

    public override void Update()
    {
        if (player_detection)
        {
          //  dialogText.text = lines;
        }

        base.Update();
        SetDialogBasedOnTime();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            player_detection = true;
            timer.StopTimer();  // optional: stop time when player finishes
          //  SetDialogBasedOnTime();
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_detection = false;
            dialogText.text = "";
            dialogPanel.SetActive(false);
        }
    }
}