using Sandbox;
using System;

public sealed class Cointroller : Component
{
	[Property] public Rigidbody rb {get; set;}
	[Property] public float speed {get; set;}

	protected override void OnStart()
	{
		Random rnd = new Random();
        var rndSpeed = rnd.Next(500, 700);
		rb.Velocity = Vector3.Down * rndSpeed;
	}
}
