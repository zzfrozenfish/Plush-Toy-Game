using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    public Transform targetLimb;
    public bool mirror;

    private ConfigurableJoint cj;
    private Quaternion startingRotation;
    private Quaternion targetStartingRotation;

    void Start()
    {
        cj = GetComponent<ConfigurableJoint>();

        // Memorize the exact starting poses of both the physical ragdoll and the animated dummy
        startingRotation = transform.localRotation;
        targetStartingRotation = targetLimb.localRotation;
    }

    void Update()
    {
        // Calculate exactly how far the red dummy's limb has rotated since the game started
        Quaternion rotationDifference = targetLimb.localRotation * Quaternion.Inverse(targetStartingRotation);

        if (!mirror)
        {
            // Unity Configurable Joints require inverted rotations. 
            // This applies the movement while maintaining your exact starting pose.
            cj.targetRotation = Quaternion.Inverse(rotationDifference) * startingRotation;
        }
        else
        {
            // Flips the rotation for mirrored joints
            cj.targetRotation = rotationDifference * startingRotation;
        }
    }
}