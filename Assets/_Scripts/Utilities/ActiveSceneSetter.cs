using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveSceneSetter : MonoBehaviour
{
    void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
    }

}
