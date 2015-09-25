using UnityEngine;

namespace RyanNielson.PixelCamera2D
{
    [ExecuteInEditMode]
    public class PixelCamera2D : MonoBehaviour
    {
        [SerializeField]
        private int baseWidth = 400;

        [SerializeField]
        private int baseHeight = 240;

        [SerializeField]
        private PixelCamera2DBehaviour behaviour;

        [SerializeField]
        private MeshRenderer quad;

        public int BaseWidth { get { return baseWidth; } }
        public int BaseHeight { get { return baseHeight; } }

        private Camera pixelCamera;

        private int previousWidth = 0;
        private int previousHeight = 0;
        private PixelCamera2DBehaviour previousBehaviour;

        private void OnEnable()
        {
            pixelCamera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (Screen.width != previousWidth || Screen.height != previousHeight || previousBehaviour != behaviour)
            {
                UpdatePreviousValues();

                UpdateCamera();
            }
        }

        public void SetRenderTexture(RenderTexture renderTexture)
        {
            pixelCamera.targetTexture = renderTexture;
            quad.sharedMaterial.mainTexture = renderTexture;
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

        private void BestFitBehaviour()
        {
            int nearestWidth = Screen.width / baseWidth * baseWidth;
            int nearestHeight = Screen.height / baseHeight * baseHeight;

            int xScaleFactor = nearestWidth / baseWidth;
            int yScaleFactor = nearestHeight / baseHeight;

            int scaleFactor = yScaleFactor < xScaleFactor ? yScaleFactor : xScaleFactor;

            float heightRatio = (baseHeight * (float)scaleFactor) / Screen.height;

            quad.transform.localScale = new Vector3(baseWidth / (float)baseHeight * heightRatio, 1f * heightRatio, 1f);
        }

        private void ScaleBehaviour()
        {
            float targetAspectRatio = baseWidth / (float)baseHeight;
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

        private void UpdatePreviousValues()
        {
            previousWidth = Screen.width;
            previousHeight = Screen.height;
            previousBehaviour = behaviour;
        }
    }
}