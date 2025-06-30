using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoIntroController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished; // Subscribe to the event
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainScene"); // Ganti dengan nama scene yang ingin Anda tuju
    }
}
