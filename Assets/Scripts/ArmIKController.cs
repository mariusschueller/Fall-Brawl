using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArmIKController : MonoBehaviour
{
    public Transform leftController; // Assign the left controller in the inspector
    public Transform rightController; // Assign the right controller in the inspector
    public Transform leftArmIKTarget; // Assign the left arm IK target in the inspector
    public Transform rightArmIKTarget; // Assign the right arm IK target in the inspector

    public float movementScale = 2.2f; // Adjust this scale factor to match the movement

    private Vector3 initialLeftOffset;
    private Vector3 initialRightOffset;

    void Start()
    {
        // Save the initial offset between the controllers and IK targets
        initialLeftOffset = leftArmIKTarget.position - leftController.position;
        initialRightOffset = rightArmIKTarget.position - rightController.position;
    }

    void Update()
    {
        // Scale and adjust the position of the IK targets relative to the controllers
        leftArmIKTarget.position = leftController.position + initialLeftOffset * movementScale;
        leftArmIKTarget.rotation = leftController.rotation;

        rightArmIKTarget.position = rightController.position + initialRightOffset * movementScale;
        rightArmIKTarget.rotation = rightController.rotation;
    }
}
