using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class NpcSystem : MonoBehaviour
{
    public bool player_detection = false;
    public TextMeshProUGUI dialogText;
    public GameObject dialogPanel;
    public bool ForceInteract = false;

    public Transform player;

    [TextArea] public string[] lines;
    [FormerlySerializedAs("textSpeed")] public float textTime = 0.25f; // seconds per character

    // internals
    private int index;
    private bool typing;
    private bool skip;
    private bool running;
    private Coroutine typeRoutine;

    void Awake()
    {
        var go = GameObject.FindGameObjectWithTag("Player");
        if (go) player = go.transform;
        if (dialogPanel) dialogPanel.SetActive(false);
        if (dialogText) dialogText.text = "";
    }

    public virtual void Update()
    {
        if (!player_detection || !running) return;

        if (Input.GetKeyDown(KeyCode.K) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Return) ||
            Input.GetMouseButtonDown(0))
        {
            if (typing) skip = true;
            else NextLine();
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        player_detection = true;
        if (ForceInteract || !running) StartDialogue();
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        player_detection = false;
        EndDialogue();
    }
    public void EndDialogue()
    {
        running = false;
        typing = false;
        skip = false;

        if (typeRoutine != null) { StopCoroutine(typeRoutine); typeRoutine = null; }

        if (dialogText) dialogText.text = "";
        if (dialogPanel) dialogPanel.SetActive(false);
    }

    public void StartDialogue()
    {
        if (lines == null || lines.Length == 0) return;

        running = true;
        index = 0;
        typing = false;
        skip = false;

        if (dialogPanel) dialogPanel.SetActive(true);
        if (dialogText) dialogText.text = string.Empty;

        StartTypingCurrent();
    }

    void StartTypingCurrent()
    {
        if (typeRoutine != null) StopCoroutine(typeRoutine);
        typeRoutine = StartCoroutine(TypeLine(lines[index]));
    }

   public IEnumerator TypeLine(string line)
    {
        typing = true;
        skip = false;
        dialogText.text = string.Empty;

        if (string.IsNullOrEmpty(line) || textTime <= 0f)
        {
            dialogText.text = line;
            typing = false;
            yield break;
        }

        for (int i = 0; i < line.Length; i++)
        {
            if (skip) { dialogText.text = line; break; }

            if (line[i] == '<')
            {
                int close = line.IndexOf('>', i);
                if (close != -1)
                {
                    dialogText.text += line.Substring(i, close - i + 1);
                    i = close;
                    continue;
                }
            }

            dialogText.text += line[i];
            yield return new WaitForSeconds(textTime);
        }

        typing = false;
        typeRoutine = null;
    }

   public void NextLine()
    {
        if (typing) { skip = true; return; }

        if (index < lines.Length - 1)
        {
            index++;
            StartTypingCurrent();
        }
        else
        {
            EndDialogue();
        }
    }
}
