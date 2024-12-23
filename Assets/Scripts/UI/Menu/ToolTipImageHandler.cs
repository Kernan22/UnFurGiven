using UnityEngine;

public class TooltipImageHandler : MonoBehaviour
{
    public GameObject instructionImage;  // Reference to the instruction image

    // Show Image
    public void ShowImage()
    {
        instructionImage.SetActive(true);
    }

    // Hide Image
    public void HideImage()
    {
        instructionImage.SetActive(false);
    }
}