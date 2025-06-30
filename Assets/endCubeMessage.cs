using UnityEngine;

public class endCubeMessage : NpcSystem
{
    public Timer timer;

    void SetDialogBasedOnTime()
    {
        float time = timer.elaspedTime/100;

        if (time < 0.40f)
        {
            dialog = "Wow you're pretty quick aren't you? Feel free to meet up with Mr Hudson, if you pass the patience trial that is...";
        }
        else if (time < 0.60f)
        {
            dialog = "Applicant #110, you've passed the exam, however... on assignments you must have more skill in order to succeed.";
        }
        else
        {
            dialog = $"Applicant #110, you have failed this trial. To become an agent, you have to be faster. Try again next time. Time: {time:F2}s";
        }
    }

    public override void Update()
    {
        if (player_detection)
        {
            dialogText.text = dialog;
        }

        base.Update();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_detection = true;
            timer.StopTimer();  // optional: stop time when player finishes
            SetDialogBasedOnTime();
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_detection = false;
            dialogText.text = "";
            dialogPanel.SetActive(false);
        }
    }
}