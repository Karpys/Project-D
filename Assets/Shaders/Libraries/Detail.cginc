#ifndef CUSTOM_DETAIL_INCLUDED
#define CUSTOM_DETAIL_INCLUDED

// #ifdef DET_OVRL
// half3 ApplyOverlay (half4 diff, half4 det)
// {
//     half luminance =  dot(det, half4(0.2126, 0.7152, 0.0722, 0));
//     det = lerp(diff, 1-2*(1-det)*(1-diff), det.a);
//     return det.rgb;
// }
// #endif

void ApplyDetail (inout v2f i, inout half4 col)
{
	fixed4 detail = tex2D(_DetailTex, i.uv.zw) * UNITY_ACCESS_INSTANCED_PROP(_DetailColor_arr, _DetailColor);

	#ifdef DET_ADD
		col.rgb = lerp(col.rgb, saturate(col.rgb + detail.rgb), detail.a); //Additive
	#else
		#ifdef _DET_MLT
			col.rgb = lerp(col.rgb, col.rgb * detail.rgb, detail.a); //Multiply
		#else
				col.rgb = lerp(col.rgb, detail.rgb, detail.a); //Value
		#endif
	#endif
}

#endif