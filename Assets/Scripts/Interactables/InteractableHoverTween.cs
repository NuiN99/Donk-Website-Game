using NuiN.SpleenTween;
using UnityEngine;

public class InteractableHoverTween : InteractableEventsReciever
{
    [SerializeField] Transform target;
    
    [Header("Scale Tween")]
    [SerializeField] Vector2 targetScale;
    [SerializeField] float scaleOutDuration;
    [SerializeField] Ease scaleOutEase;
    [SerializeField] float scaleInDuration;
    [SerializeField] Ease scaleInEase;
    
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
        _scaleTween?.Stop();
        _scaleTween = SpleenTween.Scale(target, _initialScale, targetScale, scaleInDuration).SetEase(scaleInEase);
    }

    protected override void HoverEnd()
    {
        if (_scaleTween.Active == false)
        {
            _scaleTween?.Stop();
            TweenScaleOut();
        }
        else
        {
            _scaleTween.OnComplete(TweenScaleOut);
        }

        return;

        void TweenScaleOut()
        {
            _scaleTween = SpleenTween.Scale(target, targetScale, _initialScale, scaleOutDuration).SetEase(scaleOutEase);
        }
    }

    protected override void Hold() { }
    protected override void HoldStart() { }
    protected override void HoldEnd() { }
}
