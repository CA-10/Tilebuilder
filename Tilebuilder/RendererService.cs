using Raylib_CsLo;
using System.Numerics;

namespace Tilebuilder;

public class RendererService : IRendererService
{
	private ITilemapService _tilemapService;

	private static RendererService? instance = null;
	private Camera2D camera;

	public RendererService(ITilemapService tilemapService)
	{
		camera = new Camera2D();
		camera.zoom = 1.0f;
		_tilemapService = tilemapService;
	}

	public static RendererService GetInstance(ITilemapService tilemapService)
	{
		if (instance is null)
			instance = new RendererService(tilemapService);

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
		Vector2 topLeft = Raylib.GetScreenToWorld2D(new Vector2(0, 0), camera);
		Vector2 bottomRight = Raylib.GetScreenToWorld2D(new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), camera);

		int startX = Math.Max(0, (int)Math.Floor(topLeft.X / Constants.CellSize));
		int endX = Math.Min(Constants.gridWidth - 1, (int)Math.Floor(bottomRight.X / Constants.CellSize));

		int startY = Math.Max(0, (int)Math.Floor(topLeft.Y / Constants.CellSize));
		int endY = Math.Min(Constants.gridHeight - 1, (int)Math.Floor(bottomRight.Y / Constants.CellSize));

		//Uses occlusion culling
		for (int x = startX; x <= endX; x++)
		{
			for (int y = startY; y <= endY; y++)
			{
				Color color = (_tilemapService.Tilemap[x][y] == 1) ? Raylib.RED : Raylib.BLACK;

				int worldX = x * Constants.CellSize;
				int worldY = y * Constants.CellSize;

				Raylib.DrawRectangleLines(worldX, worldY, Constants.CellSize, Constants.CellSize, color);
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

		float wheel = Raylib.GetMouseWheelMove();

		if (wheel != 0)
		{
			Vector2 mouseWorldPos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera);
			camera.offset = Raylib.GetMousePosition();
			camera.target = mouseWorldPos;

			float scale = 0.2f * wheel;
			camera.zoom = RayMath.Clamp((float)Math.Exp(Math.Log(camera.zoom) + scale), 0.125f, 40.0f);
		}
	}

	public Camera2D Camera
	{
		get => camera;
	}
}