using UnityEngine;

public class CameraAspectRatioScaler : MonoBehaviour
{
    private float targetRatio = 16f / 9f;

    void Start()
    {
        // Get our ratio and figure the difference to the current
        float windowRatio = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowRatio / targetRatio;

        Camera camera = GetComponent<Camera>();

        // Add letterboxes if taller
        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else // Side letterboxes if wider
        {
            float scalewidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}