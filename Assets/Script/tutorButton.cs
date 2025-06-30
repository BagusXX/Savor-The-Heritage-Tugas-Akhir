using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnHideManager : MonoBehaviour
{
    public List<GameObject> objectsToSpawn; // List GameObject yang akan di-spawn
    public List<GameObject> objectsToHide;  // List GameObject yang akan di-hidden
    public KeyCode spawnKeyCode = KeyCode.W; // Tombol untuk memicu spawn
    public KeyCode hideKeyCode = KeyCode.H;  // Tombol untuk memicu hide
    public float hideDelay = 2f; // Jeda sebelum menyembunyikan objek (dalam detik)

    private Coroutine hideCoroutine; // Variabel untuk menyimpan coroutine hide

    void Update()
    {
        // Deteksi input dari tombol keyboard untuk spawn
        if (Input.GetKeyDown(spawnKeyCode))
        {
            SpawnObjects();
        }

        // Deteksi input dari tombol keyboard untuk hide
        if (Input.GetKeyDown(hideKeyCode))
        {
            // Hentikan coroutine sebelumnya jika ada
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }
            // Jalankan coroutine baru untuk hide dengan jeda
            hideCoroutine = StartCoroutine(HideObjectsWithDelay());
        }
    }

    // Method untuk spawn semua objek dalam list
    private void SpawnObjects()
    {
        foreach (GameObject obj in objectsToSpawn)
        {
            if (obj != null)
            {
                Instantiate(obj, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Salah satu objek yang ingin di-spawn belum di-assign!");
            }
        }
    }

    // Coroutine untuk menyembunyikan semua objek dalam list setelah jeda
    private IEnumerator HideObjectsWithDelay()
    {
        yield return new WaitForSeconds(hideDelay);

        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Salah satu objek yang ingin di-hide belum di-assign!");
            }
        }
    }
}
