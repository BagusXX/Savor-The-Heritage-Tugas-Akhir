// using UnityEngine;
// using UnityEngine.UI;

// public class muteSound1 : MonoBehaviour
// {
//     public AudioSource audioSource; // AudioSource yang digunakan untuk memutar suara
//     public Sprite soundOnSprite; // Sprite untuk ikon saat suara aktif
//     public Sprite soundOffSprite; // Sprite untuk ikon saat suara dimute
//     public Image soundButtonImage; // Image untuk tombol suara

//     private bool isMuted = false; // Status mute/unmute

//     [SerializeField]
//     private KeyCode toggleMuteKey = KeyCode.M; // Key untuk toggle mute/unmute, default adalah M

//     void Start()
//     {
//         // Inisialisasi ikon tombol suara
//         UpdateSoundButtonImage();
//     }

//     void Update()
//     {
//         // Memeriksa jika tombol toggleMuteKey ditekan
//         if (Input.GetKeyDown(toggleMuteKey))
//         {
//             ToggleSound();
//             ToggleObjectVisibility();
//         }
//     }

//     public void ToggleSound()
//     {
//         isMuted = !isMuted;

//         // Mengatur mute/unmute audio berdasarkan status isMuted
//         audioSource.mute = isMuted;

//         // Update ikon tombol suara
//         UpdateSoundButtonImage();
//     }

//     void UpdateSoundButtonImage()
//     {
//         // Memilih sprite yang tepat berdasarkan status isMuted
//         soundButtonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
//     }

//     void ToggleObjectVisibility()
//     {
//         // Memeriksa apakah objectToShowHide sedang aktif atau tidak, kemudian membalikkan statusnya
//         if (objectToShowHide != null)
//         {
//             objectToShowHide.SetActive(!objectToShowHide.activeSelf);
//         }
//     }
// }
