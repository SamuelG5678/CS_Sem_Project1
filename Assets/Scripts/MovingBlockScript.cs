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

    private float distanceToTargetX;
    private float distanceToTargetY;
    private float distanceToTargetZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x != currentTarget.transform.position.x)
        {
            if ((transform.position.x - currentTarget.transform.position.x) > 0)
            {

            }
        }


    }


}
