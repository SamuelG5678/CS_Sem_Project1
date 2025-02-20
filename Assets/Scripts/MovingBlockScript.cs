using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingBlockScript : MonoBehaviour
{
    private BoxCollider blockCollider;
    private Transform vector1;
    private Transform vector2;

    public GameObject point1;
    public GameObject point2;
    public GameObject currentTarget;

    public float speed = 1f;

    private float distanceToTargetX;
    private float distanceToTargetY;
    private float distanceToTargetZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockCollider = GetComponent<BoxCollider>();
        currentTarget = point1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x != currentTarget.transform.position.x)
        {
            if ((transform.position.x - currentTarget.transform.position.x) > 0)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if ((transform.position.x - currentTarget.transform.position.x) < 0)
            {
                
                transform.Translate(Vector3.right * -1 * speed * Time.deltaTime);
            }
        } else if (transform.position.x == currentTarget.transform.position.x)
        {
            if (currentTarget == point1)
            {
                currentTarget = point2;
            } else if (currentTarget == point1)
            {
                currentTarget = point2;
            }
        }
             
    }
}
