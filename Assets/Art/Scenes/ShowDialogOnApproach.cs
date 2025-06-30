using UnityEngine;
using System.Collections;

public class ShowDialogOnApproach : MonoBehaviour
{
    public GameObject player; // Referensi ke pemain
    public GameObject dialogCanvas; // Referensi ke canvas dialog
    public float triggerDistance = 5.0f; // Jarak untuk memicu dialog
    public float updateInterval = 0.5f; // Interval pembaruan dalam detik

    void Start()
    {
        // Pastikan canvas dialog tidak aktif pada awalnya
        dialogCanvas.SetActive(false);

        // Mulai Coroutine untuk memperbarui status canvas pada interval tertentu
        StartCoroutine(UpdateDialogCanvas());
    }

    IEnumerator UpdateDialogCanvas()
    {
        while (true)
        {
            // Hitung jarak antara pemain dan objek
            float distance = Vector3.Distance(player.transform.position, transform.position);

            // Jika jarak kurang dari atau sama dengan triggerDistance, tampilkan dialog
            if (distance <= triggerDistance)
            {
                if (!dialogCanvas.activeSelf)
                {
                    dialogCanvas.SetActive(true);
                    Debug.Log("Canvas diaktifkan: Jarak = " + distance);
                }
            }
            else
            {
                if (dialogCanvas.activeSelf)
                {
                    dialogCanvas.SetActive(false);
                    Debug.Log("Canvas dinonaktifkan: Jarak = " + distance);
                }
            }

            // Tunggu sebelum melakukan pemeriksaan berikutnya
            yield return new WaitForSeconds(updateInterval);
        }
    }
}
