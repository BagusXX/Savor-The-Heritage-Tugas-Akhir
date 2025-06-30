using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScript : MonoBehaviour
{
    public GameObject PauseMenuCanvas;
    public GameObject OptionalCanvas1; // Contoh tambahan canvas opsional 1
    public GameObject OptionalCanvas2; // Contoh tambahan canvas opsional 2
    // Tambahkan canvas opsional lainnya sesuai kebutuhan

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = false; // Sembunyikan kursor saat game dimulai
        Cursor.lockState = CursorLockMode.Locked; // Kunci kursor di tengah layar

        // Pastikan semua canvas opsional tidak terlihat saat memulai
        if (OptionalCanvas1 != null)
        {
            OptionalCanvas1.SetActive(false);
        }
        if (OptionalCanvas2 != null)
        {
            OptionalCanvas2.SetActive(false);
        }
        // Pastikan untuk menambahkan inisialisasi untuk canvas opsional lainnya jika diperlukan
    }

    // Method ini dipanggil saat player kalah atau kondisi tertentu untuk menampilkan canvas
    public void ShowLoseCanvas()
    {
        // Nonaktifkan canvas lain jika aktif
        if (OptionalCanvas1 != null && OptionalCanvas1.activeSelf)
        {
            OptionalCanvas1.SetActive(false);
        }
        if (OptionalCanvas2 != null && OptionalCanvas2.activeSelf)
        {
            OptionalCanvas2.SetActive(false);
        }

        // Aktifkan canvas lose
        if (PauseMenuCanvas != null)
        {
            PauseMenuCanvas.SetActive(true);
        }

        // Tampilkan kursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Lepaskan kursor dari posisi terkunci

        // Hentikan waktu
        Time.timeScale = 0f;
    }

    public void Play()
    {
        // Nonaktifkan canvas lose
        if (PauseMenuCanvas != null && PauseMenuCanvas.activeSelf)
        {
            PauseMenuCanvas.SetActive(false);
        }

        // Sembunyikan kursor saat melanjutkan permainan
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // Kunci kursor di tengah layar

        // Lanjutkan waktu
        Time.timeScale = 1f;
    }

    public void MainMenuButton()
    {
        // Pastikan waktu berjalan normal sebelum memuat scene baru
        Time.timeScale = 1f;

        // Pindah ke scene MAIN MENU
        SceneManager.LoadScene("MAIN MENU");
    }

    // Fungsi untuk berpindah ke level 1
    public void LoadLevel1()
    {
                // Pastikan waktu berjalan normal sebelum memuat scene baru
        Time.timeScale = 1f;

        SceneManager.LoadScene("level 1");
    }

    // Fungsi untuk berpindah ke level 2
    public void LoadLevel2()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("level 2");
    }

    // Fungsi untuk berpindah ke level 3
    public void LoadLevel3()
    {
        SceneManager.LoadScene("level 3");
    }

    // Tambahkan fungsi-fungsi untuk canvas opsional lainnya sesuai kebutuhan
}
