using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeyToContinue : MonoBehaviour
{
    public string nextSceneName; // Nama scene yang akan dituju

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
