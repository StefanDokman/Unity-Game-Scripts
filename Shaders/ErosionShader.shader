Shader "Custom/ErosionShader"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor("Src Factor", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor("Dst Factor", Float) = 10
        [Enum(UnityEngine.Rendering.BlendOp)]
        _Opp("Operation", Float) = 0

        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex ("Texture", 2D) = "white" {}
        _RevealValue ("Reveal", float) = 0
        _FeatherlValue ("Feather", float) = 0
        _BiasDirection ("Erosion Bias Direction", Vector) = (0, 1, 0, 0)


        _ErodeColor ("Erode Color", Color) = (1,1,1,1) 
        _ErodeColor2 ("Erode Color", Color) = (1,1,1,1) 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Blend [_SrcFactor] [_DstFactor]
        BlendOp [_Opp]

        Pass
        {
            ZWrite Off
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
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _MaskTex;
            float4 _MaskTex_ST;
            float _RevealValue;
            float _FeatherlValue;
            float4 _ErodeColor;
            float4 _ErodeColor2;
            float4 _BiasDirection;



            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.zw = TRANSFORM_TEX(v.uv, _MaskTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv.xy);
                fixed4 mask = tex2D(_MaskTex, i.uv.zw);

                float2 biasUV = i.uv.xy;
                float biasOffset = dot(biasUV, _BiasDirection.xy);
                float biasedMask = mask.r + biasOffset;

                float revealAmmountTop = step(biasedMask,_RevealValue + _FeatherlValue);
                float revealAmmountBottom = step(biasedMask,_RevealValue - _FeatherlValue);
                float revealDifference = revealAmmountTop - revealAmmountBottom;

                float3 finalCol = lerp(col.rgb, lerp(_ErodeColor,_ErodeColor2,(_RevealValue + _FeatherlValue - biasedMask) / (2.0 * _FeatherlValue)), revealDifference);
                return fixed4(finalCol.rgb,col.a * revealAmmountTop);
            }
            ENDCG
        }
    }
}
