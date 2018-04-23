Shader "Example/AlphaTest" 
{
	Properties{
		_MainColor("Main Color", Color) = (1, 1, 1, 1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	    _TestVal("Test Value", Range(0, 1)) = 0.5
		_ScrollXSpeed("X Scroll Speed", Range(-100, 100)) = 20
		_ScrollYSpeed("Y Scroll Speed", Range(-100, 100)) = 20

	}
		SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
#pragma surface surf Lambert alphatest:_TestVal

		fixed4 _MainColor;
		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;
	sampler2D _MainTex;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed2 scrolledUV = IN.uv_MainTex;

		//Create variables that store the individual x and y 
		//components for the uv's scaled by time
		fixed xScrollValue = _ScrollXSpeed * _Time;
		fixed yScrollValue = _ScrollYSpeed * _Time;

		//Apply the final uv offset
		scrolledUV += fixed2(xScrollValue, yScrollValue);

		//Apply textures and tint
		half4 c = tex2D(_MainTex, scrolledUV);

		//half4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = _MainColor.rgb * c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
