using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.TestTransition, TestTransition);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.TestTransition, TestTransition);
    }

    private void TestTransition()
    {
        DataMessenger.SetString(StringKey.NewSceneName, SceneManager.GetActiveScene().name);
        DataMessenger.SetString(StringKey.PreviousSceneName, SceneManager.GetActiveScene().name);
        EventMessenger.TriggerEvent(EventKey.TransitionScene);
    }
}
