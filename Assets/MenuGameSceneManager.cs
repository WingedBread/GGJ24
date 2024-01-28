using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameSceneManager : GameSceneManager
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _gameManager.ChangeScene();
        }
    }
}
