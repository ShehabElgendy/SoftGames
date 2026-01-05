Shader "Softgames/FireParticle"
{
    // WebGL-optimized fire particle shader for URP
    // Features: Soft particles, color tinting, animated distortion

    Properties
    {
        _MainTex ("Particle Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "gray" {}
        [HDR] _Color ("Tint Color", Color) = (1, 0.5, 0.1, 1)
        _Intensity ("Intensity", Range(0.5, 5)) = 2
        _DistortionStrength ("Distortion", Range(0, 0.5)) = 0.1
        _ScrollSpeed ("Scroll Speed", Range(0, 5)) = 1
        _SoftParticles ("Soft Particles Factor", Range(0, 3)) = 1
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent+100"
            "RenderPipeline" = "UniversalPipeline"
            "IgnoreProjector" = "True"
        }

        Pass
        {
            Name "FireParticle"

            Blend One One // Additive blending
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_particles
            #pragma multi_compile_fog

            // WebGL compatibility
            #pragma target 3.0

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                float2 noiseUV : TEXCOORD1;
                float4 projectedPosition : TEXCOORD2;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_NoiseTex);
            SAMPLER(sampler_NoiseTex);
            TEXTURE2D(_CameraDepthTexture);
            SAMPLER(sampler_CameraDepthTexture);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _NoiseTex_ST;
                half4 _Color;
                half _Intensity;
                half _DistortionStrength;
                half _ScrollSpeed;
                half _SoftParticles;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output;

                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.color = input.color;
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);

                // Animated noise UV
                float2 noiseOffset = float2(0, _Time.y * _ScrollSpeed);
                output.noiseUV = TRANSFORM_TEX(input.uv, _NoiseTex) + noiseOffset;

                // For soft particles
                output.projectedPosition = ComputeScreenPos(output.positionCS);

                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                // Sample noise for distortion
                half noise = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, input.noiseUV).r;

                // Apply distortion to UV
                float2 distortedUV = input.uv;
                distortedUV.x += (noise - 0.5) * _DistortionStrength;
                distortedUV.y += (noise - 0.5) * _DistortionStrength * 0.5;

                // Sample main texture
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, distortedUV);

                // Combine colors
                half4 finalColor = texColor * input.color * _Color * _Intensity;

                // Soft particles (fade near surfaces) - optional for WebGL
                #if defined(SOFTPARTICLES_ON)
                float2 screenUV = input.projectedPosition.xy / input.projectedPosition.w;
                float sceneDepth = LinearEyeDepth(
                    SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, screenUV).r,
                    _ZBufferParams
                );
                float particleDepth = input.projectedPosition.w;
                float fade = saturate(_SoftParticles * (sceneDepth - particleDepth));
                finalColor.a *= fade;
                #endif

                // Pre-multiply alpha for additive blending
                finalColor.rgb *= finalColor.a;

                return finalColor;
            }
            ENDHLSL
        }
    }

    Fallback "Particles/Standard Unlit"
}
