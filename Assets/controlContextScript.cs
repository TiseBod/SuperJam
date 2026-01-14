using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class controlContextScript : MonoBehaviour
{
   // public GameObject textBox; 
   public TMP_Text textBox;
   public VideoPlayer videoPlayer;

   [TextArea]public string[] descriptions;
   public VideoClip[] videoClips;

   //public Image targetImage;
   //public Sprite[] sprites;
    
   
    void Start()
    {
       //tex = textBox.GetComponent<TextMeshPro>();
      
        
    }

    void Update()
    {
        videoPlayer.playbackSpeed = 1f;
        Debug.Log("video player playback speed"+videoPlayer.playbackSpeed);
    }


    public void setContext(int index)
    {
        if (index >= 0 && index < descriptions.Length)
        {
            videoPlayer.clip = videoClips[index];
          
            textBox.text = descriptions[index];
            videoPlayer.Play();
            
        }

        videoPlayer.playbackSpeed = 1f;

    }
}
