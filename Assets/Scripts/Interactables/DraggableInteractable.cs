using UnityEngine;

public class DraggableInteractable : InteractableEventsReciever
{
    [SerializeField] Rigidbody2D rb;

    protected override void Hover() { }
    protected override void Hold() { }
    protected override void Select() => MouseJoint.Instance.Attach(rb);
    protected override void Release() => MouseJoint.Instance.Detach();
}