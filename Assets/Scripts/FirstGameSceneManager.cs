using System.Collections;
using UnityEngine;

public class FirstGameSceneManager : GameSceneManager
{
    [SerializeField] private PhoneManager _phoneManager;
    [SerializeField] private InteractableBehaviour _phoneInteractable;
    [SerializeField] private InteractableBehaviour _keyInteractable;
    [SerializeField] private InteractableBehaviour _boxInteractable;
    [SerializeField] private InteractableBehaviour _shoesInteractable;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _suspiroAudioClip;

    private Coroutine _phoneCoroutine;
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
    }

    public override IEnumerator StartScene()
    {
        Debug.Log("Start first scene");
        
        yield return new WaitForSeconds(2.0f);
        _phoneManager.StartNewConversation();
        _phoneCoroutine = StartCoroutine(PhoneRinging(5.0f));

        yield return new WaitUntil(() => _phoneInteractable.HasBeenInteracted);
        StopCoroutine(_phoneCoroutine);
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

        if (interactable == _shoesInteractable)
        {
            if (!_secondMessageCompleted)
                return false;
        }

        return true;
    }
}
