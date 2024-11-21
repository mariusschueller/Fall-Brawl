using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class PlayerMovement : MonoBehaviour
{
    public XRNode inputSource; // LeftHand or RightHand
    public float speed = 2.0f; // Movement speed
    public float acceleration = 10f; // Acceleration rate
    public float deceleration = 15f; // Deceleration rate

    private Vector2 inputAxis; // To store joystick input
    private Vector3 currentVelocity = Vector3.zero; // Current movement velocity
    private Rigidbody rb; // Reference to Rigidbody
    private XROrigin xrRig; // Reference to XR Origin

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        xrRig = GetComponent<XROrigin>();
    }

    void Update()
    {
        // Get input from the joystick
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 axis))
        {
            inputAxis = axis;
        }
        else
        {
            inputAxis = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        // Calculate target direction relative to the player's head direction
        Quaternion headRotation = Quaternion.Euler(0, xrRig.Camera.transform.eulerAngles.y, 0);
        Vector3 targetDirection = headRotation * new Vector3(inputAxis.x, 0, inputAxis.y);

        // Smooth acceleration and deceleration
        if (targetDirection.magnitude > 0.1f)
        {
            // Accelerate towards the target velocity
            currentVelocity = Vector3.MoveTowards(
                currentVelocity, 
                targetDirection * speed, 
                acceleration * Time.fixedDeltaTime
            );
        }
        else
        {
            // Decelerate to a stop
            currentVelocity = Vector3.MoveTowards(
                currentVelocity, 
                Vector3.zero, 
                deceleration * Time.fixedDeltaTime
            );
        }

        // Apply the calculated velocity to the Rigidbody
        rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z); // Retain Y velocity for gravity
    }
}

