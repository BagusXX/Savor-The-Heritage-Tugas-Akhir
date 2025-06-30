using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausescript : MonoBehaviour
{
    public static bool Paused = false;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        Cursor.visible = true; // Tampilkan kursor
        Cursor.lockState = CursorLockMode.None; // Lepaskan kursor dari posisi terkunci
    }

    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        Cursor.visible = false; // Sembunyikan kursor saat melanjutkan permainan
        Cursor.lockState = CursorLockMode.Locked; // Kunci kursor di tengah layar
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f; // Pastikan waktu berjalan normal sebelum memuat scene baru
        SceneManager.LoadScene("MAIN MENU"); // Ganti dengan nama skenario utama Anda
    }

    // Fungsi untuk menampilkan canvas opsional 1
    public void ShowOptionalCanvas1()
    {
        if (OptionalCanvas1 != null)
        {
            OptionalCanvas1.SetActive(true);
        }
    }

    // Fungsi untuk menyembunyikan canvas opsional 1
    public void HideOptionalCanvas1()
    {
        if (OptionalCanvas1 != null)
        {
            OptionalCanvas1.SetActive(false);
        }
    }

    // Fungsi untuk menampilkan canvas opsional 2
    public void ShowOptionalCanvas2()
    {
        if (OptionalCanvas2 != null)
        {
            OptionalCanvas2.SetActive(true);
        }
    }

    // Fungsi untuk menyembunyikan canvas opsional 2
    public void HideOptionalCanvas2()
    {
        if (OptionalCanvas2 != null)
        {
            OptionalCanvas2.SetActive(false);
        }
    }

    // Tambahkan fungsi-fungsi untuk canvas opsional lainnya sesuai kebutuhan
}
