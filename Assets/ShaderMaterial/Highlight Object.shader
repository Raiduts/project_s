Shader "UI/MultiHoleHighlight"
{
    Properties
    {
        _Color ("Tint", Color) = (0,0,0,0.7)
        _Radius ("Radius", Float) = 0.1
        _Softness ("Softness", Float) = 0.05
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Color;
            float _Radius;
            float _Softness;

            int _TargetCount;
            float4 _Targets[10]; // max 10 highlight

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float circle(float2 uv, float2 center, float radius, float softness)
            {
                float dist = distance(uv, center);
                return smoothstep(radius, radius - softness, dist);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float alpha = _Color.a;

                for (int j = 0; j < _TargetCount; j++)
                {
                    float2 center = _Targets[j].xy;
                    float mask = circle(i.uv, center, _Targets[j].z, _Softness);
                    alpha *= mask;
                }

                return float4(_Color.rgb, alpha);
            }
            ENDCG
        }
    }
}