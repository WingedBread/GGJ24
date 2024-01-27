using System.Collections;
using UnityEngine;

public class FirstGameSceneManager : GameSceneManager
{
    [SerializeField] private PhoneManager _phoneManager;

    public override void InitializeScene()
    {
        Debug.Log("Initialize first scene");
    }

    public override IEnumerator StartScene(GameManager gameManager)
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Start first scene");
    }
}
