using NuiN.NExtensions;
using UnityEngine;

public abstract class InteractableEventsReciever : MonoBehaviour
{
    [SerializeField, InjectComponent] InteractableEvents interactableEvents;
    
    void OnEnable()
    {
        interactableEvents.OnHover += Hover;
        interactableEvents.OnHold += Hold;
        interactableEvents.OnSelect += Select;
        interactableEvents.OnRelease += Release;
    }
    void OnDisable()
    {
        interactableEvents.OnHover -= Hover;
        interactableEvents.OnHold -= Hold;
        interactableEvents.OnSelect -= Select;
        interactableEvents.OnRelease -= Release;
    }
    
    protected abstract void Hover();
    protected abstract void Hold();
    protected abstract void Select();
    protected abstract void Release();
}