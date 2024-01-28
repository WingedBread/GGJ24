using UnityEngine;
using KinematicCharacterController.Walkthrough.BasicMovement;

public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private MyPlayer _player;
    [SerializeField]
    private GameObject _interactionModeLabel;
    [SerializeField]
    private LayerMask _interactablesLayerMask;
    [SerializeField, Range(0.0f, 10.0f)]
    private float _interactablesMaxDistance;

    private InteractableBehaviour _hightlightedInteractable;
    private bool _interactionModeEnabled;
    private InteractableBehaviour _interactionModeInteractable;

    private void Awake()
    {
        _hightlightedInteractable = null;
        _interactionModeEnabled = false;
    }

    private void Update()
    {
        if (!_interactionModeEnabled)
            UpdateInteractables();

        else
            UpdateInteractionMode();
    }

    private void UpdateInteractables()
    {
        if (_hightlightedInteractable != null)
            _hightlightedInteractable.DisableHighlight();

        InteractableBehaviour interactable = FindFirstInteractableObjectInSight();
        if (interactable == null)
            return;

        interactable.EnableHighlight();

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (interactable.type == InteractableBehaviour.InteractableType.Zoom)
                EnterInteractionMode(interactable);
            else
                JustInteract(interactable);
        }

        _hightlightedInteractable = interactable;
    }

    private InteractableBehaviour FindFirstInteractableObjectInSight()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _interactablesMaxDistance, _interactablesLayerMask))
        {
            InteractableBehaviour interactable = hit.transform.GetComponent<InteractableBehaviour>();
            if (interactable == null && hit.transform.parent != null) interactable = hit.transform.GetComponent<InteractableBehaviour>();
            return interactable;
        }

        return null;
    }

    private void UpdateInteractionMode()
    {
        if (Input.GetKeyUp(KeyCode.E))
            ExitInteractionMode();
    }

    private void JustInteract(InteractableBehaviour interactable)
    {
        interactable.SetInteractionMode(_camera);
    }

    private void EnterInteractionMode(InteractableBehaviour interactable)
    {
        _interactionModeEnabled = true;
        _interactionModeLabel.SetActive(true);
        _player.DisableInput();
        _interactionModeInteractable = interactable;

        interactable.SetInteractionMode(_camera);
    }

    private void ExitInteractionMode()
    {
        _interactionModeEnabled = false;
        _interactionModeLabel.SetActive(false);
        _player.EnableInput();
        _interactionModeInteractable = null;

        _interactionModeInteractable.UnsetInteractionMode();
    }

}
