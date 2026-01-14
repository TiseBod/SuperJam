using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class DashEffects : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]private bool isDashingBool;
    [SerializeField] private GameObject dashPool;
    [SerializeField] private GameObject[] dashEffectObjects;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private float effectRefreshRate;
    [SerializeField] private float deactivateEffect;
     [SerializeField]private bool EffectActive = false;

    void Awake()
    {
        dashEffectObjects = new GameObject[dashPool.transform.childCount];
        for (int i = 0; i < dashPool.transform.childCount; i++)
        {
            dashEffectObjects[i] = dashPool.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashingBool && !EffectActive)
        {
            for (int i = 0; i < dashEffectObjects.Length; i++)
            {
                Debug.Log("ActivateDashEffects called");    
                if (!dashEffectObjects[i].activeInHierarchy)
                {
                    Debug.Log("ActivateDashEffects called and pool used:"+dashEffectObjects[i].name);
                    StartCoroutine(RefreshDashEffects());
                    dashEffectObjects[i].SetActive(true);
                    dashEffectObjects[i].transform.SetPositionAndRotation(PlayerTransform.position, PlayerTransform.rotation);
                    StartCoroutine(DeactivateDashEffects(dashEffectObjects[i]));
                    break;
                }
            
            }
        }
    }

    void SetDashBool(bool isDashing)
    {
        this.isDashingBool = isDashing;
    }

    IEnumerator RefreshDashEffects()
    {
        
      
        EffectActive = true;
        yield return new WaitForSeconds(effectRefreshRate);
        EffectActive = false;
    }

    IEnumerator DeactivateDashEffects(GameObject target)
    {
        yield return new WaitForSeconds(deactivateEffect);
        target.SetActive(false);
       
    }

    void OnEnable()
    {
        MovementScript.OnDash += SetDashBool;
    }
}
