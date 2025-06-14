using Raylib_CsLo;

namespace Tilebuilder;

public class Program
{
    private static IRendererService? _renderer;
    private static IUpdateService? _updateService;

    public static void Main(string[] args)
    {
        _renderer = RendererService.GetInstance();
        _updateService = UpdateService.GetInstance();

        Raylib.InitWindow(1800, 960, "Tilebuilder");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.RAYWHITE);
            Raylib.BeginMode2D(_renderer.Camera);

            _renderer.RenderCamera();
            _updateService.Update();

            Raylib.EndMode2D();

            _renderer.RenderUI();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}