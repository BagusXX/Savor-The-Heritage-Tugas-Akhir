using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro package

public class LoadingScreenController : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    public TextMeshProUGUI loadingProgressText; // Menggunakan TextMeshProUGUI untuk teks loading

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        // Menampilkan layar loading
        loadingScreen.SetActive(true);

        // Mulai memuat scene secara asinkron
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Sembari scene dimuat, update progress bar
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;

            // Update teks kemajuan loading menggunakan TextMeshProUGUI
            if (loadingProgressText != null)
            {
                loadingProgressText.text = $"{progress * 100f:F2}%";
            }

            yield return null;
        }
    }
}
