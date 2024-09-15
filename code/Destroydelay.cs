using Sandbox;

public sealed class Destroydelay : Component
{
	[Property] public float timeTillDestroy {get; set;}

	protected override void OnStart()
	{
		destroyer();
	}

	async void destroyer()
	{
		await Task.DelaySeconds(timeTillDestroy);
		GameObject.Destroy();
	}
}
