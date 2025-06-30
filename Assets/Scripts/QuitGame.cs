using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public KeyCode quitKey = KeyCode.Return; // Tombol yang akan digunakan untuk keluar

    void Update()
    {
        // Periksa jika tombol yang ditentukan ditekan
        if (Input.GetKeyDown(quitKey))
        {
            Quit(); // Panggil method Quit() jika tombol ditekan
        }
    }

    // Method untuk keluar dari permainan
    void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Berhenti jika sedang dijalankan di editor Unity
        #else
        Application.Quit(); // Keluar dari aplikasi jika di-build
        #endif
    }
}
