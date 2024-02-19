Shader "Unlit/ShadowShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags {
            "RenderType" = "Transparent"
        }

        // defines the level of detail i guess
        // this is the default value for unlit
        // shaders so i think it's good?
        // https://docs.unity3d.com/560/Documentation/Manual/SL-ShaderLOD.html
        LOD 100

        // allow transparency i think
        // https://docs.unity3d.com/Manual/SL-Blend.html
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct VertexInfo {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct FragmentInfo {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            FragmentInfo vert(VertexInfo input) {
                FragmentInfo output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.uv = input.uv;

                return output;
            }

            fixed4 frag(FragmentInfo input) : SV_Target {
                fixed4 textureColor = tex2D(_MainTex, input.uv);

                return fixed4(0.8, 1.0, 0.9, textureColor.a * 0.1);
            }
            ENDCG
        }
    }
}
