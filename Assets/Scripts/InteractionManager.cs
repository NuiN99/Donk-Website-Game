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
        if(_curInteractable != null) return;
        
        InteractableEvents hoveredInteractable = GetRaycastedInteractable();

        if (hoveredInteractable == null)
        {
            if (_lastHoveredInteractable != null)
            {
                _lastHoveredInteractable.HoverEnd();
                _lastHoveredInteractable = null;
            }
        }
        else 
        {
            if (_lastHoveredInteractable == null)
            {
                _lastHoveredInteractable = hoveredInteractable;
                _lastHoveredInteractable.HoverStart();
            }
            else if (_lastHoveredInteractable != hoveredInteractable)
            {
                if (_lastHoveredInteractable != null)
                {
                    _lastHoveredInteractable.HoverEnd();
                }
                
                _lastHoveredInteractable = hoveredInteractable;
                _lastHoveredInteractable.HoverStart();
            }
            
            hoveredInteractable.Hover();
        }
    }

    void TrySelect()
    {
        InteractableEvents selectedInteractable = GetRaycastedInteractable();
        if(selectedInteractable == null)
        {
            return;
        }

        _curInteractable = selectedInteractable;
        _curInteractable.HoldStart();
    }

    void TryRelease()
    {
        if (_curInteractable == null)
        {
            return;
        }

        _lastReleasedInteractable = _curInteractable;
        
        _curInteractable.HoldEnd();
        _curInteractable = null;
    }
}