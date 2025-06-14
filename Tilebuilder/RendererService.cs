using Raylib_CsLo;
using System.Numerics;

namespace Tilebuilder;

public class RendererService : IRendererService
{
	private static RendererService? instance = null;
	private Camera2D camera;

	public RendererService()
	{
		camera = new Camera2D();
		camera.zoom = 1.0f;
	}

	public static RendererService GetInstance()
	{
		if (instance is null)
			instance = new RendererService();

		return instance;
	}

	public void RenderCamera()
	{
		RenderGrid();

		UpdateCamera();
	}

	public void RenderUI()
	{

	}

	private void RenderGrid()
	{
		for (int x = 0; x < 1000; x += Constants.CellSize)
		{
			for (int y = 0; y < 1000; y+= Constants.CellSize)
			{
				Raylib.DrawRectangleLines(x, y, Constants.CellSize, Constants.CellSize, Raylib.BLACK);
			}
		}
	}

	private void UpdateCamera()
	{
		if (Raylib.IsMouseButtonDown(Raylib.MOUSE_RIGHT_BUTTON))
		{
			Vector2 delta = Raylib.GetMouseDelta();
			delta = RayMath.Vector2Scale(delta, -1.0f / camera.zoom);
			camera.target = RayMath.Vector2Add(camera.target, delta);
		}
	}

	public Camera2D Camera
	{
		get => camera;
	}
}
