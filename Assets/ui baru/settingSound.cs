using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SettingSound : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Image soundButtonImage;

    public GameObject objectToShowHide; // GameObject yang ingin ditampilkan atau disembunyikan

    private bool isMuted = false;

    [SerializeField]
    private KeyCode toggleMuteKey = KeyCode.M; // Default key is M, can be changed in inspector

    void Start()
    {
        // Inisialisasi ikon tombol suara
        UpdateSoundButtonImage();
    }

    void Update()
    {
        // Memeriksa jika tombol toggleMuteKey ditekan
        if (Input.GetKeyDown(toggleMuteKey))
        {
            ToggleSound();
            ToggleObjectVisibility();
        }
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        videoPlayer.SetDirectAudioMute(0, isMuted); // Mute/unmute audio track 0 (video's audio track)

        // Update ikon tombol suara
        UpdateSoundButtonImage();
    }

    void UpdateSoundButtonImage()
    {
        if (isMuted)
        {
            soundButtonImage.sprite = soundOffSprite; // Gambar saat sound off
        }
        else
        {
            soundButtonImage.sprite = soundOnSprite; // Gambar saat sound on
        }
    }

    void ToggleObjectVisibility()
    {
        // Memeriksa apakah objectToShowHide sedang aktif atau tidak, kemudian membalikkan statusnya
        objectToShowHide.SetActive(!objectToShowHide.activeSelf);
    }
}
