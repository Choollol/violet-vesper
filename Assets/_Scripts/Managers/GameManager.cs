using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private const string PAUSE_MENU_SCENE_NAME = "Pause_Menu";
    private const string TESTING_SCENE_NAME = "Testing";
    private const string TITLE_SCREEN_SCENE_NAME = "Ocean_Start";

    /*private InputAction cancelAction;
    private InputAction testSceneAction;*/

    private void OnEnable()
    {
        EventMessenger.StartListening(EventKey.PauseGame, PauseGame);
        EventMessenger.StartListening(EventKey.UnpauseGame, UnpauseGame);

        EventMessenger.StartListening(EventKey.MenuOpened, MenuOpened);
        EventMessenger.StartListening(EventKey.MenuClosed, MenuClosed);

        EventMessenger.StartListening(EventKey.TransitionScene, TransitionScene);

        EventMessenger.StartListening(EventKey.OpenPauseMenu, OpenPauseMenu);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(EventKey.PauseGame, PauseGame);
        EventMessenger.StopListening(EventKey.UnpauseGame, UnpauseGame);

        EventMessenger.StopListening(EventKey.MenuOpened, MenuOpened);
        EventMessenger.StopListening(EventKey.MenuClosed, MenuClosed);

        EventMessenger.StopListening(EventKey.TransitionScene, TransitionScene);
        
        EventMessenger.StartListening(EventKey.OpenPauseMenu, OpenPauseMenu);
    }
    private void Start()
    {
        /*cancelAction = InputSystem.actions.FindAction("Cancel");
        testSceneAction = InputSystem.actions.FindAction("TestScene");

        cancelAction.performed += CancelPressed;
        testSceneAction.performed += TestScenePressed;*/

        if (!Application.isEditor)
        {
            SceneManager.LoadSceneAsync(TITLE_SCREEN_SCENE_NAME, LoadSceneMode.Additive);
        }

        DataMessenger.SetBool(BoolKey.CanOpenMenu, true);
        DataMessenger.SetBool(BoolKey.IsGameActive, true);
    }
    /*private void CancelPressed(InputAction.CallbackContext context)
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
            OpenPauseMenu();
        }
    }
    private void TestScenePressed(InputAction.CallbackContext context)
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
    }*/
    private void TransitionScene()
    {
        StartCoroutine(HandleTransitionScene(DataMessenger.GetString(StringKey.NewSceneName),
            DataMessenger.GetString(StringKey.PreviousSceneName)));
    }

    private IEnumerator HandleTransitionScene(string newScene, string oldScene = "")
    {
        // Messenger work
        DataMessenger.SetBool(BoolKey.IsSceneTransitioning, true);
        DataMessenger.SetBool(BoolKey.IsGameActive, false);

        if (DataMessenger.GetBool(BoolKey.IsMenuOpen))
        {
            EventMessenger.TriggerEvent(EventKey.CloseMenu);
        }

        AsyncOperation loadTransition =  
            SceneManager.LoadSceneAsync(SceneUtils.SceneName.Transition_Scene.ToString(), LoadSceneMode.Additive);

        while (!loadTransition.isDone) yield return null;

        // Wait for transition screen
        yield return DataMessenger.WaitForBool(BoolKey.IsScreenTransitioning);

        if (!string.IsNullOrEmpty(oldScene))
        {
            AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(oldScene);
            while (!unloadScene.isDone) yield return null;
        }

        AsyncOperation loadScene = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        while (!loadScene.isDone) yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));

        // Fade transition out and trigger events
        EventMessenger.TriggerEvent(EventKey.EndScreenTransition);

        // Wait for transition screen
        yield return DataMessenger.WaitForBool(BoolKey.IsScreenTransitioning);

        // Messenger work
        DataMessenger.SetBool(BoolKey.IsGameActive, true);

        DataMessenger.SetBool(BoolKey.IsSceneTransitioning, false);

        yield break;
    }

    private void OpenPauseMenu()
    {
        SceneManager.LoadSceneAsync(PAUSE_MENU_SCENE_NAME, LoadSceneMode.Additive);
        PauseGame();
    }
    private void PauseGame()
    {
        DataMessenger.SetBool(BoolKey.IsGameActive, false);
        Time.timeScale = 0;
    }
    private void UnpauseGame()
    {
        DataMessenger.SetBool(BoolKey.IsGameActive, true);
        Time.timeScale = 1;
    }
    public void MenuOpened()
    {
        DataMessenger.SetBool(BoolKey.IsMenuOpen, true);
        DataMessenger.SetBool(BoolKey.IsGameActive, false);
    }
    public void MenuClosed()
    {
        DataMessenger.SetBool(BoolKey.IsMenuOpen, false);
        DataMessenger.SetBool(BoolKey.IsGameActive, true);
    }
}
