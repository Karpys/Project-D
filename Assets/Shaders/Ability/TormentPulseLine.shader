Shader "Unlit/TormentPulseLine"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
         _PulseColor ("PulseColor", Color) = (0,0,0,0)
        _PulseRange ("PulseRange", float) = 0
        _PulseSpeed ("Pulse Speed", float) = 0
        _Scale ("Scale", float) = 1
        _MultLoop ("MultLoop", float) = 1
        _TransparencyPulse ("TransparancyPulse", Range (0, 1)) = 1
        [MaterialToggle] _Loop ("Loop", Int) = 0
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
    }
    
    

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        

        Pass
        {
            CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFragment
            #pragma target 2.0
            #pragma multi_compile_instancing
            #include "UnitySprites.cginc"

            half4 _PulseColor;
            float _PulseRange;
            float _PulseSpeed;
            float _Loop;
            float _MultLoop;
            float _Scale;
            float _TransparencyPulse;
            
            float _StartTime;

            
            fixed4 SpriteFragment(v2f i) : SV_Target
            {
                fixed4 color = SpriteFrag(i);
                float adjustedTime = _Time.y - _StartTime;

                float pulseOffset;
                
                if(_Loop == 1)
                {
                    pulseOffset = frac(adjustedTime * _PulseSpeed * (1.0 / _MultLoop)) * _MultLoop;
                }
                else
                {
                    pulseOffset = adjustedTime * _PulseSpeed;
                }

                //Set scale value to : Distance / width
                //Set MultLoopValue to :
                float2 pulseCenter = float2(pulseOffset * _Scale, 0.5);
                pulseCenter.x -= _PulseRange * 2;
                float2 texCoordRescaled = float2(i.texcoord.x * _Scale,i.texcoord.y);

                float distance = length(texCoordRescaled  - pulseCenter);

                if (distance < _PulseRange)
                {
                    return _TransparencyPulse * _PulseColor;
                }
                
                return color * _Color;
            }
        ENDCG
        }
    }
}
