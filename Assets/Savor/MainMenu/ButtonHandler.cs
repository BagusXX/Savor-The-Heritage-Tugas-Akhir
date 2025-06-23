using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public SceneTransition sceneTransition;

    public void OnPlayButtonClicked()
    {
        sceneTransition.FadeToScene("CutScene");  
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
