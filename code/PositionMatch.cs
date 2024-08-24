using Sandbox;

public sealed class PositionMatch : Component
{
	[Property] public GameObject blob1 {get;set;}

	protected override void OnUpdate()
	{
		Transform.Position = blob1.Transform.Position;
	}
}
