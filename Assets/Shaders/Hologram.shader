Shader "Unlit/Hologram"
{
	Properties
	{
		_Color ("Color", Color) = (0.25, 0.25, 1, 0.8)
		_LineSize ("Line Size (Pixels)", Float) = 10
		_TimeScale ("Time Scale", Float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 100

		Pass
		{
            Stencil {
                Ref 1
                Comp NotEqual
                Pass IncrSat
                ZFail Keep
            }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			fixed4 _Color;
			float _LineSize;
			float _TimeScale;

			void vert (float4 vertex : POSITION, out float4 outPos : SV_POSITION)
			{
				outPos = UnityObjectToClipPos(vertex);
			}
			
			fixed4 frag (UNITY_VPOS_TYPE screen : VPOS) : SV_Target
			{
				// sample the texture
				fixed4 col = _Color;

				col.a *= abs(sin(3.1415 * ((screen.y * _ScreenParams.y / _LineSize) + _Time.y * _TimeScale)));
				// apply fog
				return col;
			}
			ENDCG
		}
	}
}
