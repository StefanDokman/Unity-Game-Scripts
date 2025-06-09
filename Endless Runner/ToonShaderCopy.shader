Shader "Custom/FlatToonWithOutline_Displace"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0, 0.1)) = 0.02
        _OutlineDepthOffset ("Outline Depth Offset", Range(0, 1)) = 0.0
        _WashAmount ("Base Wash Amount", Range(0, 1)) = 0.6
        _WashDistanceScale ("Distance Fade Factor", Range(0, 1)) = 0.2
        _DispHorizontal ("Displace XZ Amount", Range(-1, 1)) = 0.1
        _DispVertical ("Displace Y Amount", Range(-1, 1)) = 0.1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        // === OUTLINE PASS ===
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode"="Always" }
            Cull Front
            ZWrite On
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vertOutline
            #pragma fragment fragOutline
            #include "UnityCG.cginc"

            float _OutlineThickness;
            float _OutlineDepthOffset;
            float4 _OutlineColor;

            float _DispVertical;
            float _DispHorizontal;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vertOutline(appdata v)
            {
                v2f o;

                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

                float dist = distance(worldPos.xyz, float3(0, 0, 0));
                float displaceFactor = dist * dist;

                float3 toCam = normalize(_WorldSpaceCameraPos - worldPos.xyz);
                float3 camRightWS = normalize(cross(float3(0, 1, 0), toCam));
                float3 camUpWS = normalize(cross(toCam, camRightWS));

                worldPos.xyz += camUpWS * _DispVertical * displaceFactor;
                worldPos.xyz += camRightWS * _DispHorizontal * displaceFactor;

                float3 offset = worldNormal * _OutlineThickness;
                worldPos.xyz += offset;

                float4 pos = UnityWorldToClipPos(worldPos);
                pos.z += _OutlineDepthOffset;

                o.pos = pos;
                return o;
            }

            fixed4 fragOutline(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

        Pass
        {
            Name "FORWARD"
            Tags { "LightMode"="Always" }
            Cull Back
            ZWrite On
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _WashAmount;
            float _WashDistanceScale;
            float4 _LightColor0;

            float _DispHorizontal;
            float _DispVertical;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 viewPos : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;

                float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
                float3 viewSpacePos = mul(UNITY_MATRIX_V, worldPos).xyz;

                float dist = distance(worldPos.xyz, float3(0, 0, 0));
                float displaceFactor = dist * dist;

                float3 toCam = normalize(_WorldSpaceCameraPos - worldPos.xyz);
                float3 camRightWS = normalize(cross(float3(0, 1, 0), toCam));
                float3 camUpWS = normalize(cross(toCam, camRightWS));

                worldPos.xyz += camUpWS * _DispVertical * displaceFactor;
                worldPos.xyz += camRightWS * _DispHorizontal * displaceFactor;

                o.pos = UnityWorldToClipPos(worldPos);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.viewPos = viewSpacePos;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texcol = tex2D(_MainTex, i.uv) * _Color;

                float dist = abs(i.viewPos.z);
                float distFactor = saturate(dist * _WashDistanceScale);
                float totalWash = saturate(_WashAmount + distFactor);

                float gray = dot(texcol.rgb, float3(0.3, 0.59, 0.11));
                texcol.rgb = lerp(texcol.rgb, float3(gray, gray, gray), 0.3);
                texcol.rgb = lerp(texcol.rgb, float3(1, 1, 1), totalWash);

                texcol.rgb *= _LightColor0.rgb;
                return texcol;
            }
            ENDCG
        }
    }
}
