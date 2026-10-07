using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed; //forward&back
    public float strafeSpeed; //left&right
    public float jumpForce;

    public Rigidbody hips;
    public bool isGrounded;

    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRun", true);
                hips.AddForce(hips.transform.forward * speed * 1.5f);
            }
            else
            {
                animator.SetBool("isRun", false);
                hips.AddForce(hips.transform.forward * speed);
            }
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isSideLeft", true);
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }
        else
        {
            animator.SetBool("isSideLeft", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isWalkBack", true);
            hips.AddForce(-hips.transform.forward * speed);
        }
        else
        {
            animator.SetBool("isWalkBack", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isSideRight", true);
            hips.AddForce(hips.transform.right * strafeSpeed);
        }
        else
        {
            animator.SetBool("isSideRight", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }

        isGrounded = false;
    }
}