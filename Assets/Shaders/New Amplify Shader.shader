// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify Shader/Elephant"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "white" {}
		_BumpMap("NormalMap", 2D) = "white" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Emission("Emission", Range( 0 , 1)) = 0
		_SecondColor("SecondColor", Color) = (1,1,1,1)
		_SecondTex("SecondTex", 2D) = "white" {}
		_SwitchPosX("SwitchPosX", Range( 0 , 1)) = 0
		[Toggle]_IsChange("IsChange", Float) = 1
		_MeshXMin("MeshXMin", Float) = 0
		_MeshXMax("MeshXMax", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _BumpMap;
		uniform float4 _BumpMap_ST;
		uniform float _IsChange;
		uniform float _SwitchPosX;
		uniform float _MeshXMin;
		uniform float _MeshXMax;
		uniform float4 _Color;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _SecondColor;
		uniform sampler2D _SecondTex;
		uniform float4 _SecondTex_ST;
		uniform float _Emission;
		uniform float _Metallic;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_BumpMap = i.uv_texcoord * _BumpMap_ST.xy + _BumpMap_ST.zw;
			o.Normal = tex2D( _BumpMap, uv_BumpMap ).rgb;
			float3 ase_worldPos = i.worldPos;
			float3 objToWorld12 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float2 uv_SecondTex = i.uv_texcoord * _SecondTex_ST.xy + _SecondTex_ST.zw;
			float4 temp_output_10_0 = ( _SecondColor * tex2D( _SecondTex, uv_SecondTex ) );
			float4 ifLocalVar23 = 0;
			if( ase_worldPos.x <= ( (_MeshXMin + (_SwitchPosX - 0.0) * (_MeshXMax - _MeshXMin) / (1.0 - 0.0)) + objToWorld12.x ) )
				ifLocalVar23 = temp_output_10_0;
			else
				ifLocalVar23 = ( _Color * tex2D( _MainTex, uv_MainTex ) );
			o.Albedo = lerp(ifLocalVar23,temp_output_10_0,_IsChange).rgb;
			float3 temp_cast_2 = (_Emission).xxx;
			o.Emission = temp_cast_2;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15600
48;17;1906;1004;2598.095;948.2148;2.031741;True;False
Node;AmplifyShaderEditor.RangedFloatNode;15;-1990.608,-772.485;Float;False;Constant;_Float0;Float 0;2;0;Create;False;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-2039.097,-890.05;Float;False;Property;_SwitchPosX;SwitchPosX;8;0;Create;True;0;0;False;0;0;0.471;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1983.245,-688.3561;Float;False;Constant;_Float1;Float1;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2000.876,-570.4326;Float;False;Property;_MeshXMin;MeshXMin;10;0;Create;True;0;0;False;0;0;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-1997.007,-469.6858;Float;False;Property;_MeshXMax;MeshXMax;11;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-1014.362,245.6381;Float;False;Property;_SecondColor;SecondColor;6;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TransformPositionNode;12;-1597.836,-556.1281;Float;False;Object;World;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TFHCRemapNode;20;-1591.007,-809.6858;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-1026.168,-248.3564;Float;False;Property;_Color;Color;0;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1018.432,-2.641487;Float;True;Property;_MainTex;MainTex;1;0;Create;True;0;0;False;0;None;e70a4cc9a27a530468623a76c6c025fe;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1019.453,452.0042;Float;True;Property;_SecondTex;SecondTex;7;0;Create;True;0;0;False;0;None;80ab37a9e4f49c842903bb43bdd7bcd2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-1177.903,-520.3312;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;11;-1289.243,-1071.572;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-587.8392,-69.82073;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-603.0437,313.5405;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ConditionalIfNode;23;-157.0667,-622.4154;Float;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;29;320.6585,49.06642;Float;False;Property;_Smoothness;Smoothness;4;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;26;273.8217,-424.3845;Float;True;Property;_BumpMap;NormalMap;2;0;Create;False;0;0;False;0;None;9a4a55d8d2e54394d97426434477cdcf;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;27;348.4329,-219.4891;Float;False;Property;_Emission;Emission;5;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;298.3494,-102.574;Float;False;Property;_Metallic;Metallic;3;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;25;302.6307,-565.2242;Float;False;Property;_IsChange;IsChange;9;0;Create;True;0;0;False;0;1;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1257.725,-391.6629;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Amplify Shader/Elephant;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;20;0;14;0
WireConnection;20;1;15;0
WireConnection;20;2;16;0
WireConnection;20;3;17;0
WireConnection;20;4;18;0
WireConnection;21;0;20;0
WireConnection;21;1;12;1
WireConnection;7;0;6;0
WireConnection;7;1;1;0
WireConnection;10;0;9;0
WireConnection;10;1;2;0
WireConnection;23;0;11;1
WireConnection;23;1;21;0
WireConnection;23;2;7;0
WireConnection;23;3;10;0
WireConnection;23;4;10;0
WireConnection;25;0;23;0
WireConnection;25;1;10;0
WireConnection;0;0;25;0
WireConnection;0;1;26;0
WireConnection;0;2;27;0
WireConnection;0;3;28;0
WireConnection;0;4;29;0
ASEEND*/
//CHKSM=FABC71336716BE93538B4E79AAE0A6C69FB1E755