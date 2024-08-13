using Sandbox;
using SpriteTools;

public sealed class Blob1Controller : Component
{
	
	[RequireComponent] SpriteComponent badSprite { get; set; }
	public bool upgrade;

	protected override void OnUpdate()
	{
		badSprite.OnBroadcastEvent = OnBroadcastEvent;
	}

	
	void OnBroadcastEvent(string name)
	{
		if (name == "Shoot")
		{
			upgrade = true;
		}
		else if (name == "BadShoot")
		{
			upgrade = false;
		}
	}
}
