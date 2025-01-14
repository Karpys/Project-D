Shader "Mila/Environment/_ZESHADER (Opaque-Transparent)"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)

		_Cutoff ("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
		_CullMode("Cull Mode", Int) = 2

		_SrcBlend ("Src Blend", Float) = 1
		_DstBlend ("Dst Blend", Float) = 0
		_ZWrite ("Z Write", Float) = 1
		_ZTest ("Z Test", Float) = 2
		
        _ShadowRamp ("Shadow Ramp", 2D) = "black" {}
        _CutShadowColor ("Cut Shadow Color", Color) = (1,1,1,1)
        _DiffuseShadowColor ("Diffuse Shadow Color", Color) = (1,1,1,1)
		
        _NormalMap ("Texture", 2D) = "bump" {}
		_NormalScale ("Scale", Float) = 1
		
		_DetailTex("Texture", 2D) = "black" {}
		_DetailColor("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		
		_VColInfluence ("Influence", Range(0.0, 1.0)) = 1.0
		
        _LightmapThreshold ("Threshold", Float) = 0.5
	}

	SubShader //LIGHTMAP BAKING (editor only)
    {
        Pass
        {
            Name "META"
            Tags {"LightMode" = "Meta"}
			Cull [_CullMode]

            CGPROGRAM
    
            #include"UnityStandardMeta.cginc"
    
            sampler2D _GIAlbedoTex;
            fixed4 _GIAlbedoColor;

            float4 frag_meta2(v2f_meta i) : SV_Target
            {
                FragmentCommonData data = UNITY_SETUP_BRDF_INPUT(i.uv);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT(UnityMetaInput, o);
                fixed4 c = tex2D(_GIAlbedoTex, i.uv);
                float4 mainTex = tex2D(_MainTex, i.uv);
                o.Albedo = mainTex * _Color;
                return UnityMetaFragment(o);
            }
    
            #pragma vertex vert_meta
            #pragma fragment frag_meta2
            #pragma shader_feature _EMISSION
            #pragma shader_feature _METALLICGLOSSMAP
            #pragma shader_feature ___ _DETAIL_MULX2

            ENDCG
        }
    }
	
	SubShader
	{
		
		Pass
		{
            Name "ZESHADER"
			Tags { "LightMode" = "ForwardBase" }
			Blend [_SrcBlend] [_DstBlend]
			ZWrite [_ZWrite]
			ZTest [_ZTest]
			Cull [_CullMode]

			CGPROGRAM

			#pragma shader_feature _CLIPPING

			#pragma shader_feature _VLIT
			#pragma shader_feature _FLIT
			#pragma shader_feature _LIGHTMAPPED

			#pragma shader_feature _NORMAL
			
			#pragma shader_feature _DETAIL

			#pragma shader_feature DET_ADD
			#pragma shader_feature _DET_MLT
			
			#pragma shader_feature _VCOL
			
			#pragma shader_feature _SHOW_LIGHTMAP

			#pragma multi_compile_instancing
			
            #pragma multi_compile_fog
			#pragma shader_feature _FOG

			#pragma vertex vert
			#pragma fragment frag

			#include "../Passes/UniversalPass.cginc"

			ENDCG
		}
	}
	CustomEditor "ZeShaderGUI"
}
