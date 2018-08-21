Shader "NatureManufacture Shaders/Terrain/Terrain Snow Cover Base"
{
	Properties
	{
		_SpecularPower("Specular Power", Range( 0 , 1)) = 0.01
		_MainTex("MainTex", 2D) = "white" {}
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
		_SnowNormalRGB("Snow Normal (RGB)", 2D) = "bump" {}
		_SnowMaxAngle("Snow Max Angle ", Range( 0.001 , 90)) = 90
		_SnowHardness("Snow Hardness", Range( 1 , 10)) = 5
		_SnowNormalCoverHardness("Snow Normal Cover Hardness", Range( 0 , 10)) = 0
		_Snow_Min_Height("Snow_Min_Height", Range( -1000 , 10000)) = -1000
		_Snow_Min_Height_Blending("Snow_Min_Height_Blending", Range( 0 , 500)) = 1
		[Toggle(_MESHMATERIAL_ON)] _MeshMaterial("Mesh Material", Float) = 0
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
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _SnowColor;
		uniform sampler2D _SnowAlbedoRGBSmoothnessA;
		uniform float _SnowTilling;
		uniform float _SnowNormalCoverHardness;
		uniform sampler2D _SnowNormalRGB;
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
				float4 staticSwitch308 = float4(0,0,0,0);
			#else
				float4 staticSwitch308 = temp_cast_0;
			#endif
			v.vertex.xyz += staticSwitch308.xyz;
		}

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			o.Normal = float3(0,0,1);
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode292 = tex2D( _MainTex, uv_MainTex );
			float3 ase_worldPos = i.worldPos;
			float2 appendResult202 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 temp_output_200_0 = ( appendResult202 / _SnowTilling );
			float4 temp_output_268_0 = ( _SnowColor * tex2D( _SnowAlbedoRGBSmoothnessA, temp_output_200_0 ) );
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
			float3 lerpResult249 = lerp( normalizeResult264 , float3( 0,0,1 ) , ( saturate( ( ase_worldNormal.y * clampResult240 ) ) * temp_output_247_0 ));
			float clampResult257 = clamp( ( ( WorldNormalVector( i , lerpResult249 ).y * clampResult240 ) * ( ( clampResult240 * _SnowHardness ) * temp_output_247_0 ) ) , 0.0 , 1.0 );
			float2 uv_SnowMaskA = i.uv_texcoord * _SnowMaskA_ST.xy + _SnowMaskA_ST.zw;
			float temp_output_265_0 = ( 1.0 * saturate( ( clampResult257 * pow( tex2D( _SnowMaskA, uv_SnowMaskA ).a , _SnowMaskPower ) ) ) );
			float4 lerpResult91 = lerp( tex2DNode292 , temp_output_268_0 , temp_output_265_0);
			o.Albedo = lerpResult91.rgb;
			float3 appendResult290 = (float3(tex2DNode292.rgb));
			float4 clampResult288 = clamp( temp_output_268_0 , float4( 0,0,0,0 ) , float4( 0.5,0.5,0.5,0 ) );
			float4 lerpResult186 = lerp( float4( ( appendResult290 * _SpecularPower ) , 0.0 ) , ( clampResult288 * _SnowSpecularPower ) , temp_output_265_0);
			o.Specular = lerpResult186.rgb;
			float lerpResult190 = lerp( tex2DNode292.a , _SnowSmoothness , temp_output_265_0);
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
	Fallback "Diffuse"
}