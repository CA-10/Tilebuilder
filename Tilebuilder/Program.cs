using Raylib_CsLo;

namespace Tilebuilder;

public class Program
{
    private static IRendererService? _renderer;
    private static IUpdateService? _updateService;
    private static ITilemapService? _tilemapService;

    public static void Main(string[] args)
    {
		_tilemapService = TilemapService.GetInstance();

		_renderer = RendererService.GetInstance(_tilemapService);
        _updateService = UpdateService.GetInstance(_renderer, _tilemapService);

        Raylib.InitWindow(1800, 960, "Tilebuilder");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.RAYWHITE);
            Raylib.BeginMode2D(_renderer.Camera);

			_updateService.Update();
			_renderer.RenderCamera();

            Raylib.EndMode2D();

            _renderer.RenderUI();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}