using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    public Transform targetLimb;
    public bool mirror;
    ConfigurableJoint cj;
    Quaternion startRot;

    void Start()
    {
        cj = GetComponent<ConfigurableJoint>();
        // Memorizes how the limb was attached on frame 1
        startRot = transform.localRotation;
    }

    void Update()
    {
        if (!mirror)
        {
            // Applies the animation using local space and the starting offset
            cj.targetRotation = targetLimb.localRotation * startRot;
        }
        else
        {
            cj.targetRotation = Quaternion.Inverse(targetLimb.localRotation) * startRot;
        }
    }
}