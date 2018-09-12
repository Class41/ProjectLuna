using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace UnityEngine.Experimental.Rendering.LightweightPipeline
{
    public class OpaquePostProcessPass : ScriptableRenderPass
    {
        private RenderTargetHandle colorAttachmentHandle { get; set; }
        private RenderTextureDescriptor descriptor { get; set; }
        private PostProcessRenderContext postContext { get; set; }

        public void Setup(
            PostProcessRenderContext postProcessRenderContext,
            RenderTextureDescriptor baseDescriptor,
            RenderTargetHandle colorAttachmentHandle)
        {
            this.colorAttachmentHandle = colorAttachmentHandle;
            this.postContext = postProcessRenderContext;
            descriptor = baseDescriptor;
        }

        public override void Execute(ScriptableRenderer renderer, ref ScriptableRenderContext context,
            ref CullResults cullResults,
            ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("Render Opaque PostProcess Effects");

            RenderTargetIdentifier source = colorAttachmentHandle.Identifier();
            LightweightPipeline.RenderPostProcess(cmd, postContext, ref renderingData.cameraData, descriptor.colorFormat, source, colorAttachmentHandle.Identifier(), true);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
}
