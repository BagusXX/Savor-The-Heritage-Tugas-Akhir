using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public SceneTransition sceneTransition;

    public void OnPlayButtonClicked()
    {
        sceneTransition.FadeToScene("CutSceneLevel1");  
    }

    public void OnTrainingButtonClicked()
    {
        sceneTransition.FadeToScene("/");  
    }

    public void OnExitButtonClicked()
    {
        sceneTransition.QuitApplication();
    }
}
