using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform player; // Drag the player GameObject here in the Inspector

    void Update()
    {
        if (player != null)
        {
            // Make the canvas face the player
            transform.LookAt(player);

            // Optionally reverse the canvas so it doesn't appear flipped
            transform.Rotate(0, 180, 0);
        }
    }
}

