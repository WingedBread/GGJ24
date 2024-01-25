using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform _interactionModeTransform;
    [SerializeField]
    private bool _canBeRotated;
    [SerializeField]
    private Vector2Reference _rotationSens;

    private Outline _outline;
    private Transform _originalParent;
    private Vector3 _originalLocalPosition;
    private Quaternion _originalLocalRotation;
    private bool _interactionModeEnabled;
    private Camera _camera;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _interactionModeEnabled = false;
    }

    // NOTE: For some weird reason outline only works properly when they start enabled in the scene.
    // To work around this, they start enabled but are automatically disabled after one frame in this
    // Start function
    private IEnumerator Start()
    {
        yield return null;
        _outline.enabled = false;
    }

    private void OnMouseDrag()
    {
        if (!_canBeRotated || !_interactionModeEnabled)
            return;

        Vector3 delta = Input.mousePositionDelta;
        transform.RotateAround(_camera.transform.up, delta.x * _rotationSens.value.x);
        transform.RotateAround(_camera.transform.right, delta.y * _rotationSens.value.y);
    }

    public void EnableHighlight()
    {
        _outline.enabled = true;
    }

    public void DisableHighlight()
    {
        _outline.enabled = false;
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
        _camera = camera;
        Cursor.lockState = CursorLockMode.Confined;

        Quaternion interactionModeRotation = _interactionModeTransform != null ? Quaternion.Inverse(_interactionModeTransform.localRotation) : Quaternion.identity;
        transform.SetParent(camera.transform, worldPositionStays: false);
        transform.SetLocalPositionAndRotation(1.5f * Vector3.forward, interactionModeRotation);
    }

    public void UnsetInteractionMode()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.SetParent(_originalParent, worldPositionStays: false);
        transform.SetLocalPositionAndRotation(_originalLocalPosition, _originalLocalRotation);
        _interactionModeEnabled = false;
    }
}
