using UnityEngine;

public class DebugInteractable : InteractableEventsReciever
{
    protected override void Hover() => Debug.Log($"{name}: Hovered", this);
    protected override void Select() => Debug.Log($"{name}: Selected", this);
    protected override void Hold() => Debug.Log($"{name}: Holding", this);
    protected override void Release() => Debug.Log($"{name}: Released", this);
}
