using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Tambahkan ini untuk menggunakan TextMesh Pro

public class timerlevel3 : MonoBehaviour
{
    public float countdownTime = 60f; // Set waktu mundur 1 menit
    public TMP_Text timerText; // Referensi ke TMP_Text

    private float currentTime;

    void Start()
    {
        // Inisialisasi waktu saat scene dimulai
        currentTime = countdownTime;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (currentTime > 0)
        {
            // Kurangi waktu
            currentTime -= Time.deltaTime;
            // Format waktu menjadi mm:ss
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            // Update TMP_Text
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            // Tunggu satu frame
            yield return null;
        }

        // Waktu habis, transisi ke scene level 2
        SceneManager.LoadScene("kl3"); // Pastikan nama scene sesuai dengan scene yang dituju
    }
}
