using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingBlockScript : MonoBehaviour
{
    private BoxCollider blockCollider;

    public float speed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("moving box collided");
        if (other.gameObject.CompareTag("dir_Switch"))
        {
            speed = -1 * speed;
        }
    }
}
