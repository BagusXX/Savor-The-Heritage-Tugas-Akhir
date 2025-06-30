using UnityEngine;
using TMPro;

// Class untuk menyimpan informasi target
[System.Serializable]
public class TargetInfo
{
    public Transform target; // Transform dari target
    public bool detected; // Flag untuk menandai apakah target telah terdeteksi
}

public class RaycastDetection : MonoBehaviour
{
    public TargetInfo[] targets; // Array objek target yang akan dideteksi
    private int score = 0; // Variabel untuk menyimpan skor
    public TMP_Text scoreText; // Objek TMP_Text pada canvas UI untuk menampilkan skor
    public int targetScore = 3; // Skor yang harus dicapai untuk memunculkan objek
    public GameObject[] objectsToAppear; // Array objek yang akan muncul
    public GameObject[] objectsToDisappear; // Array objek yang akan menghilang
    public TMP_Text finishText; // Objek TMP_Text untuk menampilkan teks "selesai"
    public LayerMask targetLayer; // Layer mask untuk objek target
    public string targetTag = "Draggable"; // Tag objek target yang akan dideteksi

    // Fungsi Start dipanggil sekali saat script pertama kali dijalankan
    void Start()
    {
        // Pastikan scoreText tidak null
        if (scoreText != null)
        {
            // UpdateScoreText(); // Perbarui teks skor pada canvas UI
            foreach(TargetInfo target in targets)
            {
                if(target.detected == true)
                {
                    AddScore(1);
                    UpdateScoreText();
                }
            }
        }

        // Sembunyikan semua objek yang akan muncul
        foreach (GameObject obj in objectsToAppear)
        {
            obj.SetActive(false);
        }

        // Sembunyikan teks "selesai"
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
                // DetectTarget(targetInfo); // Panggil fungsi untuk mendeteksi target
            }
        }
        // UpdateScoreText(); // Perbarui teks skor pada canvas UI
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

                if (score >= targetScore)
                {
                    AppearObjects(); // Munculkan objek
                    DisappearObjects(); // Hilangkan objek
                    ShowFinishText(); // Tampilkan teks "selesai"
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

    // Fungsi untuk mendeteksi target menggunakan raycast
    void DetectTarget(TargetInfo targetInfo)
    {
        // Buat ray dari posisi `RaycastDetection` ke posisi target
        Vector3 direction = targetInfo.target.position - transform.position; // Hitung arah dari posisi kamera ke target
        Ray ray = new Ray(transform.position, direction); // Buat ray dari posisi kamera ke arah target

        // Debug log untuk melihat arah raycast
        Debug.DrawRay(transform.position, direction, Color.red);

        // Periksa apakah terdapat tabrakan dengan objek target menggunakan raycast
        if (Physics.Raycast(ray, out RaycastHit hit, direction.magnitude, targetLayer))
        {
            // Periksa apakah objek yang ditabrak memiliki tag yang sesuai
            if (hit.collider.CompareTag(targetTag))
            {
                // Jika target terdeteksi, tambahkan skor dan set flag detected menjadi true
                AddScore(1); // Tambah skor
                targetInfo.detected = true; // Set flag detected menjadi true

                // Nonaktifkan objek target
                targetInfo.target.gameObject.SetActive(false);

                // Debug log untuk melihat skor yang ditambahkan
                Debug.Log($"Target detected: {targetInfo.target.name}, Score: {score}");
            }
        }
    }

    // Fungsi untuk menambahkan skor
    void AddScore(int points)
    {
        score += points; // Tambah poin ke skor
    }

    // Fungsi untuk memperbarui teks skor pada canvas UI
    void UpdateScoreText()
    {
        if (scoreText != null) // Pastikan scoreText tidak null
        {
            scoreText.text = $"{score} / {targets.Length}"; // Perbarui teks skor pada canvas UI
        }
    }

    // Fungsi untuk memunculkan objek-objek ketika skor tercapai
    void AppearObjects()
    {
        foreach (GameObject obj in objectsToAppear)
        {
            obj.SetActive(true);
        }
    }

    // Fungsi untuk menghilangkan objek-objek ketika skor tercapai
    void DisappearObjects()
    {
        foreach (GameObject obj in objectsToDisappear)
        {
            obj.SetActive(false);
        }
    }

    // Fungsi untuk menampilkan teks "selesai" ketika skor tercapai
    void ShowFinishText()
    {
        if (finishText != null)
        {
            finishText.gameObject.SetActive(true);
            finishText.text = "Selesai";
        }
    }
}