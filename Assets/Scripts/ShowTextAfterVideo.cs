using UnityEngine;
using UnityEngine.UI; // Jika menggunakan TextMeshPro, gunakan "using TMPro;"
using UnityEngine.Video;

public class ShowImageAfterVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Image endImage; // Jika menggunakan TextMeshPro, ganti dengan "public TMP_Text endText;"

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished; // Subscribe to the event
        endImage.gameObject.SetActive(false); // Pastikan gambar tidak terlihat saat awal
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        endImage.gameObject.SetActive(true); // Tampilkan gambar setelah video selesai
    }
}
