#ifndef CUSTOM_COMMONLIT_INCLUDED
#define CUSTOM_COMMONLIT_INCLUDED

#ifdef _NORMAL
	#include "UnityStandardBRDF.cginc"
	
	half3 CreateBinormal (float3 normal, float3 tangent, float binormalSign)
	{
		return cross(normal, tangent.xyz) * (binormalSign * unity_WorldTransformParams.w);
	}
	
	#ifdef _FLIT
		void InitializeFragmentNormal(inout v2f i)
		{
		    float3 normalMap = UnpackScaleNormal(tex2D(_NormalMap, i.uv.xy), UNITY_ACCESS_INSTANCED_PROP(_NormalScale_arr, _NormalScale));
		    i.normal = normalize(
		        normalMap.x * i.tangent +
		        normalMap.y * i.binormal +
		        normalMap.z * i.normal
		    );
		}
	#endif
#endif

#ifdef _LIT
	void ProcessLitVert(appdata v, inout v2f o)
	{
		half3 normal = UnityObjectToWorldNormal(v.normal);
		
		#ifdef _FLIT
			o.normal = normal;
			#ifdef _NORMAL
				o.tangent = UnityObjectToWorldDir(v.tangent.xyz);
				o.binormal = CreateBinormal(o.normal, o.tangent, v.tangent.w);
			#endif
		#else
			#ifdef _VLIT
				o.light = DotClamped(_WorldSpaceLightPos0.xyz, normal);
			#endif
		#endif
	
	}
#endif

void ProcessLitFrag(v2f i, inout half4 col, half lightmapValue)
{
	half light = 1.0;

	#ifdef _FLIT
		light = DotClamped(_WorldSpaceLightPos0.xyz, i.normal);
	#else
		#ifdef _VLIT
			light = i.light;
		#endif
	#endif
	
	half4 diffShadowColor = UNITY_ACCESS_INSTANCED_PROP(_DiffuseShadowColor_arr, _DiffuseShadowColor);
	half4 diffuseShadow = lerp(diffShadowColor, 1.0, (1-diffShadowColor.a));
	col.rgb *= lerp(diffuseShadow, 1.0, light * _LightColor0 * saturate(lightmapValue));
	
	half4 cutShadowColor = UNITY_ACCESS_INSTANCED_PROP(_CutShadowColor_arr, _CutShadowColor);
	half4 shadowMap = UNITY_ACCESS_INSTANCED_PROP(_ShadowRamp_ST_arr, _ShadowRamp_ST);
	light = tex2D(_ShadowRamp, half2(shadowMap.x * light * lightmapValue + shadowMap.z, 0.0));
	half4 cutShadow = lerp(cutShadowColor, 1.0, (1-cutShadowColor.a));
	col.rgb *= lerp(cutShadow, 1.0, light * _LightColor0);
}

#endif