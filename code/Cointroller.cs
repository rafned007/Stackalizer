using Sandbox;
using System;

public sealed class Cointroller : Component
{
	[Property] public Rigidbody rb {get; set;}

	protected override void OnStart()
	{
		Random rnd = new Random();
        var rndSpeed = rnd.Next(450, 650);
		rb.Velocity = Vector3.Down * rndSpeed;
		Log.Info(rndSpeed);
	}
}
