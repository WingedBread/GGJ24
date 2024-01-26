using UnityEngine;

public class SceneManagers : MonoBehaviour
{

    private ScenesManager _scenesManager;

    private void Start()
    {
        _scenesManager = FindFirstObjectByType<ScenesManager>();
    }

    public void ChangeScene(int sceneBuildIndex)
    {
        _scenesManager.ChangeScene(sceneBuildIndex);
    }

}
