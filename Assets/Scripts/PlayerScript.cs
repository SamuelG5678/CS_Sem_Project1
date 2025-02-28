using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Objects")]
    private Rigidbody rb;
    public static PlayerScript instance;
    public GameManagerScript gameManager;

    [Header("Movement")]
    private float horizontalInput;
    private float forwardInput;
    private float currentSpeed;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 1f;
    public int maxJumps = 1;
    private int jumpsRemaining;

    [Header("Inventory")]
    public int bluePickups = 0;
    public int redPickups = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        jumpsRemaining = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (PlayerRotScript.instance.isInMenu == false)
        {
            PlayerMove();
            Jump();
        }
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
            AudioManagerScript.instance.PlaySFX(AudioManagerScript.instance.jump);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManagerScript.instance.PlaySFX(AudioManagerScript.instance.collide);
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
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
            AudioManagerScript.instance.PlaySFX(AudioManagerScript.instance.pickupitem);
            
            if(other.gameObject.name == "PickupBlue" && bluePickups < 3)
            {
                bluePickups++;
                UIManagerScript.instance.UpdateSliderValue("blue", bluePickups);
            } else if(other.gameObject.name == "PickupRed" && redPickups < 3)
            {
                redPickups++;
                UIManagerScript.instance.UpdateSliderValue("red", redPickups);
            }

            Destroy(other.gameObject);
        }
    }

    public void FreezePlayer()
    {
        rb.constraints = RigidbodyConstraints.FreezePosition;
        PlayerRotScript.instance.isInMenu = true;
        CameraRotScript.instance.isInMenu = true;
    }

    public void UnfreezePlayer()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        PlayerRotScript.instance.isInMenu = false;
        CameraRotScript.instance.isInMenu = false;
    }
}