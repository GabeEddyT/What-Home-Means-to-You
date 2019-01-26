Shader "Sprites/Spritz"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_First_Swap("First_Swap",Color) = (0,0,1,1)
		_First_Replace("First_Replace",Color) = (1,1,1,1)
		_Second_Swap	("Second_Swap",Color) = (1,0,0,1)
		_Second_Replace	("Second_Replace",Color) = (1,1,1,1)
		_Third_Swap		("Third_Swap",Color) = (0,1,0,1)
		_Third_Replace	("Third_Replace",Color) = (1,1,1,1)
		_Fourth_Swap	("Fourth_Swap",Color) = (1,1,0,1)
		_Fourth_Replace	("Fourth_Replace",Color) = (1,1,1,1)
		_SwapTex("Color Data", 2D) = "transparent" {}
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
				};

				fixed4 _Color;
				fixed4 _First_Swap;
				fixed4 _First_Replace;
				fixed4 _Second_Swap;
				fixed4 _Second_Replace;
				fixed4 _Third_Swap;
				fixed4 _Third_Replace;
				fixed4 _Fourth_Swap;
				fixed4 _Fourth_Replace;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif
					return OUT;
				}

				sampler2D _MainTex;
				sampler2D _AlphaTex;
				float _AlphaSplitEnabled;

				fixed4 SampleSpriteTexture(float2 uv)
				{
					fixed4 color = tex2D(_MainTex, uv);

	#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
					if (_AlphaSplitEnabled)
						color.a = tex2D(_AlphaTex, uv).r;
	#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

					return color;
				}

				bool nearlyEqual(float a, float b)
				{
					return (abs(a - b) < .3);
				}

				bool nearlyEqual4(fixed4 a, fixed4 b)
				{
					return nearlyEqual(a.r,b.r) && nearlyEqual(a.g,b.g) && nearlyEqual(a.b,b.b) &&nearlyEqual(a.a,b.a);
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
					c.rgb *= c.a;

					if(nearlyEqual4(c, _First_Swap)){
						// return fixed4(c.r,c.r,c.r,c.r);
						return _First_Replace;
					}else if(nearlyEqual4(c, _Second_Swap)){
						return _Second_Replace;
					}else if (nearlyEqual4(c, _Third_Swap)) {
						return _Third_Replace;
					}else if (nearlyEqual4(c, _Fourth_Swap)) {
						return _Fourth_Replace;
					}

					return c;
				}
			ENDCG
			}
		}
}