Shader "Custom/DrippingSlime"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1)
        _SlimeColor ("Slime Color", Color) = (0, 1, 0, 1)
        _DripSpeed ("Drip Speed", Float) = 1.0
        _DripAmount ("Drip Amount", Float) = 0.5
        _NoiseScale ("Noise Scale", Float) = 0.5
        _NoiseIntensity ("Noise Intensity", Float) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        fixed4 _BaseColor;
        fixed4 _SlimeColor;
        float _DripSpeed;
        float _DripAmount;
        float _NoiseScale;
        float _NoiseIntensity;

        struct Input
        {
            float3 worldPos;
            float2 uv : TEXCOORD0;
        };

        half PerlinNoise (float2 uv, float scale, float intensity)
        {
          float2 i = floor(uv * scale);
          float2 f = fract(uv * scale);
          f = f * f * (3.0 - 2.0 * f);
          float a = smoothstep(0.0, 1.0, dot(f, hash(i)));
          float b = smoothstep(0.0, 1.0, dot(f - float2(1.0, 0.0), hash(i + float2(1.0, 0.0))));
          float c = smoothstep(0.0, 1.0, dot(f - float2(0.0, 1.0), hash(i + float2(0.0, 1.0))));
          float d = smoothstep(0.0, 1.0, dot(f - float2(1.0, 1.0), hash(i + float2(1.0, 1.0))));
          return mix(mix(a, b, f.x), mix(c, d, f.x), f.y) * intensity;
        }

        half4 _Main (Input IN) : SV_Target
        {
            float mask = 1.0 - smoothstep(0.0, _DripAmount, IN.worldPos.y);
            float3 slimeColor = lerp(_BaseColor.rgb, _SlimeColor.rgb, mask);

            float noise = PerlinNoise (IN.worldPos * _NoiseScale, _NoiseIntensity);
            float yDisplacement = noise * mask;

            float3 finalPos = IN.worldPos + float3(0, yDisplacement, 0);

            half4 finalColor = half4(slimeColor, 1.0);

            return surf (finalPos, finalColor);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
