// using UnityEngine;
// using UnityEngine.UI;

// [System.Serializable]
// public class TargetInfo
// {
//     public Transform target;
//     public bool detected; // Flag untuk menandai apakah target telah terdeteksi
// }

// public class RaycastDetection : MonoBehaviour
// {
//     public TargetInfo[] targets; // Array objek target yang akan dideteksi
//     public LayerMask pinkLayer; // Layer untuk deteksi tas
//     public LayerMask hijauLayer; // Layer untuk deteksi sandal
//     public LayerMask cokelatLayer; // Layer untuk deteksi laptop

//     private Renderer cubeRenderer;
//     private int score = 0;
//     public Text scoreText; // Objek Text pada canvas UI

//     void Start()
//     {
//         cubeRenderer = GetComponent<Renderer>();

//         // Atur kubus menjadi transparan (optional, jika belum diatur)
//         if (cubeRenderer != null)
//         {
//             cubeRenderer.material.color = new Color(1, 1, 1, 0.3f); // Set transparansi
//         }
//     }

//     void Update()
//     {
//         foreach (TargetInfo targetInfo in targets)
//         {
//             if (targetInfo.target != null && !targetInfo.detected) // Periksa apakah target tidak null dan belum terdeteksi
//             {
//                 DetectTarget(targetInfo);
//             }
//         }
//         UpdateScoreText(); // Perbarui teks pada canvas UI
//     }

//     void DetectTarget(TargetInfo targetInfo)
//     {
//         // Buat ray dari posisi kubus ke posisi target
//         Vector3 direction = targetInfo.target.position - transform.position;
//         Ray ray = new Ray(transform.position, direction);

//         // Buat array untuk menyimpan semua hit yang ditemukan oleh Physics.RaycastAll
//         RaycastHit[] hits = Physics.RaycastAll(ray, direction.magnitude);

//         // Periksa setiap hit
//         foreach (RaycastHit hit in hits)
//         {
//             if (hit.collider.transform == targetInfo.target)
//             {
//                 int layer = hit.collider.gameObject.layer;

//                 switch (layer)
//                 {
//                     case 8: // Bag Layer
//                         Debug.Log("Detected: pink (Type 1)");
//                         // Tambahkan logika untuk tas
//                         AddScore(1); // Tambahkan skor +1 jika tas terdeteksi
//                         targetInfo.detected = true; // Set flag detected menjadi true
//                         break;
//                     case 9: // Sandal Layer
//                         Debug.Log("Detected: hijau (Type 2)");
//                         // Tambahkan logika untuk sandal
//                         AddScore(1); // Tambahkan skor +1 jika sandal terdeteksi
//                         targetInfo.detected = true; // Set flag detected menjadi true
//                         break;
//                     case 10: // Laptop Layer
//                         Debug.Log("Detected: cokelat (Type 3)");
//                         // Tambahkan logika untuk laptop
//                         AddScore(1); // Tambahkan skor +1 jika laptop terdeteksi
//                         targetInfo.detected = true; // Set flag detected menjadi true
//                         break;
//                     default:
//                         Debug.Log("Detected: Unknown object");
//                         break;
//                 }
//             }
//         }
//     }

//     void AddScore(int points)
//     {
//         score += points;
//     }

//     void UpdateScoreText()
//     {
//         if (scoreText != null) // Pastikan scoreText tidak null
//         {
//             scoreText.text = "Score: " + score.ToString(); // Perbarui teks score pada canvas UI
//         }
//     }
// }
