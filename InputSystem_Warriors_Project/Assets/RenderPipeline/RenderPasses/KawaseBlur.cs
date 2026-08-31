using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
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
        static readonly int offsetId = Shader.PropertyToID("_offset");

        public Material blurMaterial;
        public int passes;
        public int downsample;
        public bool copyToFramebuffer;
        public string targetName;

        // One property block per blit. The render graph executes every pass after all of them have
        // been recorded, so a single shared block would hand each pass the last _offset written.
        readonly List<MaterialPropertyBlock> propertyBlocks = new List<MaterialPropertyBlock>();
        int nextPropertyBlock;

        string cachedTargetName;
        int cachedTargetNameId;

        public CustomRenderPass(string profilerTag)
        {
            profilingSampler = new ProfilingSampler(profilerTag);

            // The first blit samples the camera colour, which cannot be read back when the camera
            // renders straight into the back buffer.
            requiresIntermediateTexture = true;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            if (blurMaterial == null)
                return;

            var resourceData = frameData.Get<UniversalResourceData>();
            if (resourceData.isActiveTargetBackBuffer)
                return;

            var cameraData = frameData.Get<UniversalCameraData>();

            var descriptor = cameraData.cameraTargetDescriptor;
            descriptor.depthBufferBits = 0;
            descriptor.msaaSamples = 1;
            descriptor.bindMS = false;
            descriptor.useMipMap = false;
            descriptor.autoGenerateMips = false;
            descriptor.width = Mathf.Max(1, descriptor.width / downsample);
            descriptor.height = Mathf.Max(1, descriptor.height / downsample);

            var cameraColor = resourceData.activeColorTexture;
            var blurA = UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "_KawaseBlurA", false, FilterMode.Bilinear);
            var blurB = UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "_KawaseBlurB", false, FilterMode.Bilinear);

            nextPropertyBlock = 0;

            // first pass
            AddBlurPass(renderGraph, cameraColor, blurA, 1.5f, "KawaseBlur Downsample");

            for (int i = 1; i < passes - 1; i++)
            {
                AddBlurPass(renderGraph, blurA, blurB, 0.5f + i, "KawaseBlur Iteration");

                // ping-pong
                (blurA, blurB) = (blurB, blurA);
            }

            // final pass
            var finalOffset = 0.5f + passes - 1f;
            if (copyToFramebuffer)
            {
                AddBlurPass(renderGraph, blurA, cameraColor, finalOffset, "KawaseBlur To Camera");
            }
            else
            {
                using (var builder = AddBlurPass(renderGraph, blurA, blurB, finalOffset, "KawaseBlur Final", true))
                    builder.SetGlobalTextureAfterPass(blurB, GetTargetNameId());
            }
        }

        IBaseRenderGraphBuilder AddBlurPass(RenderGraph renderGraph, TextureHandle source, TextureHandle destination,
            float offset, string passName, bool returnBuilder = false)
        {
            var propertyBlock = GetPropertyBlock();
            propertyBlock.SetFloat(offsetId, offset);

            var parameters = new RenderGraphUtils.BlitMaterialParameters(
                source, destination, blurMaterial, 0, propertyBlock,
                RenderGraphUtils.FullScreenGeometryType.ProceduralTriangle);

            return renderGraph.AddBlitPass(parameters, passName, returnBuilder);
        }

        MaterialPropertyBlock GetPropertyBlock()
        {
            if (nextPropertyBlock == propertyBlocks.Count)
                propertyBlocks.Add(new MaterialPropertyBlock());

            return propertyBlocks[nextPropertyBlock++];
        }

        int GetTargetNameId()
        {
            if (cachedTargetName != targetName)
            {
                cachedTargetName = targetName;
                cachedTargetNameId = Shader.PropertyToID(targetName);
            }

            return cachedTargetNameId;
        }
    }

    CustomRenderPass scriptablePass;

    public override void Create()
    {
        scriptablePass = new CustomRenderPass("KawaseBlur");
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (settings.blurMaterial == null)
            return;

        scriptablePass.blurMaterial = settings.blurMaterial;
        scriptablePass.passes = settings.blurPasses;
        scriptablePass.downsample = settings.downsample;
        scriptablePass.copyToFramebuffer = settings.copyToFramebuffer;
        scriptablePass.targetName = settings.targetName;
        scriptablePass.renderPassEvent = settings.renderPassEvent;

        renderer.EnqueuePass(scriptablePass);
    }
}
