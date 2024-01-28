using System.Collections;
using UnityEngine;

public abstract class GameSceneManager : MonoBehaviour
{
    protected GameManager _gameManager;

    public virtual void InitializeScene(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public virtual IEnumerator StartScene()
    {
        yield break;
    }

    public virtual bool IsObjectInteractable(InteractableBehaviour interactableBehaviour)
    {
        return true;
    }

    public virtual bool OnInteractionMade(InteractableBehaviour interactableBehaviour)
    {
        return true;
    }
}
