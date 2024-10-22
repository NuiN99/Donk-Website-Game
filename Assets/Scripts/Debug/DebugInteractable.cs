using UnityEngine;

public class DebugInteractable : InteractableEventsReciever
{
    protected override void Hover() => Debug.Log($"{name}: Hovering", this);
    protected override void HoverStart() => Debug.Log($"{name}: Hover Start", this);
    protected override void HoverEnd() => Debug.Log($"{name}: Hover End", this);

    protected override void Hold() => Debug.Log($"{name}: Holding", this);
    protected override void HoldStart() => Debug.Log($"{name}: Selected", this);
    protected override void HoldEnd() => Debug.Log($"{name}: Released", this);
}
