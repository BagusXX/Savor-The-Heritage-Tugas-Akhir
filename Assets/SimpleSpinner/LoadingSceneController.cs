using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneNameToLoad = "TUTORIAL"; // Default scene name, can be changed in the inspector

    public LoadingScreenController loadingScreenController;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    public void OnPlayButtonPressed()
    {
        StartCoroutine(LoadSceneWithDelay(2f)); // Start coroutine with 2-second delay
    }

    public void OnSettingsButtonPressed()
    {
        settingsPanel.SetActive(true);
    }

    public void OnCreditsButtonPressed()
    {
        creditsPanel.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    private IEnumerator LoadSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        loadingScreenController.LoadScene(sceneNameToLoad); // Load the scene
    }
}
