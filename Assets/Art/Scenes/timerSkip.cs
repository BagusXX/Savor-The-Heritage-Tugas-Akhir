using UnityEngine;
using TMPro;

public class TextMeshProAppearAfterDelay : MonoBehaviour
{
    public float delayTime = 10f; // Waktu delay sebelum teks muncul
    public TMP_Text textObject; // Objek teks TextMeshPro

    private float timer; // Timer untuk menghitung waktu

    void Start()
    {
        timer = 0f; // Mulai timer
        textObject.enabled = false; // Pastikan teks tidak terlihat pada awalnya
    }

    void Update()
    {
        timer += Time.deltaTime; // Menghitung waktu berjalan

        // Jika waktu sudah mencapai delayTime, tampilkan teks
        if (timer >= delayTime)
        {
            textObject.enabled = true; // Aktifkan teks
            enabled = false; // Matikan script ini setelah teks muncul (opsional)
        }
    }
}
