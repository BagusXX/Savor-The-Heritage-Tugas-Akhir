using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color normalColor = Color.white; // Warna default tombol
    public Color hoverColor = Color.yellow; // Warna saat di-hover
    private Button button;
    private Image buttonImage;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();
        buttonImage.color = normalColor; // Set warna default saat start
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Ubah warna saat kursor masuk ke area tombol
        buttonImage.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Kembalikan warna ke warna default saat kursor keluar dari area tombol
        buttonImage.color = normalColor;
    }
}
