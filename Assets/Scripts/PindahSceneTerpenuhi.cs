using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PindahSceneTerpenuhi : MonoBehaviour
{
    public TargetInfo[] targets; // Array objek target yang akan dideteksi
    private int score = 0; // Variabel untuk menyimpan skor
    public TMP_Text scoreText; // Objek TMP_Text pada canvas UI untuk menampilkan skor
    public int targetScore; // Skor yang harus dicapai untuk memunculkan objek
    public string nextSceneName; // Nama scene berikutnya yang akan dimuat
    public AudioClip successSound; // Sound yang dimainkan ketika skor tercapai
    private AudioSource audioSource; // AudioSource untuk memainkan sound
    private bool isScoreReached = false; // Flag untuk memastikan WaitAndNextScene hanya dipanggil sekali
    public TMP_Text finishText; // Objek TMP_Text untuk menampilkan teks "Finish"

    // Fungsi Start dipanggil sekali saat script pertama kali dijalankan
    void Start()
    {
        // Pastikan scoreText tidak null
        if (scoreText != null)
        {
            UpdateScoreText(); // Perbarui teks skor pada canvas UI
        }

        // Setup AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        Debug.Log("AudioSource added.");

        // Sembunyikan teks "Finish" jika ada
        if (finishText != null)
        {
            finishText.gameObject.SetActive(false);
        }
    }

    // Fungsi Update dipanggil sekali per frame
    void Update()
    {
        // Loop melalui semua target di array targets
        foreach (TargetInfo targetInfo in targets)
        {
            // Periksa apakah target tidak null dan belum terdeteksi
            if (targetInfo.target != null && !targetInfo.detected)
            {
                DetectTarget(targetInfo); // Panggil fungsi untuk mendeteksi target
            }
        }
        UpdateScoreText(); // Perbarui teks skor pada canvas UI

        // Periksa apakah skor telah mencapai target
        // if (score >= targetScore && !isScoreReached)
        // {
        //     Debug.Log("Score reached target! Attempting to transition to the next scene...");
        //     isScoreReached = true;
        //     PlaySuccessSound(); // Mainkan sound sukses
        //     ShowFinishText(); // Tampilkan teks "Finish"
        //     StartCoroutine(WaitAndNextScene(2f)); // Pindah ke scene berikutnya setelah 2 detik
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Object entered trigger: " + other.gameObject.name);

        // Loop melalui semua targetInfo di targets
        foreach (TargetInfo targetInfo in targets)
        {
            // Periksa apakah collider yang masuk adalah target dan belum terdeteksi
            if (other.gameObject == targetInfo.target.gameObject && !targetInfo.detected)
            {
                // Debug.Log("Target detected: " + targetInfo.target.name);

                // Tandai target sebagai terdeteksi
                targetInfo.detected = true;

                // Tambah skor
                AddScore(1);

                // Update teks skor
                UpdateScoreText();

                if (score >= targetScore && !isScoreReached)
                {
                    Debug.Log("Score reached target! Attempting to transition to the next scene...");
                    isScoreReached = true;
                    PlaySuccessSound(); // Mainkan sound sukses
                    ShowFinishText(); // Tampilkan teks "Finish"
                    
                    CoroutineHandler.Instance.StartCoroutine(WaitAndNextScene(2f));
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Debug.Log("Object exited trigger: " + other.gameObject.name);

        // Loop melalui semua targetInfo di targets
        foreach (TargetInfo targetInfo in targets)
        {
            // Periksa apakah collider yang keluar adalah target dan telah terdeteksi
            if (other.gameObject == targetInfo.target.gameObject && targetInfo.detected)
            {
                Debug.Log("Target lost: " + targetInfo.target.name);

                // Tandai target sebagai tidak terdeteksi
                targetInfo.detected = false;

                // Kurangi skor
                AddScore(-1);

                // Update teks skor
                UpdateScoreText();
            }
        }
    }

    IEnumerator WaitAndNextScene(float waitTime)
    {
        Debug.Log("WaitAndNextScene started");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Waktu tunggu selesai");

        LoadNextScene();
    }

    // Fungsi untuk mendeteksi target menggunakan raycast
    void DetectTarget(TargetInfo targetInfo)
    {
        // Buat ray dari posisi PindahSceneTerpenuhi ke posisi target
        Vector3 direction = targetInfo.target.position - transform.position; // Hitung arah dari posisi kamera ke target
        Ray ray = new Ray(transform.position, direction); // Buat ray dari posisi kamera ke arah target

        // Debug log untuk melihat arah raycast
        Debug.DrawRay(transform.position, direction, Color.red);

        // Periksa apakah terdapat tabrakan dengan objek target menggunakan raycast
        if (Physics.Raycast(ray, out RaycastHit hit, direction.magnitude))
        {
            // Periksa apakah objek yang ditabrak memiliki tag "Draggable"
            if (hit.collider.CompareTag("Draggable"))
            {
                // Debug log untuk melihat objek yang ditabrak
                Debug.Log($"Raycast hit: {hit.collider.name}");

                // Jika target terdeteksi, tambahkan skor dan set flag detected menjadi true
                AddScore(1); // Tambah skor
                targetInfo.detected = true; // Set flag detected menjadi true

                // Debug log untuk melihat skor yang ditambahkan
                Debug.Log($"Target detected: {targetInfo.target.name}, Score: {score}");
            }
        }
    }

    // Fungsi untuk menambahkan skor
    void AddScore(int points)
    {
        score += points; // Tambah poin ke skor
        Debug.Log($"Score updated: {score}");
    }

    // Fungsi untuk memperbarui teks skor pada canvas UI
    void UpdateScoreText()
    {
        if (scoreText != null) // Pastikan scoreText tidak null
        {
            scoreText.text = $"{score} / {targets.Length}"; // Perbarui teks skor pada canvas UI
        }
    }

    // Fungsi untuk memainkan sound sukses ketika skor tercapai
    void PlaySuccessSound()
    {
        if (successSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(successSound);
            Debug.Log("Success sound played.");
        }
    }

    // Fungsi untuk menampilkan teks "Finish"
    void ShowFinishText()
    {
        if (finishText != null)
        {
            Debug.Log("Showing finish text.");
            finishText.gameObject.SetActive(true);
            finishText.text = " ";
        }
        else
        {
            Debug.LogError("Finish text is not assigned.");
        }
    }

    // Fungsi untuk memuat scene berikutnya
    void LoadNextScene()
    {
        Debug.Log("Attempting to load next scene...");
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log($"Loading next scene: {nextSceneName}");
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is empty or null");
        }
    }
}
