#ifndef CUSTOM_UNIVERSAL_PASS_INCLUDED
#define CUSTOM_UNIVERSAL_PASS_INCLUDED

#ifndef _LIGHTMAPPED
	#ifdef _VLIT
		#define _LIT
	#endif
	#ifdef _FLIT
		#define _LIT
	#endif
#endif


#ifdef _LIT
	#define _SHADOWMAP
#else
	#ifdef _LIGHTMAPPED
		#define _SHADOWMAP
	#endif
#endif

#ifdef _SHADOWMAP
	#include "UnityPBSLighting.cginc"
	sampler2D _ShadowRamp;
#else
	#include "UnityCG.cginc"
#endif

#ifdef _NORMAL
	sampler2D _NormalMap;
#endif

sampler2D _MainTex;

#ifdef _DETAIL
	sampler2D _DetailTex;
#endif

UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
	
		UNITY_DEFINE_INSTANCED_PROP(half4, _MainTex_ST)
	#define _MainTex_ST_arr UnityPerMaterial
		UNITY_DEFINE_INSTANCED_PROP(half4, _Color)
	#define _Color_arr UnityPerMaterial
		UNITY_DEFINE_INSTANCED_PROP(half, _Cutoff)
	#define _Cutoff_arr UnityPerMaterial
	
	#ifdef _SHADOWMAP
		UNITY_DEFINE_INSTANCED_PROP(half4, _ShadowRamp_ST)
	#define _ShadowRamp_ST_arr UnityPerMaterial
		UNITY_DEFINE_INSTANCED_PROP(half4, _CutShadowColor)
	#define _CutShadowColor_arr UnityPerMaterial
		UNITY_DEFINE_INSTANCED_PROP(half4, _DiffuseShadowColor)
	#define _DiffuseShadowColor_arr UnityPerMaterial
	#endif
	
	#ifdef _NORMAL
		UNITY_DEFINE_INSTANCED_PROP(half, _NormalScale)
	#define _NormalScale_arr UnityPerMaterial
	#endif
	
	#ifdef _DETAIL
		UNITY_DEFINE_INSTANCED_PROP(half4, _DetailTex_ST)
#define _DetailTex_ST_arr UnityPerMaterial
	UNITY_DEFINE_INSTANCED_PROP(half4, _DetailColor)
#define _DetailColor_arr UnityPerMaterial
	#endif
	
	#ifdef _VCOL
		UNITY_DEFINE_INSTANCED_PROP(half, _VColInfluence)
	#define _VColInfluence_arr UnityPerMaterial
	#endif
	
	#ifdef _LIGHTMAPPED
		UNITY_DEFINE_INSTANCED_PROP(half, _LightmapThreshold)
	#define _LightmapThreshold_arr UnityPerMaterial
	#endif
	
UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

#include "../Libraries/Common.cginc"

struct appdata
{
	half4 vertex : POSITION;
	half2 uv : TEXCOORD0;
	half2 uv2 : TEXCOORD1;
	half2 uv3 : TEXCOORD2;

	#ifdef _LIT
		half3 normal : NORMAL;
	#endif

	#ifdef _NORMAL
		half4 tangent : TANGENT;
	#endif
	
	#ifdef _VCOL
		half4 color : COLOR;
	#endif

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f
{
	half4 uv : TEXCOORD0;

	#ifdef _FOG
		UNITY_FOG_COORDS(5)
	#endif

	half4 vertex : SV_POSITION;
	
	#ifdef _FLIT
		half3 normal : TEXCOORD1;

		#ifdef _NORMAL
			half3 tangent : TEXCOORD2;
			half3 binormal : TEXCOORD3;
		#endif
	#else
		#ifdef _VLIT
			half light : TEXCOORD1;
		#endif
	#endif

	#ifdef _VCOL
		half4 color : COLOR;
	#endif
	
	#ifdef _LIGHTMAPPED
		half2 lightmapUV : TEXCOORD4;
	#endif

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

#ifdef _LIT
	#include "../Libraries/CommonLit.cginc"
#else
	#ifdef _LIGHTMAPPED
		#include "../Libraries/CommonLit.cginc"
	#endif
#endif

#ifdef _DETAIL
	#include "../Libraries/Detail.cginc"
#endif


v2f vert (appdata v) {
	v2f o;
	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_TRANSFER_INSTANCE_ID(v, o);
	o.vertex = UnityObjectToClipPos(v.vertex);
	
	o.uv.xy = TransformMainUV(v.uv);
	o.uv.zw = TransformMainUV(v.uv2);

	#ifdef _DETAIL
		o.uv.zw = TransformUV(v.uv3, UNITY_ACCESS_INSTANCED_PROP(_DetailTex_ST_arr, _DetailTex_ST));
	#endif
	
	#ifdef _LIGHTMAPPED
		o.lightmapUV = v.uv2 * unity_LightmapST.xy + unity_LightmapST.zw;
	#endif
	
	#ifdef _FOG
		UNITY_TRANSFER_FOG(o,o.vertex);
	#endif
	
	#ifdef _VCOL
		o.color = v.color;
	#endif

	#ifdef _LIT
		ProcessLitVert(v,o);
	#endif

	return o;
}

#ifdef _LIGHTMAPPED
	#include "../Libraries/Lightmapped.cginc"
#endif

half4 frag (v2f i) : SV_TARGET {
	UNITY_SETUP_INSTANCE_ID(i);

	half4 col = tex2D(_MainTex, i.uv.xy) * UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);

	#ifdef _DETAIL
		ApplyDetail(i, col);
	#endif

	#ifdef _CLIPPING
		clip(col.a - UNITY_ACCESS_INSTANCED_PROP(_Cutoff_arr, _Cutoff));
	#endif
	
	#ifdef _NORMAL
		#ifdef _FLIT
			InitializeFragmentNormal(i);
		#endif
	#endif

	half lightmapValue = 1.0;

	#ifdef _LIGHTMAPPED
		lightmapValue = GetLightmapLuminance(i);
		#ifdef _SHOW_LIGHTMAP
			return lightmapValue.rrrr;
		#endif
	#endif
	
	#ifdef _LIT
		ProcessLitFrag(i, col, lightmapValue);
	#else
		#ifdef _LIGHTMAPPED
			ProcessLitFrag(i, col, lightmapValue);
		#endif
	#endif

	#ifdef _VCOL
		col = lerp(col, col * i.color, i.color.a * UNITY_ACCESS_INSTANCED_PROP(_VColInfluence_arr, _VColInfluence));
	#endif
	
	#ifdef _FOG
		UNITY_APPLY_FOG(i.fogCoord, col);
	#endif

	return col;
}

#endif