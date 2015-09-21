using UnityEngine;
using System.Collections;

namespace RyanNielson.PixelCamera2D
{
    [ExecuteInEditMode]
    public class PixelCamera2D : MonoBehaviour
    {
        [SerializeField]
        private int targetWidth = 400;

        [SerializeField]
        private int targetHeight = 240;

        [SerializeField]
        private PixelCamera2DBehaviour behaviour;

        [SerializeField]
        private MeshRenderer quad;

        public int TargetWidth { get { return targetWidth; } }
        public int TargetHeight { get { return targetHeight; } }

        private Camera pixelCamera;

        private int previousWidth = 0;
        private int previousHeight = 0;

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
            if (behaviour == PixelCamera2DBehaviour.BestPixelPerfectFit)
            {
                BestFitBehaviour();
            }
            else if (behaviour == PixelCamera2DBehaviour.ScaleToFit)
            {
                ScaleBehaviour();
            }
        }

        public void SetRenderTexture(RenderTexture renderTexture)
        {
            pixelCamera.targetTexture = renderTexture;
            quad.sharedMaterial.mainTexture = renderTexture;
        }

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

        private void GetPixelCamera()
        {
            if (!pixelCamera)
            {
                pixelCamera = GetComponent<Camera>();
            }
        }

        private void UpdatePreviousSizes()
        {
            previousWidth = Screen.width;
            previousHeight = Screen.height;
        }
    }
}