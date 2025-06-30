using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referensi ke komponen VideoPlayer
    public string nextSceneName; // Nama scene yang akan dipindahkan setelah video selesai
    public LoadingScreenController loadingScreenController; // Referensi ke LoadingScreenController

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished; // Berlangganan ke event selesai video
        }
        else
        {
            Debug.LogError("VideoPlayer is not assigned.");
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
        LoadNextScene();
    }

    void SkipVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop(); // Hentikan pemutaran video
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Pindah ke scene berikutnya menggunakan loading screen
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            loadingScreenController.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not set.");
        }
    }
}
