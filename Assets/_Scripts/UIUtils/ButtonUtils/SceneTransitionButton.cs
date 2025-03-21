using UnityEngine;

public class SceneTransitionButton : ButtonUtil
{
    [SerializeField] private SceneUtils.SceneName targetScene;
    public override void Start()
    {
        base.Start();

        button.onClick.AddListener(TransitionScene);
    }
    private void TransitionScene()
    {
        DataMessenger.SetString(StringKey.NewSceneName, targetScene.ToString());
        DataMessenger.SetString(StringKey.PreviousSceneName, gameObject.scene.name);
        EventMessenger.TriggerEvent(EventKey.TransitionScene);
    }
}
