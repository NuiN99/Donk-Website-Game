using NuiN.NExtensions;
using NuiN.SpleenTween;
using UnityEngine;

public class InteractableHoverScaleTween : InteractableEventsReciever
{
    [SerializeField] Transform target;
    
    [Header("Hover In Out Scale Tween")]
    [SerializeField] float targetHoverScaleMult;
    [SerializeField] float hoverScaleInDuration = 0.1f;
    [SerializeField] Ease hoverScaleInEase = Ease.InSine;
    [SerializeField] float hoverScaleOutDuration = 0.5f;
    [SerializeField] Ease hoverScaleOutEase = Ease.OutSine;
    
    ITween _hoverScaleTween;
    
    [SerializeField, HideInInspector] Vector2 initialScale;

    void OnValidate()
    {
        if(!Application.isPlaying) initialScale = transform.localScale;
    }
    
    protected override void Hover() { }

    protected override void HoverStart()
    {
        _hoverScaleTween?.Stop();
        _hoverScaleTween = SpleenTween.Scale(target, initialScale * targetHoverScaleMult, hoverScaleInDuration).SetEase(hoverScaleInEase);
    }

    protected override void HoverEnd()
    {
        if (_hoverScaleTween.Active == false)
        {
            _hoverScaleTween?.Stop();
            TweenScaleOut();
        }
        else
        {
            _hoverScaleTween.OnComplete(TweenScaleOut);
        }

        return;

        void TweenScaleOut()
        {
            _hoverScaleTween = SpleenTween.Scale(target, initialScale, hoverScaleOutDuration).SetEase(hoverScaleOutEase);
        }
    }

    protected override void Hold() { }
    protected override void HoldStart() { }
    protected override void HoldEnd() { }
}