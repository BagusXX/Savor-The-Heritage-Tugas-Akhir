using UnityEngine;
using UnityEngine.UI;

public class MuteSound1 : MonoBehaviour
{
    public AudioSource audioSource; // Change from VideoPlayer to AudioSource
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Image soundButtonImage;

    public GameObject objectToShowHide; // GameObject to be shown or hidden

    private bool isMuted = false;

    [SerializeField]
    private KeyCode toggleMuteKey = KeyCode.M; // Default key is M, can be changed in inspector

    void Start()
    {
        // Initialize sound button icon
        UpdateSoundButtonImage();
    }

    void Update()
    {
        // Check if the toggleMuteKey is pressed
        if (Input.GetKeyDown(toggleMuteKey))
        {
            ToggleSound();
            ToggleObjectVisibility();
        }
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        audioSource.mute = isMuted; // Mute/unmute the audio source

        // Update sound button icon
        UpdateSoundButtonImage();
    }

    void UpdateSoundButtonImage()
    {
        if (isMuted)
        {
            soundButtonImage.sprite = soundOffSprite; // Image when sound is off
        }
        else
        {
            soundButtonImage.sprite = soundOnSprite; // Image when sound is on
        }
    }

    void ToggleObjectVisibility()
    {
        // Check if the objectToShowHide is active, then toggle its status
        objectToShowHide.SetActive(!objectToShowHide.activeSelf);
    }
}
