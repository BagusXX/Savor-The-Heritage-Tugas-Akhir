using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tutor : MonoBehaviour
{
    public float countdownTime = 60f; // Set waktu mundur 1 menit
    public Text timerText; // Referensi ke UI Text

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
            // Update UI Text
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            // Tunggu satu frame
            yield return null;
        }

        // Waktu habis, transisi ke scene level 2
        SceneManager.LoadScene("OPENING"); // Pastikan nama scene sesuai dengan scene yang dituju
    }
}
