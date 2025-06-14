using Raylib_CsLo;

namespace Tilebuilder;

public interface IRendererService
{
	public void RenderCamera();
	public void RenderUI();
	public Camera2D Camera { get; }
}