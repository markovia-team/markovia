Shader "Custom/TerrainShader"
{
    Properties
    {

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        
        float minHeight; 
        float maxHeight;
        int q_colors; 

        float3 floorColor = float3(155/255, 195/255, 87/255);
        float3 ceilColor = float3(92/255, 73/255, 39/255);

        float3 ascendingColors[2];

        struct Input
        {
            float3 worldPos; 
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float inverseLerp (float a, float b, float c) {
            return saturate((c-a)/(b-a)); 
        }
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            
            float heightPercent = inverseLerp(minHeight, maxHeight, IN.worldPos.y);
            int colorLevel = heightPercent * (q_colors - 2);
            heightPercent = (heightPercent - (colorLevel)*1.0f/(q_colors-1))*(q_colors-1); 
            o.Albedo = ascendingColors[colorLevel] + heightPercent * (ascendingColors[colorLevel+1] - ascendingColors[colorLevel]); 
        }
        ENDCG
    }
    FallBack "Diffuse"
}
