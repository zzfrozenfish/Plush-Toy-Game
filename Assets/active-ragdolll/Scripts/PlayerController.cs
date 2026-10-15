using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Transform mainCamera;
    public Transform animationDummy;

    public float speed = 15f;
    public float runMultiplier = 1.5f; // Boosts speed when shifting
    public float rotationSpeed = 15f;
    public float jumpForce = 50f;

    public Rigidbody hips;
    public bool isGrounded;

    private Vector3 moveDirection;
    private bool isWalking;
    private bool isRunning;
    private bool isWalkBack;

    void Start()
    {
        hips = GetComponent<Rigidbody>();
        if (mainCamera == null) mainCamera = Camera.main.transform;
    }

    void Update()
    {
        // 1. Collect all inputs exactly as they are pressed
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Checks if either Shift key is held down
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Checks if the 'S' key is actively pulling backward
        isWalkBack = v < 0;

        // 2. Calculate movement direction relative to the camera
        Vector3 camForward = mainCamera.forward;
        Vector3 camRight = mainCamera.right;
        camForward.y = 0;
        camRight.y = 0;

        moveDirection = (camForward.normalized * v + camRight.normalized * h).normalized;
        isWalking = moveDirection.magnitude > 0.1f;

        // 3. Trigger the specific Animator booleans from your original setup
        animator.SetBool("isWalk", isWalking && !isWalkBack);
        animator.SetBool("isRun", isWalking && isRunning && !isWalkBack);
        animator.SetBool("isWalkBack", isWalkBack);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (isWalking)
        {
            // Apply your 1.5x speed boost if running
            float currentSpeed = isRunning ? speed * runMultiplier : speed;

            // Determine which way the body should face
            Quaternion targetRot;

            if (isWalkBack)
            {
                // If walking backward, lock the character facing FORWARD while the body moves BACKWARD
                targetRot = Quaternion.LookRotation(-moveDirection);
            }
            else
            {
                // Otherwise, face the direction of movement
                targetRot = Quaternion.LookRotation(moveDirection);
            }

            // Smoothly rotate the physical hips and sync the red dummy
            hips.MoveRotation(Quaternion.Slerp(hips.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime));
            if (animationDummy != null)
            {
                animationDummy.rotation = hips.rotation;
            }

            // Push the character using the calculated speed
            hips.AddForce(moveDirection * currentSpeed);
        }
    }
}

//public class PlayerController : MonoBehaviour
//{
//    public Animator animator;
//    public Transform mainCamera;      // Assign your Main Camera here
//    public Transform animationDummy; // Assign your red dummy here

//    public float speed; //forward&back
//    public float strafeSpeed; //left&right
//    public float jumpForce;

//    public Rigidbody hips;
//    public bool isGrounded;

//    private Vector3 moveDirection;
//    private bool isWalking;

//    void Start()
//    {
//        hips = GetComponent<Rigidbody>();
//        if (mainCamera == null) mainCamera = Camera.main.transform;
//    }

//    private void FixedUpdate()
//    {
//        if (Input.GetKey(KeyCode.W))
//        {
//            if (Input.GetKey(KeyCode.LeftShift))
//            {
//                animator.SetBool("isRun", true);
//                hips.AddForce(hips.transform.forward * speed * 1.5f);
//            }
//            else
//            {
//                animator.SetBool("isRun", false);
//                hips.AddForce(hips.transform.forward * speed);
//            }
//            animator.SetBool("isWalk", true);
//        }
//        else
//        {
//            animator.SetBool("isWalk", false);
//            animator.SetBool("isRun", false);
//        }

//        if (Input.GetKey(KeyCode.A))
//        {
//            animator.SetBool("isSideLeft", true);
//            hips.AddForce(-hips.transform.right * strafeSpeed);
//        }
//        else
//        {
//            animator.SetBool("isSideLeft", false);
//        }

//        if (Input.GetKey(KeyCode.S))
//        {
//            animator.SetBool("isWalkBack", true);
//            hips.AddForce(-hips.transform.forward * speed);
//        }
//        else
//        {
//            animator.SetBool("isWalkBack", false);
//        }

//        if (Input.GetKey(KeyCode.D))
//        {
//            animator.SetBool("isSideRight", true);
//            hips.AddForce(hips.transform.right * strafeSpeed);
//        }
//        else
//        {
//            animator.SetBool("isSideRight", false);
//        }

//        if (Input.GetButtonDown("Jump"))
//        {
//            if (isGrounded)
//            {
//                hips.AddForce(new Vector3(0, jumpForce, 0));
//                isGrounded = false;
//            }
//        }

//        isGrounded = false;
//    }
//}