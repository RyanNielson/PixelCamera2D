using UnityEngine;

public class PixelCamera2DActivator : MonoBehaviour
{
    private Camera pixelCamera;

    private RenderTexture pixelCameraRenderTexture;

    private bool pixelCameraEnabled = true;

    private void Awake()
    {
        pixelCamera = GetComponent<Camera>();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150f, 50f), pixelCameraEnabled ? "Disable PixelCamera2D" : " Enable PixelCamera2D"))
        {
            pixelCameraEnabled = !pixelCameraEnabled;

            if (pixelCameraEnabled)
            {
                pixelCamera.targetTexture = pixelCameraRenderTexture;
                pixelCameraRenderTexture = null;

                SetActiveChildren(true);
            }
            else
            {
                pixelCameraRenderTexture = pixelCamera.targetTexture;
                pixelCamera.targetTexture = null;

                SetActiveChildren(false);
            }
        }
    }

    private void SetActiveChildren(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
