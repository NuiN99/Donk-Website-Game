using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] float rayRadius;
    
    InteractableEvents _curInteractable;
    InteractableEvents _lastHoveredInteractable;
    InteractableEvents _lastReleasedInteractable;
    Controls _controls;
    
    void OnEnable()
    {
        _controls = new Controls();
        _controls.Enable();
        
        AssignCallbacks(_controls);
    }
    
    void OnDisable()
    {
        _controls.Disable();
        _controls = null;
    }
    
    void AssignCallbacks(Controls controls)
    {
        controls.Input.MouseLeft.performed += TrySelectHandler;
        controls.Input.MouseLeft.canceled += TryReleaseHandler;
    }
    
    void TrySelectHandler(InputAction.CallbackContext ctx) => TrySelect();
    void TryReleaseHandler(InputAction.CallbackContext ctx) => TryRelease();

    void Update()
    {
        if (_curInteractable != null)
        {
            HoldCurrentInteractable();
        }
        else
        {
            TryHover();
        }
    }

    InteractableEvents GetRaycastedInteractable()
    {
        Vector2 mousePos = MainCamera.Instance.MousePosition;
        Collider2D hitCol = Physics2D.OverlapCircle(mousePos, rayRadius);

        if (hitCol == null || !hitCol.TryGetComponent(out InteractableEvents interactable) || interactable == _curInteractable)
        {
            return null;
        }

        return interactable;
    }
    
    void HoldCurrentInteractable()
    {
        _curInteractable.Hold();
    }

    void TryHover()
    {
        InteractableEvents hoveredInteractable = GetRaycastedInteractable();

        if (_lastReleasedInteractable != hoveredInteractable)
        {
            _lastReleasedInteractable = null;
            if (hoveredInteractable != _lastHoveredInteractable && hoveredInteractable != null)
            {
                hoveredInteractable.Hover();
            }
        }
        
        _lastHoveredInteractable = hoveredInteractable;
    }

    void TrySelect()
    {
        InteractableEvents selectedInteractable = GetRaycastedInteractable();
        if(selectedInteractable == null)
        {
            return;
        }

        _curInteractable = selectedInteractable;
        _curInteractable.Select();
    }

    void TryRelease()
    {
        if (_curInteractable == null)
        {
            return;
        }

        _lastReleasedInteractable = _curInteractable;
        
        _curInteractable.Release();
        _curInteractable = null;
    }
}