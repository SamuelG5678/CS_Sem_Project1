using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;

    private float horizontalInput;
    private float forwardInput;
    public float currentSpeed;

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 1f;
    public int maxJumps = 1;
    public int jumpsRemaining;

    public GameManagerScript gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpsRemaining = maxJumps;
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
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining != 0)
        {
            jumpsRemaining--;
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
        } else if (collision.gameObject.CompareTag("Wall"))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathZone"))
        {
            GameManagerScript.instance.PlayerDeath();
        }
        else if (other.gameObject.CompareTag("WinZone"))
        {
            GameManagerScript.instance.WinLevel();
        }
        else if (other.gameObject.CompareTag("Pickup"))
        {
            //put pickup into inventory, destroy pickup
        }
    }
}