Shader "Graph/GraphColorShader"
{

    SubShader
    {
        CGPROGRAM
        #pragma surface ConfigureSurface Standard fullforwardshadows
        #pragma target 3.0


        struct Input {
            float3 worldPos;
        };

        void ConfigureSurface(Input input, inout  SurfaceOutputStandard _surface)
        {
            _surface.Albedo.rg = input.worldPos.xy * 0.5 + 0.5;
        }
        ENDCG
    }
        FallBack "Diffuse"
}