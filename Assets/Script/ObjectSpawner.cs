using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text interactionText; // Referensi ke UI Text untuk menampilkan pesan interaksi

    void Start()
    {
        // Sembunyikan teks interaksi saat awal
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Saat objek memasuki collider
        if (other.CompareTag("Player"))
        {
            // Tampilkan teks interaksi
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
                interactionText.text = "Tekan P untuk membuka atau menutup pintu";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Saat objek keluar dari collider
        if (other.CompareTag("Player"))
        {
            // Sembunyikan teks interaksi kembali
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }
}
