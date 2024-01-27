using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ScenesManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _blackoutImage;
    [SerializeField, Range(0.0f, 2.0f)]
    float _blackoutAlphaDuration;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioListener _audioListener;

    private GameManager _gameManager;
    private Scene _currentScene;
    private int _sceneToLoadBuildIndex;
    private bool _isInitialSceneLoad;
    private bool _loadingComplete;
    private bool _audiosComplete;

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public void Teardown()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void LoadInitialScene(int initialSceneBuildIndex)
    {
        _isInitialSceneLoad = true;
        SceneManager.LoadSceneAsync(initialSceneBuildIndex, LoadSceneMode.Additive);
    }

    public void ChangeScene(int sceneBuildIndex, AudioClip transitionAudioClip)
    {
        _loadingComplete = false;
        _audiosComplete = false;
        _sceneToLoadBuildIndex = sceneBuildIndex;
        _blackoutImage.DOFade(1.0f, _blackoutAlphaDuration).OnComplete(OnFadeCompleted);
    }

    private void OnFadeCompleted()
    {
        SceneManager.UnloadSceneAsync(_currentScene);

        _audioListener.enabled = true;
        _audioSource.Play();
        DOTween.Sequence().AppendInterval(_audioSource.clip.length).AppendCallback(OnClipCompleted);
    }

    private void OnSceneUnloaded(Scene unloadedScene)
    {
        SceneManager.LoadSceneAsync(_sceneToLoadBuildIndex, LoadSceneMode.Additive);
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        _currentScene = loadedScene;
        SceneManager.SetActiveScene(loadedScene);
        _gameManager.InitializeScene();

        if (_isInitialSceneLoad)
        {
            _isInitialSceneLoad = false;
            return;
        }

        _loadingComplete = true;
        if (_audiosComplete)
            StartSceneAfterFadeOut();
    }

    private void OnClipCompleted()
    {
        _audioListener.enabled = false;
        _audiosComplete = true;
        if (_loadingComplete)
            StartSceneAfterFadeOut();
    }

    private void StartSceneAfterFadeOut()
    {
        _blackoutImage.DOFade(0.0f, _blackoutAlphaDuration).OnComplete(() => _gameManager.StartScene());
    }
}
