using NuiN.NExtensions;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OutlineShaderController : MonoBehaviour
{
    static readonly int MainTex = Shader.PropertyToID("_MainTex");
    static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
    static readonly int Color1 = Shader.PropertyToID("_Color");
    static readonly int OutlineWidth = Shader.PropertyToID("_OutlineWidth");
    
    [SerializeField, InjectComponent] SpriteRenderer spriteRenderer;

    [Header("Shader Properties")]
    public Texture mainTexture;
    public Color outlineColor = Color.white;
    public Color baseColor = Color.white;
    public float outlineWidth = 1.0f;
    
    void Update()
    {
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        if (mainTexture != null)
        {
            spriteRenderer.material.SetTexture(MainTex, mainTexture);
        }

        spriteRenderer.material.SetColor(OutlineColor, outlineColor);
        spriteRenderer.material.SetColor(Color1, baseColor);
        spriteRenderer.material.SetFloat(OutlineWidth, outlineWidth);
    }
}