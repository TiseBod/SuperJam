using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class controlContextScript : MonoBehaviour
{
   // public GameObject textBox; 
   public TMP_Text textBox;

   [TextArea]public string[] descriptions;

   //public Image targetImage;
   //public Sprite[] sprites;
    
   
    void Start()
    {
       //tex = textBox.GetComponent<TextMeshPro>();
       
        
    }

    void Update()
    {
        
    }


    public void setContext(int index)
    {
        if (index >= 0 && index < descriptions.Length)
        {
            //targetImage.sprite = sprites[index];
            textBox.text = descriptions[index];
        }

       

    }
}
