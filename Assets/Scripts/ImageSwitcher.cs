using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public int activeImageIndex;  // Index of the image to be active
    public GameObject[] images;   // Array of image GameObjects to be managed
    void Start()
    {
        UpdateActiveImage();
    }

    void UpdateActiveImage()
    {
        // Loop through all images and set only the one with matching index to active
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(i == activeImageIndex);
        }
    }
}