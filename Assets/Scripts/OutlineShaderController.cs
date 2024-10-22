using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OutlineShaderController : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Material _material;

    [Header("Shader Properties")]
    public Texture mainTexture;
    public Color outlineColor = Color.white;
    public Color baseColor = Color.white;
    public float outlineWidth = 1.0f;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = new Material(Shader.Find("Unlit/InnerSpriteOutline HLSL"));
        _spriteRenderer.material = _material;
    }

    void Update()
    {
        if (mainTexture != null)
        {
            _material.SetTexture("_MainTex", mainTexture);
        }

        _material.SetColor("_OutlineColor", outlineColor);
        _material.SetColor("_Color", baseColor);
        _material.SetFloat("_OutlineWidth", outlineWidth);
    }
}