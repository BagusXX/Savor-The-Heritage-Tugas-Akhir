using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Mulai dengan menyembunyikan panel jeda dan menyembunyikan kursor
        PausePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Cek jika tombol ESC ditekan
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle antara jeda dan lanjutkan permainan
            if (isPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        // Aktifkan panel jeda
        PausePanel.SetActive(true);
        // Berhenti waktu
        Time.timeScale = 0;
        // Set status jeda menjadi benar
        isPaused = true;
        // Tampilkan kursor dan lepaskan kursor dari layar
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Continue()
    {
        // Sembunyikan panel jeda
        PausePanel.SetActive(false);
        // Lanjutkan waktu
        Time.timeScale = 1;
        // Set status jeda menjadi salah
        isPaused = false;
        // Sembunyikan kursor dan kunci kursor ke tengah layar
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
