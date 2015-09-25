using UnityEngine;
using UnityEditor;

namespace RyanNielson.PixelCamera2D
{
    [CustomEditor(typeof(PixelCamera2D))]
    public class PixelCamera2DEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DrawCreateRenderTextureButton((PixelCamera2D)target);
        }

        private void DrawCreateRenderTextureButton(PixelCamera2D pixelCamera2D)
        {
            if (GUILayout.Button("Create RenderTexture Asset"))
            {
                RenderTexture renderTexture = CreateNewRenderTexture(pixelCamera2D.BaseWidth, pixelCamera2D.BaseHeight);

                SaveNewRenderTexture(renderTexture, DeterminePath());

                pixelCamera2D.SetRenderTexture(renderTexture);
            }
        }

        private string DeterminePath()
        {
            return EditorUtility.SaveFilePanelInProject(
                "Save RenderTexture",
                "PixelCamera2D.renderTexture",
                "renderTexture",
                "Please enter a name for the PixelCamera2D render texture."
            );
        }

        private RenderTexture CreateNewRenderTexture(int width, int height)
        {
            RenderTexture renderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
            renderTexture.filterMode = FilterMode.Point;
            renderTexture.DiscardContents();

            return renderTexture;
        }

        private void SaveNewRenderTexture(RenderTexture renderTexture, string path)
        {
            AssetDatabase.CreateAsset(renderTexture, path);
            AssetDatabase.Refresh();
        }
    }
}