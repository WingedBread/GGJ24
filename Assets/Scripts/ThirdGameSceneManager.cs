using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThirdGameSceneManager : GameSceneManager
{
    [SerializeField] private InteractableBehaviour _nose;
    [SerializeField] private InteractableBehaviour _lock;
    [SerializeField] private ObjectAnimation _boxAnimation;
    [SerializeField] private LockBehaviour _lockLock;

    private bool _boxOpened;

    public override void InitializeScene(GameManager gameManager)
    {
        base.InitializeScene(gameManager);
        Debug.Log("Initialize second scene");
        _boxOpened = false;
    }

    public override IEnumerator StartScene()
    {
        yield return new WaitUntil(() => _nose.HasBeenInteracted);
        yield return new WaitForSeconds(1.0f);
        _gameManager.ChangeScene();
    }


    public override bool IsObjectInteractable(InteractableBehaviour interactable)
    {
        if (interactable == _lock)
        {
            return !_boxOpened;
        }
        if (interactable == _nose)
        {
            return _boxOpened;
        }
        return true;
    }

    public override bool OnInteractionMade(InteractableBehaviour interactable)
    {
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
