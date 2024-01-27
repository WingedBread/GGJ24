using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scenes config")]
    [SerializeField] private ScenesManager _scenesManager;
    [SerializeField] private List<int> _sceneBuildIndices;
    [SerializeField] private List<AudioClip> _transitionAudioClips;
    [SerializeField] private AudioClip _firstToSecondAudioClip;
    [SerializeField] private AudioClip _secondToThirdAudioClip;
    [SerializeField] private AudioClip _thirdToFourthAudioClip;

    private GameSceneManager _currentGameSceneManager;
    private int _currentScene;

    private void Start()
    {
        _currentScene = 0;
        _scenesManager.Initialize();
        _scenesManager.LoadInitialScene(_sceneBuildIndices[_currentScene]);
    }

    private void OnDestroy()
    {
        _scenesManager.Teardown();
    }

    public void InitializeScene()
    {
        _currentGameSceneManager = FindFirstObjectByType<GameSceneManager>();
        _currentGameSceneManager.InitializeScene();
    }

    public void StartScene()
    {
        StartCoroutine(_currentGameSceneManager.StartScene(this));
    }

    public void ChangeScene()
    {
        AudioClip transitionAudioClip = _transitionAudioClips[_currentScene];
        _currentScene += 1;
        _scenesManager.ChangeScene(_currentScene, transitionAudioClip);
    }
}
