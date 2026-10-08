using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotationSpeed = 1;
    public Transform root;

    float mouseX, mouseY;
    public float torsoOffset;
    public ConfigurableJoint hipJoint, torsoJoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, 35, 60);

        Quaternion rootRotation = Quaternion.Euler(0, mouseX + mouseY, 0);

        root.rotation = rootRotation;

        hipJoint.targetRotation = Quaternion.Euler(0, mouseX, 0);
        torsoJoint.targetRotation = Quaternion.Euler(mouseY + torsoOffset, 0, 0);
    }
}
