using NuiN.SpleenTween;
using UnityEngine;

public class InteractableHoverTween : InteractableEventsReciever
{
    [SerializeField] Transform target;
    [SerializeField] Vector2 targetScale;
    [SerializeField] float scaleDuration;
    [SerializeField] AnimationCurve scaleEase;

    ITween _scaleTween;
    Vector2 _initialScale;
    
    void Start()
    {
        _initialScale = transform.localScale;
    }
    
    protected override void Hover()
    {
        
    }

    protected override void HoverStart()
    {
        if (_scaleTween is { Active: true })
        {
            return;
        }

        _scaleTween?.Stop();
        _scaleTween = SpleenTween.Scale(target, _initialScale, targetScale, scaleDuration).SetEase(scaleEase);
    }

    protected override void HoverEnd()
    {
        
    }

    protected override void Hold() { }
    protected override void HoldStart() { }
    protected override void HoldEnd() { }
}
