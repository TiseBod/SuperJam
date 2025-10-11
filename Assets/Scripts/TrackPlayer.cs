using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    private Transform playerTransform;

    void Update()
    {
        // Keep trying to find the real player if not yet found
        if (playerTransform == null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject p in players)
            {
                if (p.activeInHierarchy)
                {
                    playerTransform = p.transform;
                    Debug.Log("Tracker locked onto: " + p.name);
                    break;
                }
            }
        }
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
        }
    }
}