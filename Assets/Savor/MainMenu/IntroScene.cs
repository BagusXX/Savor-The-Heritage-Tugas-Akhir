using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "MainMenu"; // Ganti dengan nama scene MainMenu kamu

    private bool hasPressedKey = false;

    void Update()
    {
        // Cek jika tombol apa pun ditekan
        if (!hasPressedKey && Input.anyKeyDown)
        {
            hasPressedKey = true;
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
