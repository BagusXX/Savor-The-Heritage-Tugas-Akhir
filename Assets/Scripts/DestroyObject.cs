using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Tambahkan ini untuk menggunakan SceneManager

public class DestroyOnProximityAndKeyPress : MonoBehaviour
{
    public ParticleSystem destructionEffect; // Referensi ke efek partikel
    public AudioClip destructionSound; // Suara yang akan dimainkan saat objek dihancurkan
    private AudioSource audioSource; // Komponen AudioSource untuk memainkan suara
    private KeyCode keyToPress = KeyCode.Return; // Tetapkan tombol Enter (Return) sebagai keyToPress
    public float destroyRadius = 5.0f; // Radius untuk mendeteksi pemain
    private bool isPlayerInRange = false;

    // Tambahkan referensi ke teks UI untuk menampilkan skor
    public TMP_Text scoreText;
    private static int score = 0;
    public int targetScore = 5; // Skor yang dibutuhkan untuk berpindah scene

    void Start()
    {
        // Dapatkan komponen AudioSource dari objek ini
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Asumsikan pemain adalah objek dengan tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Periksa apakah pemain berada dalam radius tertentu
            if (distanceToPlayer <= destroyRadius)
            {
                isPlayerInRange = true;
            }
            else
            {
                isPlayerInRange = false;
            }

            // Aktifkan destroy hanya jika pemain berada dalam radius dan tombol Enter ditekan
            if (isPlayerInRange && Input.GetKeyDown(keyToPress))
            {
                DestroyObject();
            }
        }
    }

    void DestroyObject()
    {
        // Memainkan efek partikel jika telah ditentukan
        if (destructionEffect != null)
        {
            Instantiate(destructionEffect, transform.position, transform.rotation);
        }

        // Memainkan suara hancur jika telah ditentukan
        if (destructionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(destructionSound);
        }

        // Tambahkan skor
        AddScore(1);

        // Hancurkan objek
        Destroy(gameObject);
    }

    void AddScore(int points)
    {
        score += points;
        UpdateScoreText();

        // Periksa apakah skor telah mencapai target
        if (score >= targetScore)
        {
            // Pindah ke scene "level 3"
            SceneManager.LoadScene("CutSceneLevel3");
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null) // Pastikan scoreText tidak null
        {
            scoreText.text = score.ToString() + " / " + targetScore.ToString(); // Perbarui teks score pada canvas UI
        }
    }

    void OnDrawGizmosSelected()
    {
        // Gambar lingkaran di sekitar objek ini untuk menunjukkan radius deteksi di editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, destroyRadius);
    }
}
