using System;
using UnityEngine;

public class InteractableEvents : MonoBehaviour
{
    public event Action OnHover = delegate { };
    public event Action OnHold = delegate { };
    public event Action OnSelect = delegate { }; 
    public event Action OnRelease = delegate { };

    public void Hover() => OnHover.Invoke();
    public void Hold() => OnHold.Invoke();
    public void Select() => OnSelect.Invoke();
    public void Release() => OnRelease.Invoke();
}