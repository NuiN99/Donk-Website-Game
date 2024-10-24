using NuiN.SpleenTween;
using UnityEngine;

public class InteractableHoldScaleTween : InteractableEventsReciever
{
    [SerializeField] Transform target;
    
    [Header("Hold In Out Scale Tween")]
    [SerializeField] float targetHoldScaleMult = 0.9f;
    [SerializeField] float holdScaleInDuration = 0.1f;
    [SerializeField] Ease holdScaleInEase = Ease.InSine;
    [SerializeField] float holdScaleOutDuration = 0.1f;
    [SerializeField] Ease holdScaleOutEase = Ease.OutSine;
    
    ITween _holdScaleTween;
    [SerializeField, HideInInspector] Vector2 initialScale;

    void OnValidate()
    {
        if(!Application.isPlaying) initialScale = transform.localScale;
    }

    protected override void Hover() { }
    protected override void HoverStart() { }
    protected override void HoverEnd() { }
    protected override void Hold() { }

    protected override void HoldStart()
    {
        _holdScaleTween?.Stop();
        _holdScaleTween = SpleenTween.Scale(target, initialScale * targetHoldScaleMult, holdScaleInDuration).SetEase(holdScaleInEase);
    }

    protected override void HoldEnd()
    {
        if (_holdScaleTween.Active == false)
        {
            _holdScaleTween?.Stop();
            TweenScaleOut();
        }
        else
        {
            _holdScaleTween.OnComplete(TweenScaleOut);
        }

        return;

        void TweenScaleOut()
        {
            _holdScaleTween = SpleenTween.Scale(target, initialScale, holdScaleOutDuration).SetEase(holdScaleOutEase);
        }
    }
}