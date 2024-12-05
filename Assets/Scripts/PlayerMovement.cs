using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using Photon.Pun;

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
    Vector3 desiredVelocity;
    if (targetDirection.magnitude > 0.1f)
    {
        // Accelerate towards the target velocity
        desiredVelocity = Vector3.MoveTowards(
            rb.velocity, 
            new Vector3(targetDirection.x * speed, rb.velocity.y, targetDirection.z * speed), 
            acceleration * Time.fixedDeltaTime
        );
    }
    else
    {
        // Decelerate to a stop, only affecting X and Z axes
        desiredVelocity = Vector3.MoveTowards(
            rb.velocity, 
            new Vector3(0, rb.velocity.y, 0), 
            deceleration * Time.fixedDeltaTime
        );
    }

    // Update Rigidbody velocity without overriding Y axis forces (like gravity or AddForce effects)
    rb.velocity = desiredVelocity;
}

    
    public void PowerupStart(){
    	speed = 4.0f;
    }
    
    public void PowerupEnd(){
    	speed = 2.0f;
    }
    
    [PunRPC]
public void ApplyKnockback(Vector3 force)
{
    Rigidbody rb = GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }
}
}




