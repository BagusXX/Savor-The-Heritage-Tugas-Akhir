using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // Import this to work with UI and mouse events

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] MenuButtonController menuButtonController; // If not needed, remove
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;
    [SerializeField] string sceneToLoad;
    [SerializeField] bool isQuitButton = false;
    [SerializeField] bool isTutorialButton = false; // Added option for tutorial button

    // Reference for LoadingScreenController
    public LoadingScreenController loadingScreenController;

    private bool isSelected = false;

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            animator.SetBool("selected", true);
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }

    // Method to handle mouse pointer entering the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        isSelected = true;
    }

    // Method to handle mouse pointer exiting the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        isSelected = false;
    }

    // Method to handle mouse clicks on the button
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected)
        {
            animator.SetBool("pressed", true);
            animatorFunctions.disableOnce = true;

            if (isQuitButton)
            {
                #if UNITY_EDITOR
                EditorApplication.isPlaying = false; // For stopping play mode in editor
                Debug.Log("Quit Game!"); // For testing in editor
                #else
                Application.Quit(); // For quitting the application in build
                #endif
            }
            else if (isTutorialButton)
            {
                // Call method to load TUTORIAL scene
                LoadSceneWithLoadingScreen("TUTORIAL");
            }
            else
            {
                // Call method to load scene with specified name
                LoadSceneWithLoadingScreen(sceneToLoad);
            }
        }
    }

    // Method to load scene with loading screen
    void LoadSceneWithLoadingScreen(string sceneName)
    {
        if (loadingScreenController != null)
        {
            // Call LoadScene method from loadingScreenController
            loadingScreenController.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("LoadingScreenController reference is missing!");
        }
    }
}
