using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    public PlayerController playerController;

    private void Start()
    {
        playerController = Object.FindFirstObjectByType<PlayerController>().GetComponent<PlayerController>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer != this.gameObject.layer)
        {
            playerController.isGrounded = true;
        }
    }
}
