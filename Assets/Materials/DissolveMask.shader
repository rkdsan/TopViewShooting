Shader "Custom/DissolveMask"
{
    Properties
    {
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}
        _Dissolve ("Dissolve Value", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry-1"}

        Stencil
        {
            Ref 1
            Comp Never
            Fail Replace
        }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _DissolveTex;
        struct Input
        {
            float2 uv_DissolveTex;
        };

        fixed _Dissolve;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 dissolve = tex2D (_DissolveTex, IN.uv_DissolveTex);
            fixed alpha = dissolve.r;
            alpha = step(alpha, _Dissolve - 0.03);
            if(alpha == 1)
                discard;
            o.Alpha = alpha;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
