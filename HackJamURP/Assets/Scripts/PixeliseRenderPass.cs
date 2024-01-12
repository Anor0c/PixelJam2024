using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixeliseRenderPass : ScriptableRenderPass
{
    private readonly int pixelBufferID = Shader.PropertyToID("_PixelBuffer");
    private int pixelScreenHeight, pixelScreenWidth; 

    private readonly PixeliseRendererFeature.CustomPassSettings settings;
    private Material mat; 

    private RenderTargetIdentifier colorBuffer, pixelBuffer; 

    public PixeliseRenderPass(PixeliseRendererFeature.CustomPassSettings _settings)
    {
        this.settings = _settings;
        this.renderPassEvent = _settings.renderPassEvent; 
        if (mat == null)
        {
           //mat = CoreUtils.CreateEngineMaterial("Universal Render Pipeline/Pixelize"); 
            mat = Resources.Load<Material>("PixelMat");
        }
    }
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        colorBuffer = renderingData.cameraData.renderer.cameraColorTargetHandle;
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;


        pixelScreenHeight = settings.screenHeight;
        pixelScreenWidth = (int)(pixelScreenHeight * renderingData.cameraData.camera.aspect + 0.5f);

        mat.SetVector("_BlockCount", new Vector2(pixelScreenWidth, pixelScreenHeight));
        mat.SetVector("_BlockSize", new Vector2(1.0f / pixelScreenWidth, 1.0f / pixelScreenHeight));
        mat.SetVector("_HalfBlockSize", new Vector2(0.5f / pixelScreenWidth, 0.5f / pixelScreenHeight));

        descriptor.height = pixelScreenHeight;
        descriptor.width = pixelScreenWidth;

        cmd.GetTemporaryRT(pixelBufferID, descriptor, FilterMode.Point);
        pixelBuffer = new RenderTargetIdentifier(pixelBufferID);
    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get();
        using (new ProfilingScope(cmd, new ProfilingSampler("Pixelize Render Pass")))
        {
            Blit(cmd, colorBuffer, pixelBuffer, mat);
            Blit(cmd, pixelBuffer, colorBuffer);
        }

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        if (cmd == null) throw new System.ArgumentNullException("cmd is null");
        cmd.ReleaseTemporaryRT(pixelBufferID);
    }
}
