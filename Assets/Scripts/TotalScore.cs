// using UnityEngine;
// using UnityEngine.UI;

// public class TotalScore : MonoBehaviour
// {
//     public Text totalScoreText; // Objek UI Text untuk menampilkan total skor

//     void Start()
//     {
//         if (totalScoreText != null)
//         {
//             HitungTotalSkor();
//         }
//     }

//     void HitungTotalSkor()
//     {
//         int totalSkor = 0;

//         // Dapatkan semua skrip RaycastDetection dalam scene
//         RaycastDetection[] deteksiRaycast = FindObjectsOfType<RaycastDetection>();

//         // Loop melalui setiap skrip RaycastDetection dan tambahkan skornya ke total skor
//         foreach (RaycastDetection deteksi in deteksiRaycast)
//         {
//             totalSkor += deteksi.DapatkanTotalSkor();
//         }

//         // Perbarui teks pada UI dengan total skor yang dihitung
//         totalScoreText.text = "Total Skor: " + totalSkor.ToString();
//     }
// }
