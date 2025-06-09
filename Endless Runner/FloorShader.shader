Shader "Custom/SurfaceDisplaceWash"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _WashAmount ("Base Wash Amount", Range(0, 1)) = 0.3
        _WashDistanceScale ("Distance Fade Factor", Range(0, 0.1)) = 0.02
        _DispHorizontal ("Displace XZ Amount", Range(-1, 1)) = 0.1
        _DispVertical ("Displace Y Amount", Range(-1, 1)) = 0.1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert

        sampler2D _MainTex;
        float4 _Color;
        float _WashAmount;
        float _WashDistanceScale;
        float _DispHorizontal;
        float _DispVertical;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewPos;
        };

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);

            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
            float3 toOrigin = worldPos.xyz - float3(0,0,0);
            float dist = length(toOrigin);
            float displaceFactor = dist * dist;

            float3 camUpWS = float3(0,1,0);
            float3 camRightWS = normalize(cross(camUpWS, toOrigin)); 

            worldPos.xyz += camUpWS * _DispVertical * displaceFactor;
            worldPos.xyz += camRightWS * _DispHorizontal * displaceFactor;

            o.viewPos = mul(UNITY_MATRIX_V, worldPos).xyz;

            v.vertex = mul(unity_WorldToObject, worldPos);
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            float dist = abs(IN.viewPos.z);
            float washFactor = saturate(_WashAmount + dist * _WashDistanceScale);

            float gray = dot(tex.rgb, float3(0.3, 0.59, 0.11));
            tex.rgb = lerp(tex.rgb, float3(gray, gray, gray), 0.3);
            tex.rgb = lerp(tex.rgb, float3(1,1,1), washFactor);

            o.Albedo = tex.rgb;
            o.Alpha = tex.a;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
