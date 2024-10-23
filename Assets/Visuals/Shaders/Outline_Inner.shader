Shader "Unlit/InnerSpriteOutline HLSL"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _Color ("Color", Color) = (1,1,1,1)
        _OutlineWidth ("Outline Width", Float) = 1.0
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;

            fixed4 _OutlineColor;
            fixed4 _Color;
            float _OutlineWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;

                if (_OutlineWidth <= 0)
                {
                    return col;
                }

                float2 innerOffset = _MainTex_TexelSize.xy * _OutlineWidth;
                float2 outerOffset = _MainTex_TexelSize.xy * 1.0;

                fixed leftPixelInner = tex2D(_MainTex, i.uv + float2(-innerOffset.x, 0)).a;
                fixed upPixelInner = tex2D(_MainTex, i.uv + float2(0, innerOffset.y)).a;
                fixed rightPixelInner = tex2D(_MainTex, i.uv + float2(innerOffset.x, 0)).a;
                fixed bottomPixelInner = tex2D(_MainTex, i.uv + float2(0, -innerOffset.y)).a;

                fixed leftPixelOuter = tex2D(_MainTex, i.uv + float2(-outerOffset.x, 0)).a;
                fixed upPixelOuter = tex2D(_MainTex, i.uv + float2(0, outerOffset.y)).a;
                fixed rightPixelOuter = tex2D(_MainTex, i.uv + float2(outerOffset.x, 0)).a;
                fixed bottomPixelOuter = tex2D(_MainTex, i.uv + float2(0, -outerOffset.y)).a;

                fixed innerOutline = (1 - leftPixelInner * upPixelInner * rightPixelInner * bottomPixelInner) * col.a;
                fixed outerOutline = (1 - leftPixelOuter * upPixelOuter * rightPixelOuter * bottomPixelOuter) * (1 - col.a);

                if (outerOutline > 0.0)
                {
                    return _OutlineColor;
                }

                if (innerOutline > 0.0)
                {
                    return _OutlineColor;
                }

                if (col.a > 0.0)
                {
                    return _Color;
                }

                return fixed4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
}
