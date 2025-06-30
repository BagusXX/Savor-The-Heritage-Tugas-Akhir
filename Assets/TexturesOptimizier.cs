// using UnityEngine;

// public class PerObjectTextureOptimizer : MonoBehaviour
// {
//     [Tooltip("Maximum texture size for optimization")]
//     public int maxSize = 256; // Ukuran maksimal yang diinginkan

//     void Start()
//     {
//         OptimizeTextures();
//     }

//     void OptimizeTextures()
//     {
//         Renderer[] renderers = GetComponentsInChildren<Renderer>();
//         foreach (Renderer renderer in renderers)
//         {
//             foreach (Material mat in renderer.materials)
//             {
//                 if (mat.mainTexture != null)
//                 {
//                     Texture2D originalTexture = mat.mainTexture as Texture2D;
//                     if (originalTexture != null)
//                     {
//                         Texture2D resizedTexture = ResizeTexture(originalTexture, maxSize);
//                         mat.mainTexture = resizedTexture;
//                     }
//                 }
//             }
//         }
//     }

//     Texture2D ResizeTexture(Texture2D originalTexture, int newSize)
//     {
//         RenderTexture rt = RenderTexture.GetTemporary(newSize, newSize);
//         Graphics.Blit(originalTexture, rt);

//         Texture2D resizedTexture = new Texture2D(newSize, newSize);
//         RenderTexture.active = rt;
//         resizedTexture.ReadPixels(new Rect(0, 0, newSize, newSize), 0, 0);
//         resizedTexture.Apply();

//         RenderTexture.active = null;
//         RenderTexture.ReleaseTemporary(rt);

//         return resizedTexture;
//     }
// }
