using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractableBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform _interactionModeTransform;
    [SerializeField]
    private bool _canBeRotated;
    [SerializeField]
    private Vector2Reference _rotationSens;
    [SerializeField]
    private Outline _modelOutline;

    private Transform _originalParent;
    private Vector3 _originalLocalPosition;
    private Quaternion _originalLocalRotation;
    private Vector3 _originalLocalScale;
    private bool _interactionModeEnabled;
    private Camera _camera;
    private bool _dragging;

    private void Awake()
    {
        _dragging = false;
        _interactionModeEnabled = false;
    }

    // NOTE: For some weird reason outline only works properly when they start enabled in the scene.
    // To work around this, they start enabled but are automatically disabled after one frame in this
    // Start function
    private IEnumerator Start()
    {
        yield return null;
        _modelOutline.enabled = false;
    }

    private void Update()
    {
        if (!_interactionModeEnabled || !_canBeRotated)
            return;

        if (Input.GetMouseButtonDown(0))
            BeginDrag();

        if (Input.GetMouseButtonUp(0))
            EndDrag();

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
        transform.RotateAround(_camera.transform.right, delta.y * _rotationSens.value.y);
    }

    private void EndDrag()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        _dragging = false;
    }

    public void EnableHighlight()
    {
        _modelOutline.enabled = true;
    }

    public void DisableHighlight()
    {
        _modelOutline.enabled = false;
    }

    public void Interact()
    {
        Debug.Log("Interacting");
    }

    public void SetInteractionMode(Camera camera)
    {
        _interactionModeEnabled = true;
        _originalParent = transform.parent;
        _originalLocalPosition = transform.localPosition;
        _originalLocalRotation = transform.localRotation;
        _originalLocalScale = transform.localScale;
        _camera = camera;
        Cursor.lockState = CursorLockMode.Confined;

        transform.DOScale(0.75f * _originalLocalScale, 0.15f).From();

        Quaternion interactionModeRotation = _interactionModeTransform != null ? Quaternion.Inverse(_interactionModeTransform.localRotation) : Quaternion.identity;
        Vector3 interactionModePositionOffset = _interactionModeTransform != null ? new Vector3(_interactionModeTransform.localPosition.x, _interactionModeTransform.localPosition.y, 0.0f) : Vector3.zero;
        transform.SetParent(camera.transform, worldPositionStays: false);
        transform.SetLocalPositionAndRotation(1.5f * Vector3.forward - interactionModePositionOffset, interactionModeRotation);
    }

    public void UnsetInteractionMode()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.SetParent(_originalParent, worldPositionStays: false);
        transform.SetLocalPositionAndRotation(_originalLocalPosition, _originalLocalRotation);
        transform.localScale = _originalLocalScale;
        _interactionModeEnabled = false;
    }
}
