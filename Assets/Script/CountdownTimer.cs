using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Tambahkan ini untuk menggunakan TextMesh Pro

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60f; // Set waktu mundur 1 menit
    public TMP_Text timerText; // Referensi ke TMP_Text
    public GameObject gameOverCanvas; // Referensi ke canvas Game Over

    private float currentTime;
    private bool isTimerRunning = true; // Flag untuk menandakan apakah timer sedang berjalan

    void Start()
    {
        // Inisialisasi waktu saat scene dimulai
        currentTime = countdownTime;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (currentTime > 0 && isTimerRunning)
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

        // Jika timer habis
        if (isTimerRunning)
        {
            // Berhenti timer
            isTimerRunning = false;
            // Aktifkan canvas Game Over
            if (gameOverCanvas != null)
            {
                gameOverCanvas.SetActive(true);
            }
            // Tampilkan kursor dan biarkan bebas
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            // Hentikan waktu
            Time.timeScale = 0f;
        }
    }

    // Metode untuk memulai ulang timer (misalnya dari tombol Retry)
    public void RestartTimer()
    {
        // Nonaktifkan canvas Game Over jika sedang aktif
        if (gameOverCanvas != null && gameOverCanvas.activeSelf)
        {
            gameOverCanvas.SetActive(false);
        }

        // Reset waktu dan mulai countdown lagi
        currentTime = countdownTime;
        isTimerRunning = true;
        StartCoroutine(StartCountdown());

        // Sembunyikan kursor dan kunci di tengah layar
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // Mulai waktu kembali
        Time.timeScale = 1f;
    }
}
