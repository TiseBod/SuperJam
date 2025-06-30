using UnityEngine;

public class backAndForth : MonoBehaviour
{

    public float speed = 2f; // the objects speed
    public float distance = 2.5f; // the distance it moves
    private Vector3 startPosition;
    public bool reverse = false;

    [SerializeField] public bool zDirection, xDirection, yDirection;
    void Awake()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (zDirection && !xDirection && !yDirection)
        {

            float z = Mathf.PingPong(Time.time * speed, distance);
            float offset = reverse ? (distance - z) : z;
            transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + offset);
        }else if(yDirection && !xDirection && !yDirection) {
            
            float y = Mathf.PingPong(Time.time * speed, distance);
            float offset = reverse ? (distance - y) : y;
            transform.position = new Vector3(startPosition.x, startPosition.y + offset, startPosition.z );
        }else if (xDirection && !yDirection && !zDirection)
        {
            float x = Mathf.PingPong(Time.time * speed, distance);
            float offset = reverse ? (distance - x) : x;
            transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z );
        }
    }
}