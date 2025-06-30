// // File name: VideoPlayerController.cs

// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.Video;
// using System.Collections;

// public class VideoPlayerController : MonoBehaviour
// {
//     public VideoPlayer videoPlayerComponent;
//     public string nextSceneName;

//     void Start()
//     {
//         videoPlayerComponent.loopPointReached += EndReached;
//     }

//     void EndReached(VideoPlayer vp)
//     {
//         StartCoroutine(PlayNextScene());
//     }

//     IEnumerator PlayNextScene()
//     {
//         yield return new WaitForSeconds(2f);
//         SceneManager.LoadScene(nextSceneName);
//     }
// }
