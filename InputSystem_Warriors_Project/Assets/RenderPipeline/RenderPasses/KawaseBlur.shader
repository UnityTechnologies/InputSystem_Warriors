Shader "Custom/RenderFeature/KawaseBlur"
{
    Properties
    {
        _Tint("Color Tint", Color) = (.34, .85, .92, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        LOD 100
        ZWrite Off ZTest Always Cull Off

        Pass
        {
            Name "KawaseBlur"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // Blit.hlsl supplies the fullscreen-triangle vertex shader (Vert), the Attributes and
            // Varyings structs, and the _BlitTexture the render graph blit binds the source to.
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float4 _Tint;
            float _offset;

            half4 frag (Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                float2 res = _BlitTexture_TexelSize.xy;
                float2 uv = input.texcoord;
                float i = _offset;

                half3 col = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv).rgb;
                col += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv + float2( i,  i) * res).rgb;
                col += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv + float2( i, -i) * res).rgb;
                col += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv + float2(-i,  i) * res).rgb;
                col += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv + float2(-i, -i) * res).rgb;
                col /= 5.0h;
                col *= _Tint.rgb;

                return half4(col, 1.0h);
            }
            ENDHLSL
        }
    }
}
