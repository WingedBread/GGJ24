using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scenes config")]
    [SerializeField] private ScenesManager _scenesManager;
    [SerializeField] private List<int> _sceneBuildIndices;
    [SerializeField] private List<AudioClip> _transitionAudioClips;

    private GameSceneManager _currentGameSceneManager;
    private int _currentScene;

    private void Start()
    {
        _currentScene = 0;
        _scenesManager.Initialize(this);
        _scenesManager.LoadInitialScene(_sceneBuildIndices[_currentScene]);
    }

    private void OnDestroy()
    {
        _scenesManager.Teardown();
    }

    public void InitializeScene()
    {
        _currentGameSceneManager = FindFirstObjectByType<GameSceneManager>();
        _currentGameSceneManager.InitializeScene(this);
    }

    public void StartScene()
    {
        StartCoroutine(_currentGameSceneManager.StartScene());
    }

    public void ChangeScene()
    {
        AudioClip transitionAudioClip = null;
        if (_currentScene < _transitionAudioClips.Count) transitionAudioClip = _transitionAudioClips[_currentScene];
        _currentScene += 1;
        _currentScene %= _sceneBuildIndices.Count;
        _scenesManager.ChangeScene(_sceneBuildIndices[_currentScene], transitionAudioClip);
    }
}
