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
    private AudioClip _audioClip;

    private Scene _currentScene;

    private int _sceneToLoadBuildIndex;
    private bool _firstSceneLoad;

    private bool _loadingComplete;
    private bool _audiosComplete;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        _firstSceneLoad = true;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive); // TODO: Make these proper constants
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void ChangeScene(int sceneBuildIndex)
    {
        _loadingComplete = false;
        _audiosComplete = false;
        _sceneToLoadBuildIndex = sceneBuildIndex;
        _blackoutImage.DOFade(1.0f, _blackoutAlphaDuration).OnComplete(OnFadeCompleted);
    }

    private void OnFadeCompleted()
    {
        SceneManager.UnloadSceneAsync(_currentScene);

        _audioSource.clip = _audioClip;
        _audioSource.Play();
        DOTween.Sequence().AppendInterval(_audioClip.length).AppendCallback(OnClipCompleted);
    }

    private void OnSceneUnloaded(Scene unloadedScene)
    {
        SceneManager.LoadSceneAsync(_sceneToLoadBuildIndex, LoadSceneMode.Additive);
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        _currentScene = loadedScene;
        SceneManager.SetActiveScene(loadedScene);

        if (_firstSceneLoad)
        {
            _firstSceneLoad = false;
            return;
        }

        _loadingComplete = true;
        if (_audiosComplete)
            _blackoutImage.DOFade(0.0f, _blackoutAlphaDuration);
    }

    private void OnClipCompleted()
    {
        _audiosComplete = true;
        if (_loadingComplete)
            _blackoutImage.DOFade(0.0f, _blackoutAlphaDuration);
    }
}
