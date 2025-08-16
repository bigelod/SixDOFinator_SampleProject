Shader "Unlit/OpenXRFrameData"
{
    Properties {
        _Color ("Color", Color) = (1,1,1)
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        //Lighting Off
        //ZTest Always
        //Cull Back
        ZWrite Off
        Lighting Off
        Fog { Mode Off }

        Color [_Color]

        Pass {}
    } 
}
