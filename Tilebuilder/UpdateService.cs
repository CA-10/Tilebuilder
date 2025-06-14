namespace Tilebuilder;

public class UpdateService : IUpdateService
{
	private static UpdateService? instance = null;

	public UpdateService()
	{

	}

	public static UpdateService GetInstance()
	{
		if (instance is null)
			instance = new UpdateService();

		return instance;
	}

	public void Update()
	{

	}
}