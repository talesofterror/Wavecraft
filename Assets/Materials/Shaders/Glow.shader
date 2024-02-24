Shader "Unlit/Glow"
{
    Properties
    {
        _ColorA("Color A", Color) = (1,0,0,1)
        _ColorB("Color B", Color) = (0,1,1,1)
        _Period("Period", Float) = 1
        _Frequency("Frequency", Float) = 2
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _ColorA;
            fixed4 _ColorB;
            float _Period;
            float _Frequency;

            fixed4 frag (v2f_img i) : SV_Target
            {
                float delta = (sin(_Time.y / _Period * _Frequency) + 1)/2;
                fixed3 color = lerp(_ColorA, _ColorB, delta);
                return fixed4(color, 1);
            }
            ENDCG
        }
    }
}
