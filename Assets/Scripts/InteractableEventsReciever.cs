using NuiN.NExtensions;
using UnityEngine;

public abstract class InteractableEventsReciever : MonoBehaviour
{
    [SerializeField, InjectComponent] InteractableEvents interactableEvents;
    
    void OnEnable()
    {
        interactableEvents.OnHover += Hover;
        interactableEvents.OnHoverStart += HoverStart;
        interactableEvents.OnHoverEnd += HoverEnd;
        interactableEvents.OnHold += Hold;
        interactableEvents.OnHoldStart += HoldStart;
        interactableEvents.OnHoldEnd += HoldEnd;
    }
    void OnDisable()
    {
        interactableEvents.OnHover -= Hover;
        interactableEvents.OnHoverStart -= HoverStart;
        interactableEvents.OnHoverEnd -= HoverEnd;
        interactableEvents.OnHold -= Hold;
        interactableEvents.OnHoldStart -= HoldStart;
        interactableEvents.OnHoldEnd -= HoldEnd;
    }
    
    protected abstract void Hover();
    protected abstract void HoverStart();
    protected abstract void HoverEnd();
    protected abstract void Hold();
    protected abstract void HoldStart();
    protected abstract void HoldEnd();
}