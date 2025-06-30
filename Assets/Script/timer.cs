using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float countdownTime = 60f; // Set waktu mundur 1 menit
    public TextMeshProUGUI timerText; // Referensi ke UI Text menggunakan TextMeshProUGUI
    public string sceneToLoad; // Nama scene yang akan dimuat
    public LoadingScreenController loadingScreenController; // Referensi ke LoadingScreenController

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
            // Update UI Text menggunakan TextMeshProUGUI
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            // Tunggu satu frame
            yield return null;
        }

        // Waktu habis, gunakan LoadingScreenController untuk transisi ke scene yang ditentukan
        loadingScreenController.LoadScene(sceneToLoad);
    }
}
