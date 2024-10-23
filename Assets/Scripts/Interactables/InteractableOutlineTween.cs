using NuiN.SpleenTween;
using UnityEngine;

public class InteractableOutlineTween : InteractableEventsReciever
{
    [SerializeField] OutlineController target;
    
    [Header("Outline Tween")]
    [SerializeField] float targetThickness;
    [SerializeField] Color targetOutlineColor;
    [SerializeField] Color targetBaseColor;
    
    [SerializeField] float inDuration;
    [SerializeField] Ease inEase;
    [SerializeField] float outDuration;
    [SerializeField] Ease outEase;
    
    float _initialThickness;
    Color _initialOutlineColor;
    Color _initialBaseColor;

    ITween _thicknessTween;
    ITween _outlineColorTween;
    ITween _baseColorTween;

    void Start()
    {
        _initialThickness = target.Thickness;
        _initialOutlineColor = target.OutlineColor;
        _initialBaseColor = target.BaseColor;
    }

    protected override void Hover() { }

    protected override void HoverStart()
    {
        _thicknessTween?.Stop();
        _thicknessTween = SpleenTween.Value(_initialThickness, targetThickness, inDuration, val => target.SetThickness(val)).SetEase(inEase);

        _outlineColorTween?.Stop();
        _outlineColorTween = SpleenTween.Value0To1(inDuration, val => target.SetOutlineColor(Color.LerpUnclamped(_initialOutlineColor, targetOutlineColor, val))).SetEase(inEase);
        
        _baseColorTween?.Stop();
        _baseColorTween = SpleenTween.Value0To1(inDuration, val => target.SetBaseColor(Color.LerpUnclamped(_initialBaseColor, targetBaseColor, val))).SetEase(inEase);
    }

    protected override void HoverEnd()
    {
        if (_thicknessTween.Active == false)
        {
            _thicknessTween?.Stop();
            ScaleOutTween();
            
        }
        else
        {
            _thicknessTween.OnComplete(ScaleOutTween);
        }

        if (_outlineColorTween.Active == false)
        {
            _outlineColorTween?.Stop();
            OutlineColorOutTween();
        }
        else
        {
            _outlineColorTween.OnComplete(OutlineColorOutTween);
        }
        
        if (_baseColorTween.Active == false)
        {
            _baseColorTween?.Stop();
            BaseColorOutTween();
        }
        else
        {
            _baseColorTween.OnComplete(BaseColorOutTween);
        }

        return;

        void ScaleOutTween()
        {
            _thicknessTween = SpleenTween.Value(targetThickness, _initialThickness, outDuration, val => target.SetThickness(val)).SetEase(outEase);
        }
        
        void OutlineColorOutTween()
        {
            _outlineColorTween = SpleenTween.Value0To1(outDuration, val => target.SetOutlineColor(Color.LerpUnclamped(targetOutlineColor, _initialOutlineColor, val))).SetEase(outEase);
        }
        
        void BaseColorOutTween()
        {
            _baseColorTween = SpleenTween.Value0To1(outDuration, val => target.SetBaseColor(Color.LerpUnclamped(targetBaseColor, _initialBaseColor, val))).SetEase(outEase);
        }
    }

    protected override void Hold() { }
    protected override void HoldStart() { }
    protected override void HoldEnd() { }
}