using Sandbox;

public sealed class Destroydelay : Component
{
	protected override void OnStart()
	{
		destroyer();
	}

	async void destroyer()
	{
		await Task.DelaySeconds(5);
		GameObject.Destroy();
	}
}
