Shader "Custom/MaskShader"
{

    SubShader
    {
        Tags
        {
            "Queue"="Transparent+1"
        }

        Pass
        {
            Blend Zero One
        }
    }
}