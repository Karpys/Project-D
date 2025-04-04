Shader "Custom/TormentLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
         _PulseColor ("PulseColor", Color) = (0,0,0,0)
        _PulseSpeed ("Pulse Speed", float) = 0
        _Fade ("Fade", float) = 0
        _TransparencyPulse ("TransparancyPulse", Range (0, 1)) = 1
        [MaterialToggle] _Loop ("Loop", Int) = 0
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

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
            half4 _PulseColor;
            half4 _Color;
            float _PulseRange;
            float _PulseSpeed;
            float _Loop;
            float _MultLoop;
            float _Scale;
            float _Fade;
            float _TransparencyPulse;
            
            float _StartTime;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex,i.uv);
                float adjustedTime = _Time.y - _StartTime;

                float _pulseSpace;
                
                if(_Loop == 1)
                {
                    _pulseSpace = 1 - frac(adjustedTime * _PulseSpeed)/2;
                }
                else
                {
                    _pulseSpace = 1 - adjustedTime * _PulseSpeed;
                }
                
                if(i.uv.y < 1 - _pulseSpace)
                {
                    return color * _Color * _Fade;
                }
                
                if(i.uv.y > _pulseSpace)
                {
                    return color * _Color * _Fade;
                }
                
                return _TransparencyPulse * _PulseColor * _Fade;
            }
            ENDCG
        }
    }
}