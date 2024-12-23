using UnityEngine;

// Handles showing and hiding an instruction image when hovering over UI elements.

public class TooltipImageHandler : MonoBehaviour
{
    [Header("Tooltip Image Reference")]
    public GameObject instructionImage;  // Reference to the instruction image GameObject in the UI.
    
    // Activates the instruction image (makes it visible).
  
    public void ShowImage()
    {
        instructionImage.SetActive(true);
    }

    // Deactivates the instruction image (hides it).
    
    public void HideImage()
    {
        instructionImage.SetActive(false);
    }
}