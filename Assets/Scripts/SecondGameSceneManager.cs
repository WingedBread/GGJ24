using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGameSceneManager : GameSceneManager
{
    [SerializeField] private InteractableBehaviour _hat;
    [SerializeField] private InteractableBehaviour _nose;
    [SerializeField] private InteractableBehaviour _paintings;
    [SerializeField] private InteractableBehaviour _lock;
    [SerializeField] private InteractableBehaviour _frame;
    [SerializeField] private LockBehaviour _lockLock;
    [SerializeField] private ObjectAnimation _boxAnimation;
    [SerializeField] private InteractableBehaviour _phoneInteractable;

    [SerializeField] private PhoneManager _phoneManager;

    private bool _forcedToHoldPhone;
    private bool _firstMessageCompleted;
    private bool _secondMessageCompleted;
    private bool _boxOpened;

    public override void InitializeScene(GameManager gameManager)
    {
        base.InitializeScene(gameManager);
        Debug.Log("Initialize second scene");
        _phoneManager.Initialize();
        _phoneManager.StartAtJohnConversation();
        _forcedToHoldPhone = false;
        _firstMessageCompleted = false;
        _secondMessageCompleted = false;
        _boxOpened = false;
    }

    public override IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1.0f);
        _phoneManager.StartNewConversation();
        _phoneInteractable.ResetInteractionFlags();

        // FIRST MESSAGE
        yield return new WaitUntil(() => _phoneInteractable.HasBeenInteracted);
        _forcedToHoldPhone = true;
        _phoneManager.StartMessagingWithTimer();
        yield return new WaitUntil(() => _phoneManager.ConversationIsFinished());
        _forcedToHoldPhone = false;
        yield return new WaitUntil(() => _phoneInteractable.InteractionFinished);
        _firstMessageCompleted = true;

        // PAINTING INTERACTION
        yield return new WaitUntil(() => _paintings.HasBeenInteracted);
        yield return new WaitForSeconds(2.0f);

        // SECOND MESSAGE
        _phoneInteractable.ResetInteractionFlags();
        _phoneManager.ResetPhone();
        _phoneManager.StartNewConversation();
        yield return new WaitUntil(() => _phoneInteractable.HasBeenInteracted);
        _forcedToHoldPhone = true;
        _phoneManager.StartMessagingWithTimer();
        yield return new WaitUntil(() => _phoneManager.ConversationIsFinished());
        _forcedToHoldPhone = false;
        yield return new WaitUntil(() => _phoneInteractable.InteractionFinished);

        // HAT INTERACTION
        yield return new WaitUntil(() => _hat.HasBeenInteracted);
        // NOSE INTERACTION
        yield return new WaitUntil(() => _nose.HasBeenInteracted);

        yield return new WaitForSeconds(1.0f);
        _gameManager.ChangeScene();
    }

    public override bool IsObjectInteractable(InteractableBehaviour interactable)
    {
        if (interactable == _paintings)
        {
            return _firstMessageCompleted;
        }
        if (interactable == _hat)
        {
            return _secondMessageCompleted;
        }
        if (interactable == _lock)
        {
            return _secondMessageCompleted && !_boxOpened;
        }
        if (interactable == _nose)
        {
            return _secondMessageCompleted && _boxOpened;
        }
        if (interactable == _frame)
        {
            return _secondMessageCompleted;
        }
        return true;
    }

    public override bool OnInteractionMade(InteractableBehaviour interactable)
    {
        if (interactable == _phoneInteractable)
        {
            return !_forcedToHoldPhone;
        }

        if (interactable == _lock)
        {
            if (_lockLock.HasBeenOpened())
            {
                _boxOpened = true;
                _boxAnimation.OpenClose();
            }
            return true;
        }
        
        return true;
    }
}
