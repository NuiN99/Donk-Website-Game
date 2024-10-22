using System;
using UnityEngine;

public class InteractableEvents : MonoBehaviour
{
    public event Action OnHover = delegate { };
    public event Action OnHoverStart = delegate { };
    public event Action OnHoverEnd = delegate { };

    public event Action OnHold = delegate { };
    public event Action OnHoldStart = delegate { }; 
    public event Action OnHoldEnd = delegate { };

    public void Hover() => OnHover.Invoke();
    public void HoverStart() => OnHoverStart.Invoke();
    public void HoverEnd() => OnHoverEnd.Invoke();

    public void Hold() => OnHold.Invoke();
    public void HoldStart() => OnHoldStart.Invoke();
    public void HoldEnd() => OnHoldEnd.Invoke();
}