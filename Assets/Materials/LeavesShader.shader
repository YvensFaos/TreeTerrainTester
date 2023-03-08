Shader "Custom/LeavesShader"
 {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _WindDirection ("Wind Direction", Vector) = (0,0,0,0)
        _NoiseScale ("Noise Scale", Range(0, 2)) = 1
        _NoiseSpeed ("Noise Speed", Range(0, 2)) = 1
        _NoiseIntensity ("Noise Intensity", Range(0, 2)) = 1
    }
 
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100
 
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord : TEXCOORD0;
            };
 
            struct v2f {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
                float3 worldTangent: TEXCOORD2;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _WindDirection;
            float _NoiseScale;
            float _NoiseSpeed;
            float _NoiseIntensity;
 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                float3 worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                float3 worldTangent = normalize(mul((float3x3)UNITY_MATRIX_MV, float3(1.0, 0.0, 0.0)));
                float3 windDirection = normalize(_WindDirection.xyz);
                float2 noise = sin(_Time.y * _NoiseSpeed) * _NoiseScale;
                float windFactor = _NoiseIntensity * noise;
                o.vertex.xyz += windDirection * windFactor;
                o.worldNormal = worldNormal;
                o.worldTangent = worldTangent;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target {
                fixed4 tex = tex2D(_MainTex, i.texcoord);
                return tex * _Color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}