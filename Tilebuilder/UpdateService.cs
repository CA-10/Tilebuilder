using Raylib_CsLo;
using System.Numerics;

namespace Tilebuilder;

public class UpdateService : IUpdateService
{
	private static UpdateService? instance = null;
	private IRendererService _rendererService;
	private ITilemapService _tilemapService;

	public UpdateService(IRendererService rendererService, ITilemapService tilemapService)
	{
		_rendererService = rendererService;
		_tilemapService = tilemapService;
	}

	public static UpdateService GetInstance(IRendererService rendererService, ITilemapService tilemapService)
	{
		if (instance is null)
			instance = new UpdateService(rendererService, tilemapService);

		return instance;
	}

	public void Update()
	{
		if (Raylib.IsMouseButtonPressed(0))
		{
			Vector2 mouseWorld = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _rendererService.Camera);

			int gridX = (int)Math.Floor(mouseWorld.X / Constants.CellSize);
			int gridY = (int)Math.Floor(mouseWorld.Y / Constants.CellSize);

			if (gridX >= 0 && gridX < _tilemapService.Tilemap[0].Count())
			{
				_tilemapService.Tilemap[gridX][gridY] = 1;
			}

			Console.WriteLine($"{gridX}, {gridY}");
		}
	}
}