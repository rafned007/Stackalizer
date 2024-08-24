using Sandbox;

public sealed class Cointroller : Component
{
	[Property] public Rigidbody rb {get; set;}
	[Property] public float speed {get; set;}

	protected override void OnStart()
	{
		rb.Velocity = Vector3.Down * speed;
	}
}
