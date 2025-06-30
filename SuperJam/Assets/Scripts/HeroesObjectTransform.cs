using UnityEngine;

public class HeroesObjectTransform : MonoBehaviour
{
    private Transform activeHeroTransform;
    
    void Awake()
    {
        activeHeroTransform = gameObject.transform.parent.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        activeHeroTransform.position = gameObject.transform.position;
    }
}
