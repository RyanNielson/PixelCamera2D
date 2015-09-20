using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelCamera2D : MonoBehaviour
{
    [SerializeField]
    private int targetWidth = 400;

    [SerializeField]
    private int targetHeight = 240;

    public int TargetWidth { get { return targetWidth; } }
    public int TargetHeight { get { return targetHeight; } }

    private int previousWidth = 0;
    private int previousHeight = 0;

    private Camera pixelCamera;

    [SerializeField]
    private PixelCamera2DBehaviour behaviour;

    [SerializeField]
    private MeshRenderer quad;

    private void Awake()
    {
        pixelCamera = GetComponent<Camera>();

        //CreateAndSetRenderTexture(targetWidth, targetHeight);
    }

    private void Update()
    {
        if (Screen.width != previousWidth || Screen.height != previousHeight)
        {
             UpdatePreviousSizes();

             UpdateCamera();
        }
    }

    private void UpdateCamera()
    {
        //CreateAndSetRenderTexture(targetWidth, targetHeight);

        if (behaviour == PixelCamera2DBehaviour.BestFit)
        {
            BestFitBehaviour();
        }
        else if (behaviour == PixelCamera2DBehaviour.Scale)
        {
            ScaleBehaviour();
        }
    }

    public void SetRenderTexture(RenderTexture renderTexture)
    {
        pixelCamera.targetTexture = renderTexture;
        quad.sharedMaterial.mainTexture = renderTexture;
    }

    //private void CreateAndSetRenderTexture(int width, int height, bool release = false)
    //{
        //if (pixelCamera.targetTexture != null)
        //{
        //    pixelCamera.targetTexture.DiscardContents();
        //    pixelCamera.targetTexture.Release();
        //}

        //RenderTexture newRenderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
        //newRenderTexture.filterMode = FilterMode.Point;

        //quad.sharedMaterial.mainTexture = newRenderTexture;
        //pixelCamera.targetTexture = newRenderTexture;
    //}

    private void BestFitBehaviour()
    {
        int nearestWidth = Screen.width / targetWidth * targetWidth;
        int nearestHeight = Screen.height / targetHeight * targetHeight;

        int xScaleFactor = nearestWidth / targetWidth;
        int yScaleFactor = nearestHeight / targetHeight;

        int scaleFactor = yScaleFactor < xScaleFactor ? yScaleFactor : xScaleFactor;

        float heightRatio = (targetHeight * (float)scaleFactor) / Screen.height;
    
        quad.transform.localScale = new Vector3(targetWidth / (float)targetHeight * heightRatio, 1f * heightRatio, 1f);
    }

    private void ScaleBehaviour()
    {
        float targetAspectRatio = targetWidth / (float)targetHeight;
        float windowAspectRatio = Screen.width / (float)Screen.height;
        float scaleHeight = windowAspectRatio / targetAspectRatio;

        if (scaleHeight < 1f)
        {
            quad.transform.localScale = new Vector3(targetAspectRatio * scaleHeight, scaleHeight, 1f);
        }
        else
        {
            quad.transform.localScale = new Vector3(targetAspectRatio, 1f, 1f);
        }
    }

    private void UpdatePreviousSizes()
    {
        previousWidth = Screen.width;
        previousHeight = Screen.height;
    }
}
