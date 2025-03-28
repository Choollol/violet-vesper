using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    private const string TESTING_SCENE_NAME = "Testing";

    private InputAction cancelAction;
    private InputAction testSceneAction;
    private InputAction continueAction;

    private void Start()
    {
        cancelAction = InputSystem.actions.FindAction("Cancel");
        testSceneAction = InputSystem.actions.FindAction("TestScene");
        continueAction = InputSystem.actions.FindAction("Continue");

        cancelAction.performed += HandleCancel;
        testSceneAction.performed += HandleTestScene;
        continueAction.performed += HandleContinue;
    }
    private void HandleCancel(InputAction.CallbackContext context)
    {
        if (DataMessenger.GetBool(BoolKey.IsScreenTransitioning) || !DataMessenger.GetBool(BoolKey.CanOpenMenu))
        {
            return;
        }

        // Check if a menu is open
        if (DataMessenger.GetBool(BoolKey.IsMenuOpen))
        {
            EventMessenger.TriggerEvent(EventKey.MenuGoBack);
        }
        // If no menu is open, open the pause menu
        else if (DataMessenger.GetBool(BoolKey.IsGameActive))
        {
            EventMessenger.TriggerEvent(EventKey.OpenPauseMenu);
        }
    }
    private void HandleTestScene(InputAction.CallbackContext context)
    {
        if (DataMessenger.GetBool(BoolKey.IsScreenTransitioning) || !DataMessenger.GetBool(BoolKey.CanOpenMenu))
        {
            return;
        }

        if (DataMessenger.GetBool(BoolKey.IsMenuOpen))
        {
            EventMessenger.TriggerEvent(EventKey.CloseMenu);
        }
        else if (DataMessenger.GetBool(BoolKey.IsGameActive))
        {
            SceneManager.LoadSceneAsync(TESTING_SCENE_NAME, LoadSceneMode.Additive);
        }
    }
    private void HandleContinue(InputAction.CallbackContext context)
    {
        EventMessenger.TriggerEvent(EventKey.Continue, true);
    }
}
