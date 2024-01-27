using System.Collections;
using UnityEngine;

public class FirstGameSceneManager : GameSceneManager
{
    public override void InitializeScene()
    {
        Debug.Log("Initialize first scene");
    }

    public override IEnumerator StartScene(GameManager gameManager)
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Initialize second scene");
    }
}
