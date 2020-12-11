Shader "Unlit/UnlitDoubleSided" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        Cull Off
        LOD 100

        Pass {
            Tags {"LightMode" = "ForwardBase"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                LIGHTING_COORDS(1, 2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            uniform float4 _Color;

            v2f vert (appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float atten = LIGHT_ATTENUATION(i);
                fixed4 col = tex2D(_MainTex, i.uv) * _Color * atten;
                return col;
            }
            ENDCG
        }

        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
