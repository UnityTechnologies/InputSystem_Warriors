using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class KawaseBlur : ScriptableRendererFeature
{
    [System.Serializable]
    public class KawaseBlurSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        public Material blurMaterial = null;

        [Range(2,15)]
        public int blurPasses = 1;

        [Range(1,4)]
        public int downsample = 1;
        public bool copyToFramebuffer;
        public string targetName = "_blurTexture";
    }

    public KawaseBlurSettings settings = new KawaseBlurSettings();

    class CustomRenderPass : ScriptableRenderPass
    {
        public Material blurMaterial;
        public int passes;
        public int downsample;
        public bool copyToFramebuffer;
        public string targetName;        
        string profilerTag;

        private RTHandle rtHandle1;
        private RTHandle rtHandle2;

        private RTHandle source;

        public void Setup(RTHandle source)
        {
            this.source = source;
        }

        public CustomRenderPass(string profilerTag)
        {
            this.profilerTag = profilerTag;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            // Allocate temporary RTHandles based on camera size and downsample
            var width = cameraTextureDescriptor.width / downsample;
            var height = cameraTextureDescriptor.height / downsample;

            rtHandle1 = RTHandles.Alloc(
                width, height,
                depthBufferBits: DepthBits.None,
                colorFormat: UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm,
                name: "tmpBlurRT1");

            rtHandle2 = RTHandles.Alloc(
                width, height,
                depthBufferBits: DepthBits.None,
                colorFormat: UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm,
                name: "tmpBlurRT2");

            ConfigureTarget(rtHandle1);
            ConfigureClear(ClearFlag.None, Color.black);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

            // first pass
            cmd.SetGlobalFloat("_offset", 1.5f);
            cmd.Blit(source, rtHandle1, blurMaterial);

            for (int i = 1; i < passes - 1; i++)
            {
                cmd.SetGlobalFloat("_offset", 0.5f + i);
                cmd.Blit(rtHandle1, rtHandle2, blurMaterial);

                // ping-pong
                var tmp = rtHandle1;
                rtHandle1 = rtHandle2;
                rtHandle2 = tmp;
            }

            // final pass
            cmd.SetGlobalFloat("_offset", 0.5f + passes - 1f);
            if (copyToFramebuffer)
            {
                cmd.Blit(rtHandle1, source, blurMaterial);
            }
            else
            {
                cmd.Blit(rtHandle1, rtHandle2, blurMaterial);
                cmd.SetGlobalTexture(targetName, rtHandle2);
            }

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (rtHandle1 != null) { rtHandle1.Release(); rtHandle1 = null; }
            if (rtHandle2 != null) { rtHandle2.Release(); rtHandle2 = null; }
        }
    }

    CustomRenderPass scriptablePass;

    public override void Create()
    {
        scriptablePass = new CustomRenderPass("KawaseBlur")
        {
            blurMaterial = settings.blurMaterial,
            passes = settings.blurPasses,
            downsample = settings.downsample,
            copyToFramebuffer = settings.copyToFramebuffer,
            targetName = settings.targetName,
            renderPassEvent = settings.renderPassEvent
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        scriptablePass.Setup(renderer.cameraColorTargetHandle);
        renderer.EnqueuePass(scriptablePass);
    }
}


