using NuiN.NExtensions;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OutlineController : MonoBehaviour
{
    public float Thickness => thickness;
    public Color OutlineColor => outlineColor;
    public Color BaseColor => baseColor;
    
    static readonly int OutlineColorId = Shader.PropertyToID("_OutlineColor");
    static readonly int BaseColorId = Shader.PropertyToID("_Color");
    static readonly int ThicknessId = Shader.PropertyToID("_OutlineWidth");
    
    [SerializeField, InjectComponent] SpriteRenderer spriteRenderer;

    [Header("Properties")]
    [SerializeField] Color outlineColor = Color.white;
    [SerializeField] Color baseColor = Color.white;
    [SerializeField] float thickness = 1.0f;

    void Awake()
    {
        UpdateMaterial();
    }

    void Update()
    {
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        spriteRenderer.material.SetColor(OutlineColorId, outlineColor);
        spriteRenderer.material.SetColor(BaseColorId, baseColor);
        spriteRenderer.material.SetFloat(ThicknessId, thickness);
    }

    public void SetThickness(float thickness)
    {
        this.thickness = thickness;
    }

    public void SetOutlineColor(Color color)
    {
        this.outlineColor = color;
    }
    
    public void SetBaseColor(Color color)
    {
        this.baseColor = color;
    }
}