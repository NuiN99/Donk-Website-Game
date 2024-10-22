using UnityEngine;

public class DraggableInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] Rigidbody2D rb;
    
    public void Hover() { }
    public void Hold() { }
    public void Select() => MouseJoint.Instance.Attach(rb);
    public void Release() => MouseJoint.Instance.Detach();
}