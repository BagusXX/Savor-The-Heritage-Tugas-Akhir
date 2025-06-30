using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color normalColor = Color.white; // Warna default teks
    public Color hoverColor = Color.yellow; // Warna teks saat di-hover
    private TMP_Text buttonText;

    void Start()
    {
        // Mendapatkan komponen TMP_Text pada tombol
        buttonText = GetComponentInChildren<TMP_Text>();
        buttonText.color = normalColor; // Set warna default saat start
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Ubah warna teks saat kursor masuk ke area tombol
        buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Kembalikan warna teks ke warna default saat kursor keluar dari area tombol
        buttonText.color = normalColor;
    }
}
