using UnityEngine;

public class DebugInteractable : MonoBehaviour, IInteractable
{
    public void Hover() => Debug.Log($"{name}: Hovered", this);
    public void Select() => Debug.Log($"{name}: Selected", this);
    public void Hold() => Debug.Log($"{name}: Holding", this);
    public void Release() => Debug.Log($"{name}: Released", this);
}
