Shader "Custom/UVAnimationShader" {
    Properties{
        _MainTex1("Texture 1", 2D) = "white" {} // 第一张纹理
        _MainTex2("Texture 2", 2D) = "white" {} // 第二张纹理
        _MainTex3("Texture 3", 2D) = "white" {} // 第三张纹理
        _ScrollSpeed("Scroll Speed", Vector) = (1, 0, 0, 0) // UV滚动速度
    }
        SubShader{
            Tags { "Queue" = "Transparent" } // 设置渲染队列为透明
            Blend SrcAlpha OneMinusSrcAlpha // 设置混合模式为透明度混合

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex1;
                sampler2D _MainTex2;
                sampler2D _MainTex3;
                float4 _ScrollSpeed;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                half4 frag(v2f i) : SV_Target {
                    float progress = saturate(_Time.y * _ScrollSpeed.w); // 计算播放进度，范围在0到1之间
                    float2 offset = _ScrollSpeed.xy * progress; // 根据进度计算UV偏移量
                    float2 uv = i.uv + offset;

                    // 根据进度选择纹理并返回颜色
                    half4 col;
                    if (progress < 0.333) {
                        col = tex2D(_MainTex1, uv);
                    }
     else if (progress < 0.667) {
      col = tex2D(_MainTex2, uv);
  }
else {
 col = tex2D(_MainTex3, uv);
}

return col;
}
ENDCG
}
        }
            Fallback "Diffuse" // 如果硬件不支持透明渲染，则使用Diffuse渲染
}
