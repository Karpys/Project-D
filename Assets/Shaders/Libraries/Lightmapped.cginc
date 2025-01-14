#ifndef CUSTOM_LIGHTMAPPED_INCLUDED
#define CUSTOM_LIGHTMAPPED_INCLUDED

half LightMapThreshold()
{
    return UNITY_ACCESS_INSTANCED_PROP(_LightmapThreshold_arr, _LightmapThreshold);
}

void ApplyLightmapRaw(inout half4 color, v2f i)
{
    half3 lightmap = DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lightmapUV));
    color *= half4(saturate(lightmap.rgb) * LightMapThreshold(), 1.0);
}

void ApplyLightmapSDF(inout half4 color, v2f i, half4 shadowColor)
{
    half3 lightmap = (DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lightmapUV)));
    float shadow = saturate(lightmap.r) > LightMapThreshold() ? 1.0 : 0.0;
    color *= lerp(shadowColor,half4(1,1,1,1),saturate(shadow + (1 - shadowColor.a)));
}

void ApplyLightmapLuminanceSDF(inout half4 color, v2f i, half4 shadowColor)
{
    half3 lightmap = (DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lightmapUV)));
    float shadow = (lightmap.r + lightmap.g + lightmap.b) > LightMapThreshold() ? 1.0 : 0.0;
    color *= lerp(shadowColor,half4(1,1,1,1),saturate(shadow + (1 - shadowColor.a)));
}

half4 GetLightmapRaw(v2f i)
{
    half3 lightmap = DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lightmapUV));
    return half4(lightmap.rgb * LightMapThreshold(), 1.0);
}

half GetLightmapSDF(v2f i)
{
    half3 lightmap = (DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lightmapUV)));
    float shadow = saturate(lightmap.r) > LightMapThreshold() ? 1.0 : 0.0;
    return saturate(shadow);
}

half GetLightmapLuminance(v2f i)
{
    half3 lightmap = DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lightmapUV));
    return (lightmap.r + lightmap.g + lightmap.b) * LightMapThreshold();
}

half GetLightmapLuminanceSDF(v2f i)
{
    half3 lightmap = (DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lightmapUV)));
    float shadow = (lightmap.r + lightmap.g + lightmap.b) > LightMapThreshold() ? 1.0 : 0.0;
    return saturate(shadow);
}

#endif