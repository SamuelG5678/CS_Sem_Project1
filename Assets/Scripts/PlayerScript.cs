using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;

    private float horizontalInput;
    private float forwardInput;

    public float speed = 5f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        PlayerMove();
        Jump();
    }

    public void PlayerMove()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);
        transform.Translate(Vector3.forward * speed * Time.deltaTime * forwardInput);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    public void PanCamera()
    {
        
    }
}