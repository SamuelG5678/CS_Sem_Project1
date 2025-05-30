using System;
using System.Collections;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Objects")]
    private Rigidbody rb;
    public static PlayerScript instance;
    public GameManagerScript gameManager;
    public Camera cam;

    [Header("Movement")]
    private float horizontalInput;
    private float forwardInput;
    private float currentSpeed;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float dashSpeed = 30f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 1f;
    public int maxJumps = 1;
    private int jumpsRemaining;
    private bool isDashing = false;
    private bool isFastfalling = false;
    public float dashTime = 2f;
    public float timer;

    [Header("Inventory")]
    public int bluePickups = 0;
    public int redPickups = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        jumpsRemaining = maxJumps;

        timer = dashTime + 2;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (PlayerRotScript.instance.isInMenu == false)
        {
            if (isDashing == false && isFastfalling == false)
            {
                PlayerMove();
                Jump();
            }
            ForwardDash();
            FastFall();
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

    public void ForwardDash()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && bluePickups > 0)
        {
            bluePickups--;
            UIManagerScript.instance.UpdateSliderValue("blue", bluePickups);
            Debug.Log("triggered dash");
            AudioManagerScript.instance.PlaySFX(AudioManagerScript.instance.useitem);

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            var movementRelativeToCamera = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * dashSpeed;
            var cameraLookingDirection = cam.transform.rotation * Vector3.forward;
            cameraLookingDirection = new Vector3(cameraLookingDirection.x, 0f, cameraLookingDirection.z).normalized;
            var absoluteMovement = Quaternion.FromToRotation(Vector3.forward, cameraLookingDirection) * movementRelativeToCamera;
            rb.AddForce(absoluteMovement * dashSpeed);

            timer = 0;
        }

        timer += Time.deltaTime;

        if (timer == dashTime)
        {
            rb.linearVelocity *= 0;
        }

    }

    public void FastFall()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && redPickups > 0)
        {
            redPickups--;
            UIManagerScript.instance.UpdateSliderValue("red", redPickups);
            Debug.Log("triggered fastfall");
            AudioManagerScript.instance.PlaySFX(AudioManagerScript.instance.useitem);

            rb.linearVelocity = new Vector3(0, 0, 0);
            rb.AddForce(Vector3.down * dashSpeed);
        }
    }

}
