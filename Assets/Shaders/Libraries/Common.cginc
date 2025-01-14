#ifndef CUSTOM_COMMON_INCLUDED
#define CUSTOM_COMMON_INCLUDED

half2 TransformMainUV(half2 uv)
{
	half4 MTST = UNITY_ACCESS_INSTANCED_PROP(_MainTex_ST_arr, _MainTex_ST);
	return (uv * MTST.xy) + MTST.zw;
}

half2 TransformUV(half2 uv, half4 textureSampler)
{
	return (uv * textureSampler.xy) + textureSampler.zw;
}

#endif