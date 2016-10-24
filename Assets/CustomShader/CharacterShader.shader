Shader "Custom/CharacterShader" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,0.5)
		_SpecColor ("Spec Color", Color) = (1,1,1,1)
		_Emission ("Emmisive Color", Color) = (0.5,0.5,0.5,0.5)
		_Shininess ("Shininess", Range(0.01, 1)) = 0.7
		_BumpMap ("Bump Map", 2D) = "bump" {}
		_RimColor ("Rim Color", Color) = (1,1,1,1)
		_RimPower ("Rim Power", Range(0.1, 0.8)) = 1.0
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader {
		Tags { "RenderType" = "Opaque"}
		Pass {
			Material {
				Diffuse [_Color]
				Ambient [_Color]
				Shininess [_Shininess]
				Specular [_SpecColor]
				Emission [_Emission]
			}
			Lighting On
			SeparateSpecular On
			SetTexture [_MainTex] {
				constantColor [_Color]
				Combine texture * primary DOUBLE, texture * constant
			}
		}
		CGPROGRAM
		#pragma surface surf Lambert
		struct Input {
			float2 uvMainTexture;
			float2 uvBumpMap;
			float3 viewDirection;
		};
		sampler2D  _MainTex;
		sampler2D _BumpMap;
		float4 _RimColor;
		float _RimPower;

		void surf(Input IN, inout SurfaceOutput o)
		{
			o.Albedo = tex2D (_MainTex, IN.uvMainTexture).rgb;
			o.Normal = UnpackNormal(tex2D (_BumpMap, IN.uvBumpMap));
			half rim = 1.0 - saturate(dot(normalize(IN.viewDirection), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower);
		}
		ENDCG
	}
	Fallback "Diffuse"
}