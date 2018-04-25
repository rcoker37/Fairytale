Shader "Unlit/Candle"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)

		_CenterPositionX("CenterPositionX", float) = 0.0
		_CenterPositionY("CenterPositionY", float) = 0.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
            ZTest Off
            Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
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
				float2 screenPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			float _CenterPositionX;
			float _CenterPositionY;

            // Globals
			float _GrayScaleDist;
            float _FadeMultiplier;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.screenPos = ComputeScreenPos(o.vertex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 screenPos = i.screenPos * float2(1.0f, _ScreenParams.y / _ScreenParams.x);
				float x = screenPos.x - _CenterPositionX;
				float y = screenPos.y - _CenterPositionY;
				float dist = sqrt(pow(x, 2) + pow(y, 2));
				half4 texcol = tex2D(_MainTex, i.uv);
                half3 grayscale = 0.5 * lerp(texcol.rgb, dot(texcol.rgb, float3(0.3, 0.59, 0.11)), 1.0);
                float fadeDist = _FadeMultiplier * _GrayScaleDist;
				if (dist > _GrayScaleDist) {
					texcol.rgb = grayscale;
				} else if (dist > fadeDist) {
                    texcol.rgb = 1.0 / (_GrayScaleDist - fadeDist) * ((_GrayScaleDist - dist) * (texcol.rgb) + (dist - fadeDist) * grayscale);
                }

				return texcol;
			}
			ENDCG
		}
	}
}
