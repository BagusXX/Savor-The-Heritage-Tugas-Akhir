using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    void Start()
    {
        // Set rasio aspek yang diinginkan
        float targetaspect = 1920.0f / 1080.0f;

        // Dapatkan rasio aspek layar
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // Hitung skala rasio aspek
        float scaleheight = windowaspect / targetaspect;

        Camera camera = GetComponent<Camera>();

        // Jika skala tinggi lebih kecil, tambahkan letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        // Jika skala tinggi lebih besar, tambahkan pillarbox
        else
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
