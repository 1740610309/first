Shader "Custom/Blue" {
    Properties{
        _Color("Color", Color) = (0, 0, 1, 0.5) // 初始颜色为半透明的蓝色
        _MainTex("Texture", 2D) = "white" {}     // 主贴图
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }

            CGPROGRAM
            #pragma surface surf Lambert

            struct Input {
                float2 uv_MainTex;
            };

            sampler2D _MainTex;
            fixed4 _Color;

            void surf(Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
    }
        SubShader{
            Tags { "RenderType" = "Transparent" }

            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma surface surf Lambert alpha

            struct Input {
                float2 uv_MainTex;
            };

            sampler2D _MainTex;
            fixed4 _Color;

            void surf(Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
            }
                Fallback "Diffuse"
}
