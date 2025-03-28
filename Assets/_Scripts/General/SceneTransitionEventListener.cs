using UnityEngine;

public class SceneTransitionEventListener : MonoBehaviour
{
    [SerializeField] private string sceneTransitionEvent;

    [SerializeField] private SceneUtils.SceneName targetScene;

    private void OnEnable()
    {
        EventMessenger.StartListening(sceneTransitionEvent, TransitionScene);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(sceneTransitionEvent, TransitionScene);
    }

    private void TransitionScene()
    {
        DataMessenger.SetString(StringKey.NewSceneName, targetScene.ToString());
        DataMessenger.SetString(StringKey.PreviousSceneName, gameObject.scene.name);
        EventMessenger.TriggerEvent(EventKey.TransitionScene);
    }
}
