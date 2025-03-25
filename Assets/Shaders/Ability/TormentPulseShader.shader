Shader "Unlit/TormentPulseShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (0,0,0,0)
        _PulseColor ("PulseColor", Color) = (0,0,0,0)
        _PulseRange ("PulseRange", float) = 0
        _PulseSpeed ("Pulse Speed", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _PulseColor;
            float _PulseRange;
            float _PulseSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float pulseOffset = frac(_Time.y * _PulseSpeed);
                float minX = frac(0 + pulseOffset);
                float maxX = frac(_PulseRange + pulseOffset);

                if(minX > maxX)
                {
                    if(i.uv.x > minX || i.uv.x < maxX)
                    {
                        return _PulseColor;
                    }
                }
                
                if(i.uv.x > minX && i.uv.x < maxX)
                {
                    return _PulseColor;
                }
                
                return _Color;
            }
            ENDCG
        }
    }
}
