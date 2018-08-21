Shader "NatureManufacture Shaders/Terrain/Terrain Snow Cover World Mapped"
{
	Properties
	{
		_SpecularPower("Specular Power", Range( 0 , 1)) = 0.01
		[NoScaleOffset]_Splat0("Splat0", 2D) = "white" {}
		_Texture1Tilling("Texture 1 Tilling", Range( 0 , 100)) = 0.5
		[NoScaleOffset]_Normal0("Normal0", 2D) = "bump" {}
		_Normal1Power("Normal 1 Power", Range( 0 , 5)) = 1
		_Albedo1SnowReduction("Albedo 1 Snow Reduction", Range( -1 , 1)) = 0
		[NoScaleOffset]_Splat1("Splat1", 2D) = "white" {}
		_Texture2Tilling("Texture 2 Tilling", Range( 0 , 100)) = 0.5
		[NoScaleOffset]_Normal1("Normal1", 2D) = "bump" {}
		_Normal2Power("Normal 2 Power", Range( 0 , 5)) = 1
		_Albedo2SnowReduction("Albedo 2 Snow Reduction", Range( -1 , 1)) = 0
		[NoScaleOffset]_Splat2("Splat2", 2D) = "white" {}
		[HideInInspector]_Smoothness3("Smoothness3", Range( 0 , 1)) = 1
		[HideInInspector]_Smoothness2("Smoothness2", Range( 0 , 1)) = 1
		[HideInInspector]_Smoothness1("Smoothness1", Range( 0 , 1)) = 1
		[HideInInspector]_Smoothness0("Smoothness0", Range( 0 , 1)) = 1
		_Texture3Tilling("Texture 3 Tilling", Range( 0 , 100)) = 0.5
		[NoScaleOffset]_Normal2("Normal2", 2D) = "bump" {}
		_Normal3Power("Normal 3 Power", Range( 0 , 5)) = 1
		_Albedo3SnowReduction("Albedo 3 Snow Reduction", Range( -1 , 1)) = 0
		[NoScaleOffset]_Splat3("Splat3", 2D) = "white" {}
		_Texture4Tilling("Texture 4 Tilling", Range( 0 , 100)) = 0.5
		[NoScaleOffset]_Normal3("Normal3", 2D) = "bump" {}
		_Normal4Power("Normal 4 Power", Range( 0 , 5)) = 1
		_Albedo4SnowReduction("Albedo 4 Snow Reduction", Range( -1 , 1)) = 0
		[HideInInspector]_Control("Control", 2D) = "white" {}
		_Snow_Amount("Snow_Amount", Range( 0 , 2)) = 0.13
		_Snow_AmountGrowSpeed("Snow_Amount Grow Speed", Range( 1 , 3)) = 2
		_SnowNoiseTilling("Snow Noise Tilling", Range( 0 , 100)) = 100
		_SnowMaskA("Snow Mask (A)", 2D) = "white" {}
		_SnowMaskPower("Snow Mask Power", Range( 0 , 2)) = 0.3
		_SnowAlbedoRGBSmoothnessA("Snow Albedo (RGB) Smoothness (A)", 2D) = "white" {}
		_SnowColor("Snow Color", Color) = (1,1,1,1)
		_SnowTilling("Snow Tilling", Range( 0 , 1000)) = 1
		_SnowSpecularPower("Snow Specular Power", Range( 0 , 1)) = 0.01
		_SnowSmoothness("Snow Smoothness", Range( 0 , 1)) = 0.2
		[NoScaleOffset]_SnowNormalRGB("Snow Normal (RGB)", 2D) = "bump" {}
		_SnowMaxAngle("Snow Max Angle ", Range( 0.001 , 90)) = 90
		_SnowHardness("Snow Hardness", Range( 1 , 10)) = 5
		_SnowNormalCoverHardness("Snow Normal Cover Hardness", Range( 0 , 10)) = 0
		[Toggle(_MESHMATERIAL_ON)] _MeshMaterial("Mesh Material", Float) = 0
		_SnowNormalPower("Snow Normal Power", Range( 0 , 5)) = 5
		_Snow_Min_Height("Snow_Min_Height", Range( -1000 , 10000)) = -1000
		_Snow_Min_Height_Blending("Snow_Min_Height_Blending", Range( 0 , 500)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry-100" }
		Cull Back
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma shader_feature _MESHMATERIAL_ON
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform float _Normal1Power;
		uniform sampler2D _Normal0;
		uniform float _Texture1Tilling;
		uniform sampler2D _Control;
		uniform float _Normal2Power;
		uniform sampler2D _Normal1;
		uniform float _Texture2Tilling;
		uniform float _Normal3Power;
		uniform sampler2D _Normal2;
		uniform float _Texture3Tilling;
		uniform float _Normal4Power;
		uniform sampler2D _Normal3;
		uniform float _Texture4Tilling;
		uniform float _SnowNormalPower;
		uniform sampler2D _SnowNormalRGB;
		uniform float _SnowTilling;
		uniform float _Albedo3SnowReduction;
		uniform float _Albedo4SnowReduction;
		uniform float _Albedo2SnowReduction;
		uniform float _Albedo1SnowReduction;
		uniform float _SnowNormalCoverHardness;
		uniform float _SnowNoiseTilling;
		uniform float _Snow_Amount;
		uniform float _Snow_AmountGrowSpeed;
		uniform float _SnowMaxAngle;
		uniform float _SnowHardness;
		uniform float _Snow_Min_Height;
		uniform float _Snow_Min_Height_Blending;
		uniform sampler2D _SnowMaskA;
		uniform float4 _SnowMaskA_ST;
		uniform float _SnowMaskPower;
		uniform float _Smoothness0;
		uniform sampler2D _Splat0;
		uniform float _Smoothness1;
		uniform sampler2D _Splat1;
		uniform float _Smoothness2;
		uniform sampler2D _Splat2;
		uniform float _Smoothness3;
		uniform sampler2D _Splat3;
		uniform float4 _SnowColor;
		uniform sampler2D _SnowAlbedoRGBSmoothnessA;
		uniform float _SpecularPower;
		uniform float _SnowSpecularPower;
		uniform float _SnowSmoothness;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float localCalculateTangents203 = ( 0.0 );
			v.tangent.xyz = cross ( v.normal, float3( 0, 0, 1 ) );
			v.tangent.w = -1;
			float4 temp_cast_0 = (localCalculateTangents203).xxxx;
			#ifdef _MESHMATERIAL_ON
				float4 staticSwitch307 = float4(0,0,0,0);
			#else
				float4 staticSwitch307 = temp_cast_0;
			#endif
			v.vertex.xyz += staticSwitch307.xyz;
		}

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float3 ase_worldPos = i.worldPos;
			float2 appendResult273 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 temp_output_275_0 = ( appendResult273 * _Texture1Tilling );
			float4 tex2DNode4 = tex2D( _Control, i.uv_texcoord );
			half Splat_1_R6 = tex2DNode4.r;
			half Splat_1_G7 = tex2DNode4.g;
			float clampResult23 = clamp( ( Splat_1_R6 + Splat_1_G7 ) , 0.0 , 1.0 );
			float3 lerpResult31 = lerp( float3( 0,0,0 ) , UnpackScaleNormal( tex2D( _Normal0, temp_output_275_0 ) ,_Normal1Power ) , clampResult23);
			float2 temp_output_277_0 = ( appendResult273 * _Texture2Tilling );
			float3 lerpResult32 = lerp( lerpResult31 , UnpackScaleNormal( tex2D( _Normal1, temp_output_277_0 ) ,_Normal2Power ) , Splat_1_G7);
			float2 temp_output_279_0 = ( appendResult273 * _Texture3Tilling );
			half Splat_1_B8 = tex2DNode4.b;
			float3 lerpResult33 = lerp( lerpResult32 , UnpackScaleNormal( tex2D( _Normal2, temp_output_279_0 ) ,_Normal3Power ) , Splat_1_B8);
			float2 temp_output_282_0 = ( appendResult273 * _Texture4Tilling );
			half Splat_1_A9 = tex2DNode4.a;
			float3 lerpResult34 = lerp( lerpResult33 , UnpackScaleNormal( tex2D( _Normal3, temp_output_282_0 ) ,_Normal4Power ) , Splat_1_A9);
			float2 appendResult202 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 temp_output_200_0 = ( appendResult202 / _SnowTilling );
			float clampResult181 = clamp( ( ( _Albedo3SnowReduction * Splat_1_B8 ) + ( _Albedo4SnowReduction * Splat_1_A9 ) + ( _Albedo2SnowReduction * Splat_1_G7 ) + ( _Albedo1SnowReduction * clampResult23 ) ) , 0.0 , 1.0 );
			float3 normalizeResult264 = normalize( UnpackScaleNormal( tex2D( _SnowNormalRGB, temp_output_200_0 ) ,_SnowNormalCoverHardness ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float simplePerlin2D204 = snoise( ( i.uv_texcoord * _SnowNoiseTilling ) );
			float temp_output_229_0 = ( 4.0 - _Snow_AmountGrowSpeed );
			float clampResult240 = clamp( pow( ( ( ( ( simplePerlin2D204 + 8.0 ) * 0.3 ) * _Snow_Amount ) / temp_output_229_0 ) , temp_output_229_0 ) , 0.0 , 2.0 );
			float clampResult225 = clamp( ase_worldNormal.y , 0.0 , 0.999999 );
			float temp_output_222_0 = ( _SnowMaxAngle / 45.0 );
			float clampResult238 = clamp( ( clampResult225 - ( 1.0 - temp_output_222_0 ) ) , 0.0 , 2.0 );
			float temp_output_218_0 = ( ( 1.0 - _Snow_Min_Height ) + ase_worldPos.y );
			float clampResult236 = clamp( ( temp_output_218_0 + 1.0 ) , 0.0 , 1.0 );
			float clampResult235 = clamp( ( ( 1.0 - ( ( temp_output_218_0 + _Snow_Min_Height_Blending ) / temp_output_218_0 ) ) + -0.5 ) , 0.0 , 1.0 );
			float clampResult244 = clamp( ( clampResult236 + clampResult235 ) , 0.0 , 1.0 );
			float temp_output_247_0 = ( pow( ( clampResult238 * ( 1.0 / temp_output_222_0 ) ) , _SnowHardness ) * clampResult244 );
			float3 lerpResult249 = lerp( normalizeResult264 , lerpResult34 , ( saturate( ( ase_worldNormal.y * clampResult240 ) ) * temp_output_247_0 ));
			float clampResult257 = clamp( ( ( WorldNormalVector( i , lerpResult249 ).y * clampResult240 ) * ( ( clampResult240 * _SnowHardness ) * temp_output_247_0 ) ) , 0.0 , 1.0 );
			float2 uv_SnowMaskA = i.uv_texcoord * _SnowMaskA_ST.xy + _SnowMaskA_ST.zw;
			float temp_output_265_0 = ( ( 1.0 - clampResult181 ) * saturate( ( clampResult257 * pow( tex2D( _SnowMaskA, uv_SnowMaskA ).a , _SnowMaskPower ) ) ) );
			float3 lerpResult183 = lerp( lerpResult34 , UnpackScaleNormal( tex2D( _SnowNormalRGB, temp_output_200_0 ) ,_SnowNormalPower ) , temp_output_265_0);
			float3 normalizeResult182 = normalize( lerpResult183 );
			o.Normal = normalizeResult182;
			float4 appendResult291 = (float4(1.0 , 1.0 , 1.0 , _Smoothness0));
			float4 lerpResult24 = lerp( float4( 0,0,0,0 ) , ( appendResult291 * tex2D( _Splat0, temp_output_275_0 ) ) , clampResult23);
			float4 appendResult295 = (float4(1.0 , 1.0 , 1.0 , _Smoothness1));
			float4 lerpResult25 = lerp( lerpResult24 , ( appendResult295 * tex2D( _Splat1, temp_output_277_0 ) ) , Splat_1_G7);
			float4 appendResult299 = (float4(1.0 , 1.0 , 1.0 , _Smoothness2));
			float4 lerpResult26 = lerp( lerpResult25 , ( appendResult299 * tex2D( _Splat2, temp_output_279_0 ) ) , Splat_1_B8);
			float4 appendResult303 = (float4(1.0 , 1.0 , 1.0 , _Smoothness3));
			float4 lerpResult27 = lerp( lerpResult26 , ( appendResult303 * tex2D( _Splat3, temp_output_282_0 ) ) , Splat_1_A9);
			float4 tex2DNode78 = tex2D( _SnowAlbedoRGBSmoothnessA, temp_output_200_0 );
			float4 lerpResult91 = lerp( lerpResult27 , ( _SnowColor * tex2DNode78 ) , temp_output_265_0);
			float4 break55 = lerpResult91;
			float3 appendResult54 = (float3(break55.r , break55.g , break55.b));
			o.Albedo = appendResult54;
			float3 appendResult285 = (float3(lerpResult27.xyz));
			float4 clampResult289 = clamp( tex2DNode78 , float4( 0,0,0,0 ) , float4( 0.5,0.5,0.5,0 ) );
			float4 lerpResult186 = lerp( float4( ( ( appendResult285 * float3( 0.3,0.3,0.3 ) ) * _SpecularPower ) , 0.0 ) , ( clampResult289 * _SnowSpecularPower ) , temp_output_265_0);
			o.Specular = lerpResult186.rgb;
			float lerpResult190 = lerp( lerpResult27.w , ( _SnowSmoothness * tex2DNode78.a ) , temp_output_265_0);
			o.Smoothness = lerpResult190;
			o.Occlusion = 1.0;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardSpecular keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandardSpecular o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandardSpecular, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}

	Dependency "BaseMapShader"="NatureManufacture Shaders/Terrain/Terrain Snow Cover Base"
	Fallback "Diffuse"
}