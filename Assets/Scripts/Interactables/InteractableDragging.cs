using UnityEngine;

public class InteractableDragging : InteractableEventsReciever
{
    [SerializeField] Rigidbody2D rb;

    protected override void Hover() { }
    protected override void HoverStart() { }
    protected override void HoverEnd() { }
    protected override void Hold() { }
    protected override void HoldStart() => MouseJoint.Instance.Attach(rb);
    protected override void HoldEnd() => MouseJoint.Instance.Detach();
}