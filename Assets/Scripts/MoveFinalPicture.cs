using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveFinalPicture : MonoBehaviour
{

    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Vector2Reference _rotationSens;
    private bool _dragging;

    [SerializeField]
    GameObject pictureScene;
    [SerializeField]
    GameObject creditsScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDrag();

        if (Input.GetMouseButtonUp(0))
            EndDrag();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pictureScene.activeSelf)
            {
                pictureScene.SetActive(false);
                creditsScene.SetActive(true);
            }
            if (creditsScene.activeSelf)
            {
                //TODO: GUILLEM CAMBIAR ESTO
                SceneManager.LoadScene(0);
            }
        }

        Drag();
    }

    private void BeginDrag()
    {
        _dragging = true;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Drag()
    {
        if (!_dragging)
            return;

        // TODO: Transform around center of object => arbitrary transform
        Vector3 delta = Input.mousePositionDelta;
        transform.RotateAround(_camera.transform.up, delta.x * _rotationSens.value.x);
        //transform.RotateAround(_camera.transform.right, delta.y * _rotationSens.value.y);
    }

    private void EndDrag()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        _dragging = false;
    }
}
