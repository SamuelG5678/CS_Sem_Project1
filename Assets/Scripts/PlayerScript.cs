using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;

    private float horizontalInput;
    private float forwardInput;

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float currentSpeed;
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        } else
        {
            currentSpeed = walkSpeed;
        }

        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime * horizontalInput);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime * forwardInput);
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