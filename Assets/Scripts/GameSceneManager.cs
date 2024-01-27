using System.Collections;
using UnityEngine;

public abstract class GameSceneManager : MonoBehaviour
{
    public abstract void InitializeScene();
    public abstract IEnumerator StartScene(GameManager gameManager);
}
