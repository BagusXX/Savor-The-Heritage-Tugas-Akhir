using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referensi ke komponen VideoPlayer
    public GameObject[] objectsToDisable; // Array objek yang akan dinonaktifkan setelah video selesai
    public GameObject[] objectsToEnable; // Array objek yang akan diaktifkan setelah video selesai

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished; // Berlangganan ke acara selesai video
        }
    }

    void Update()
    {
        // Periksa jika pengguna menekan tombol "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            SkipVideo();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        HandleVideoCompletion();
    }

    void SkipVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop(); // Hentikan pemutaran video
            HandleVideoCompletion();
        }
    }

    void HandleVideoCompletion()
    {
        // Nonaktifkan semua objek dalam array objectsToDisable
        if (objectsToDisable != null && objectsToDisable.Length > 0)
        {
            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }

        // Aktifkan semua objek dalam array objectsToEnable
        if (objectsToEnable != null && objectsToEnable.Length > 0)
        {
            foreach (GameObject obj in objectsToEnable)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
