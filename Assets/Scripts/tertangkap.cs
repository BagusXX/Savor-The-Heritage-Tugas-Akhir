using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextscene : MonoBehaviour
{
    public GameObject canvasToShow; // Referensi ke canvas yang ingin ditampilkan

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aktifkan canvas jika belum aktif
            if (canvasToShow != null && !canvasToShow.activeSelf)
            {
                canvasToShow.SetActive(true);
            }

            // Tampilkan kursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Kunci kursor di tengah layar

            // Hentikan waktu
            Time.timeScale = 0f;
        }
    }
}
