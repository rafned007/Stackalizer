using Sandbox;
using System;

public sealed class Camshake : Component
{
	
	[Property] private float intensity {get;set;}
	[Property] private float speed {get;set;}
	
	Rotation lerpedRotation = Rotation.Identity;
	float Bob = 0;
	float LerpBobSpeed = 0;

	public void ViewBob()
	{
		var bobSpeed = speed;


		LerpBobSpeed = LerpBobSpeed.LerpTo( bobSpeed, Time.Delta * 10f );

		Bob += Time.Delta * 10.0f * LerpBobSpeed;
		var yaw = MathF.Sin( Bob ) * (intensity/10);
		var pitch = MathF.Cos( -Bob * 2f ) * (intensity/10);

		GameObject.WorldRotation *= Rotation.FromYaw( -yaw * LerpBobSpeed );
		GameObject.WorldRotation *= Rotation.FromPitch( -pitch * LerpBobSpeed * 0.5f );
	}

	protected override void OnUpdate()
	{
		
	}
}
