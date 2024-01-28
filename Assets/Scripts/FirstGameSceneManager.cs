using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FirstGameSceneManager : GameSceneManager
{
    [Header("Game entities")]
    [SerializeField] private PhoneManager _phoneManager;
    [SerializeField] private InteractableBehaviour _phoneInteractable;
    [SerializeField] private InteractableBehaviour _keyInteractable;
    [SerializeField] private InteractableBehaviour _boxInteractable;
    [SerializeField] private InteractableBehaviour _shoesInteractable;

    [SerializeField] private CanvasGroup _moveWithWasd;
    [SerializeField] private CanvasGroup _interactWithE;


    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _suspiroAudioClip;

    private Coroutine _phoneCoroutine;
    private bool _moveHintCompleted;
    private bool _interactHintCompleted;
    private bool _firstMessageCompleted;
    private bool _secondMessageCompleted;
    private bool _boxOpened;

    public override void InitializeScene(GameManager gameManager)
    {
        base.InitializeScene(gameManager);
        Debug.Log("Initialize first scene");
        _phoneManager.Initialize();
        _phoneManager.SetAct1State(true);
        _firstMessageCompleted = false;
        _boxOpened = false;
        _moveHintCompleted = false;
    }

    public override IEnumerator StartScene()
    {
        Debug.Log("Start first scene");
        DOTween.Sequence().Append(_moveWithWasd.DOFade(1.0f, 0.5f)).AppendInterval(5.0f).Append(_moveWithWasd.DOFade(0.0f, 0.5f).OnComplete(() => _moveHintCompleted = true));

        yield return new WaitUntil(() => _moveHintCompleted);
        yield return new WaitForSeconds(1.0f);
        
        _phoneManager.StartNewConversation();
        _phoneCoroutine = StartCoroutine(PhoneRinging(5.0f));
        _phoneInteractable.ResetInteractionFlags();

        yield return new WaitForSeconds(1.0f);
        _interactWithE.DOFade(1.0f, 0.5f);
        //DOTween.Sequence().Append(_interactWithE.DOFade(1.0f, 0.5f)).AppendInterval(5.0f).Append(_interactWithE.DOFade(0.0f, 0.5f).OnComplete(() => _interactHintCompleted = true));

        yield return new WaitUntil(() => _phoneInteractable.HasBeenInteracted);
        StopCoroutine(_phoneCoroutine);
        _interactWithE.DOFade(0.0f, 0.5f);
        _phoneManager.StartMessagingWithTimer();

        yield return new WaitUntil(() => _phoneInteractable.InteractionFinished);
        //PlaySuspiro();
        //_phoneInteractable.ResetInteractionFlags();
        //_firstMessageCompleted = true;

        //yield return new WaitUntil(() => _boxOpened);
        //_phoneManager.ResetPhone();
        //_phoneManager.StartNewConversation();
        //_phoneCoroutine = StartCoroutine(PhoneRinging(10.0f));

        //yield return new WaitUntil(() => _phoneInteractable.HasBeenInteracted);
        //StopCoroutine(_phoneCoroutine);
        //_phoneManager.StartMessagingWithTimer();

        //yield return new WaitUntil(() => _phoneInteractable.InteractionFinished);
        //_phoneInteractable.ResetInteractionFlags();
        //_secondMessageCompleted = true;

        yield return new WaitUntil(() => _shoesInteractable.HasBeenInteracted);
        yield return new WaitForSeconds(1.0f);
        _gameManager.ChangeScene();

    }

    private IEnumerator PhoneRinging(float interval)
    {
        YieldInstruction waitSomeTime = new WaitForSeconds(interval);
        while (true)
        {
            yield return waitSomeTime;
            _phoneManager.JustPlaySound();
        }
    }

    private void PlaySuspiro()
    {
        _audioSource.clip = _suspiroAudioClip;
        _audioSource.Play();
    }

    public override bool IsObjectInteractable(InteractableBehaviour interactable)
    {
        if (interactable == _boxInteractable)
        {
            if (!_firstMessageCompleted)
                return false;

            if (_keyInteractable.HasBeenInteracted)
            {
                _boxOpened = true;
                return false;
            }

            return true;
        }

        //if (interactable == _shoesInteractable)
        //{
        //    if (!_secondMessageCompleted)
        //        return false;
        //}

        return true;
    }
}
