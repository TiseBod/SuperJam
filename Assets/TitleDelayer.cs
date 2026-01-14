using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleDelayer : MonoBehaviour
{

    public string nextScene;
    public float delay;
    public Image image;
    public GameObject titleGameObject;
    void Start()
    {
        StartCoroutine(TitleDeactivate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator TitleDeactivate()
    {
        image.CrossFadeAlpha(1f, 0.3f, true);
        yield return new WaitForSeconds(delay);
        titleGameObject.SetActive(false);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

}
