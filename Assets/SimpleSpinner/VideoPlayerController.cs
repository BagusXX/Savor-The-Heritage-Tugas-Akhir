// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.Video;

// public class videoPlayer : MonoBehaviour
// {
//     public VideoPlayer videoPlayer;
//     public string nextSceneName; // Nama scene yang akan dituju setelah video selesai

//     void Start()
//     {
//         videoPlayer.loopPointReached += EndReached;
//     }

//     void EndReached(VideoPlayer vp)
//     {
//         StartCoroutine(PlayNextScene());
//     }

//     IEnumerator PlayNextScene()
//     {
//         // Tunggu beberapa detik sebelum berpindah scene (opsional)
//         yield return new WaitForSeconds(2f);

//         // Load scene yang dituju
//         SceneManager.LoadScene(nextSceneName);
//     }
// }
