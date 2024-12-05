using UnityEngine;

public class RotateY : MonoBehaviour
{
    // Rotation speed in degrees per second
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate the object around the Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}