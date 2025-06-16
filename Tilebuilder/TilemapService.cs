namespace Tilebuilder;

public class TilemapService : ITilemapService
{
	private static TilemapService? instance = null;

	public List<List<int>> Tilemap { get; set; } = new();

	public TilemapService()
	{
		for (int x = 0; x < Constants.gridWidth; x++)
		{
			List<int> row = new();

			for (int y = 0; y < Constants.gridHeight; y++)
			{
				row.Add(0);
			}

			Tilemap.Add(row);
		}
	}

	public static TilemapService GetInstance()
	{
		if (instance is null)
			instance = new TilemapService();

		return instance;
	}
}