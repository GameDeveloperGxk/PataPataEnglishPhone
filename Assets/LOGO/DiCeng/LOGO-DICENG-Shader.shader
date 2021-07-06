// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3688,x:32719,y:32712,varname:node_3688,prsc:2|emission-2232-OUT,alpha-5370-OUT;n:type:ShaderForge.SFN_Tex2d,id:8987,x:32227,y:32582,ptovrint:False,ptlb:tex,ptin:_tex,varname:node_8987,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8786,x:32339,y:33072,ptovrint:False,ptlb:alpha,ptin:_alpha,varname:_tex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8324-OUT;n:type:ShaderForge.SFN_OneMinus,id:5370,x:32499,y:33111,varname:node_5370,prsc:2|IN-8786-R;n:type:ShaderForge.SFN_Add,id:8324,x:32134,y:33089,varname:node_8324,prsc:2|A-3892-OUT,B-1534-OUT;n:type:ShaderForge.SFN_TexCoord,id:5943,x:31152,y:32910,varname:node_5943,prsc:2,uv:0;n:type:ShaderForge.SFN_Divide,id:8289,x:31351,y:33002,varname:node_8289,prsc:2|A-5943-UVOUT,B-2304-OUT;n:type:ShaderForge.SFN_Add,id:1578,x:31528,y:33114,varname:node_1578,prsc:2|A-8289-OUT,B-1570-OUT;n:type:ShaderForge.SFN_Vector1,id:1570,x:31365,y:33162,varname:node_1570,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Subtract,id:3892,x:31713,y:33204,varname:node_3892,prsc:2|A-1578-OUT,B-4804-OUT;n:type:ShaderForge.SFN_Divide,id:4804,x:31528,y:33284,varname:node_4804,prsc:2|A-1570-OUT,B-2304-OUT;n:type:ShaderForge.SFN_Slider,id:6079,x:31861,y:33310,ptovrint:False,ptlb:offset_x,ptin:_offset_x,varname:node_6079,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0,max:2;n:type:ShaderForge.SFN_Slider,id:2843,x:31825,y:33424,ptovrint:False,ptlb:offset_y,ptin:_offset_y,varname:_offset_y,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0,max:2;n:type:ShaderForge.SFN_Append,id:1534,x:32223,y:33311,varname:node_1534,prsc:2|A-6079-OUT,B-2843-OUT;n:type:ShaderForge.SFN_Multiply,id:2232,x:32487,y:32788,varname:node_2232,prsc:2|A-8987-RGB,B-7929-RGB;n:type:ShaderForge.SFN_Color,id:7929,x:32298,y:32838,ptovrint:False,ptlb:color,ptin:_color,varname:node_7929,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:2304,x:30956,y:33141,ptovrint:False,ptlb:scale,ptin:_scale,varname:_offset_z,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3505844,max:1.5;proporder:8987-7929-8786-2304-6079-2843;pass:END;sub:END;*/

Shader "Unlit/NewUnlitShader" {
    Properties {
        _tex ("tex", 2D) = "white" {}
        _color ("color", Color) = (1,1,1,1)
        _alpha ("alpha", 2D) = "white" {}
        _scale ("scale", Range(0, 1.5)) = 0.3505844
        _offset_x ("offset_x", Range(-2, 2)) = 0
        _offset_y ("offset_y", Range(-2, 2)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _tex; uniform float4 _tex_ST;
            uniform sampler2D _alpha; uniform float4 _alpha_ST;
            uniform float _offset_x;
            uniform float _offset_y;
            uniform float4 _color;
            uniform float _scale;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _tex_var = tex2D(_tex,TRANSFORM_TEX(i.uv0, _tex));
                float3 emissive = (_tex_var.rgb*_color.rgb);
                float3 finalColor = emissive;
                float node_1570 = 0.5;
                float2 node_8324 = ((((i.uv0/_scale)+node_1570)-(node_1570/_scale))+float2(_offset_x,_offset_y));
                float4 _alpha_var = tex2D(_alpha,TRANSFORM_TEX(node_8324, _alpha));
                fixed4 finalRGBA = fixed4(finalColor,(1.0 - _alpha_var.r));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
