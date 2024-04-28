Shader "Unlit/TileShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BorderWidth ("Border Width", range(0,1)) = 0
        _TileColor ("Tile Color", Color) = (1,1,1,0)
        _BorderColor("Border Color", Color) = (0,0,0,1)
        _Alpha("_Alpha", range(0,1)) = 0
        _TopBlackBorder("Top Border", float) = 1
        _BottomBlackBorder("Bottom Border", float) = 1
        _LeftBlackBorder("Left Border", float) = 1
        _RightBlackBorder("Right Border", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BorderWidth;
            float4 _TileColor;
            float4 _BorderColor;
            float _Alpha;
            int _TopBlackBorder;
            int _BottomBlackBorder;
            int _LeftBlackBorder;
            int _RightBlackBorder;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float mask = i.uv.x > _BorderWidth;
                mask *= i.uv.x < 1 - _BorderWidth;
                mask *= i.uv.y > _BorderWidth;
                mask *= i.uv.y < 1 - _BorderWidth;

                float4 outColor = lerp(_BorderColor, float4(_TileColor.rgb, _Alpha), mask);

                return outColor;

                /*
                float borderColorMask = (i.uv.x > _BorderWidth) + _LeftBlackBorder;
                borderColorMask *= (i.uv.x < 1 - _BorderWidth) + _RightBlackBorder;
                borderColorMask *= (i.uv.y > _BorderWidth) + _BottomBlackBorder;
                borderColorMask *= (i.uv.y < 1 - _BorderWidth) + _TopBlackBorder;
                
                float4 outColor = lerp(_BorderColor, _TileColor, borderColorMask);

                float mask = (i.uv.x > _BorderWidth) + (1-_LeftBlackBorder);
                mask *= (i.uv.x < 1 - _BorderWidth) + (1-_RightBlackBorder);
                mask *= (i.uv.y > _BorderWidth) + (1-_BottomBlackBorder);
                mask *= (i.uv.y < 1 - _BorderWidth) + (1-_TopBlackBorder);

                outColor *= mask;

                return outColor;
                */
            }
            ENDCG
        }
    }
}
