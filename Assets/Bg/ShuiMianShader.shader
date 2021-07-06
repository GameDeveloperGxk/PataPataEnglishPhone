// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4589,x:32981,y:32462,varname:node_4589,prsc:2|diff-8533-OUT,alpha-2938-OUT;n:type:ShaderForge.SFN_Tex2d,id:5177,x:32351,y:32642,ptovrint:False,ptlb:main,ptin:_main,varname:node_5177,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9354-OUT;n:type:ShaderForge.SFN_Add,id:9354,x:32176,y:32624,varname:node_9354,prsc:2|A-5322-UVOUT,B-6493-OUT;n:type:ShaderForge.SFN_TexCoord,id:5322,x:31880,y:32503,varname:node_5322,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:8462,x:31604,y:32691,ptovrint:False,ptlb:noisetex,ptin:_noisetex,varname:node_8462,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9994-OUT;n:type:ShaderForge.SFN_Multiply,id:6493,x:31962,y:32798,varname:node_6493,prsc:2|A-3008-OUT,B-1974-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1974,x:31774,y:32989,ptovrint:False,ptlb:noise intensity,ptin:_noiseintensity,varname:node_1974,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Panner,id:6345,x:31098,y:32671,varname:node_6345,prsc:2,spu:1,spv:0|UVIN-5493-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:5493,x:30870,y:32671,varname:node_5493,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:8285,x:30870,y:32855,varname:node_8285,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:2078,x:30839,y:33052,ptovrint:False,ptlb:x,ptin:_x,varname:node_2078,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:1007,x:30902,y:33137,ptovrint:False,ptlb:y,ptin:_y,varname:_vvv_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:5358,x:31444,y:32844,varname:node_5358,prsc:2|A-8245-OUT,B-7198-OUT;n:type:ShaderForge.SFN_Multiply,id:8245,x:31303,y:32654,varname:node_8245,prsc:2|A-6345-UVOUT,B-2078-OUT;n:type:ShaderForge.SFN_Multiply,id:7198,x:31328,y:32983,varname:node_7198,prsc:2|A-8526-UVOUT,B-1007-OUT;n:type:ShaderForge.SFN_ComponentMask,id:3008,x:31743,y:32761,varname:node_3008,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-8462-RGB;n:type:ShaderForge.SFN_Panner,id:8526,x:31098,y:32871,varname:node_8526,prsc:2,spu:0,spv:1|UVIN-5493-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:9994,x:31583,y:32909,varname:node_9994,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5358-OUT;n:type:ShaderForge.SFN_Add,id:8533,x:32599,y:32426,varname:node_8533,prsc:2|A-3628-OUT,B-7927-RGB;n:type:ShaderForge.SFN_Tex2d,id:7927,x:32324,y:32360,ptovrint:False,ptlb:tex,ptin:_tex,varname:_main_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3628,x:32422,y:32817,varname:node_3628,prsc:2|A-5177-RGB,B-9821-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9821,x:32234,y:33008,ptovrint:False,ptlb:texintensity,ptin:_texintensity,varname:_noiseintensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:2938,x:32298,y:33072,ptovrint:False,ptlb:opacity,ptin:_opacity,varname:_texintensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;proporder:5177-8462-1974-2078-1007-7927-9821-2938;pass:END;sub:END;*/

Shader "Unlit/noiseshader" {
    Properties {
        _main ("main", 2D) = "white" {}
        _noisetex ("noisetex", 2D) = "white" {}
        _noiseintensity ("noise intensity", Float ) = 0.1
        _x ("x", Float ) = 0
        _y ("y", Float ) = 0
        _tex ("tex", 2D) = "white" {}
        _texintensity ("texintensity", Float ) = 0.1
        _opacity ("opacity", Float ) = 0.1
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
            Cull Off
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
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _main; uniform float4 _main_ST;
            uniform sampler2D _noisetex; uniform float4 _noisetex_ST;
            uniform float _noiseintensity;
            uniform float _x;
            uniform float _y;
            uniform sampler2D _tex; uniform float4 _tex_ST;
            uniform float _texintensity;
            uniform float _opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_6623 = _Time + _TimeEditor;
                float2 node_9994 = float4(((i.uv0+node_6623.g*float2(1,0))*_x),((i.uv0+node_6623.g*float2(0,1))*_y)).rg;
                float4 _noisetex_var = tex2D(_noisetex,TRANSFORM_TEX(node_9994, _noisetex));
                float2 node_9354 = (i.uv0+(_noisetex_var.rgb.rg*_noiseintensity));
                float4 _main_var = tex2D(_main,TRANSFORM_TEX(node_9354, _main));
                float4 _tex_var = tex2D(_tex,TRANSFORM_TEX(i.uv0, _tex));
                float3 node_8533 = ((_main_var.rgb*_texintensity)+_tex_var.rgb);
                float3 diffuseColor = node_8533;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,_opacity);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _main; uniform float4 _main_ST;
            uniform sampler2D _noisetex; uniform float4 _noisetex_ST;
            uniform float _noiseintensity;
            uniform float _x;
            uniform float _y;
            uniform sampler2D _tex; uniform float4 _tex_ST;
            uniform float _texintensity;
            uniform float _opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_1552 = _Time + _TimeEditor;
                float2 node_9994 = float4(((i.uv0+node_1552.g*float2(1,0))*_x),((i.uv0+node_1552.g*float2(0,1))*_y)).rg;
                float4 _noisetex_var = tex2D(_noisetex,TRANSFORM_TEX(node_9994, _noisetex));
                float2 node_9354 = (i.uv0+(_noisetex_var.rgb.rg*_noiseintensity));
                float4 _main_var = tex2D(_main,TRANSFORM_TEX(node_9354, _main));
                float4 _tex_var = tex2D(_tex,TRANSFORM_TEX(i.uv0, _tex));
                float3 node_8533 = ((_main_var.rgb*_texintensity)+_tex_var.rgb);
                float3 diffuseColor = node_8533;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * _opacity,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
