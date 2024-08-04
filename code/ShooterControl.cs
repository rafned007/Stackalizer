using Sandbox;

public sealed class ShooterControl : Component
{
	[Property] public SoundEvent ShotSound { get; set; }
	[Property] public GameObject explosion { get; set; }
	[Property] public Mover playerSpeed { get; set; }
	[Property] public GameObject blob1 { get; set; }
	[Property] public float HitRadius { get; set; } = 5f;


	protected override void OnStart()
	{
		Mouse.Visible = true;
	}

	protected override void OnUpdate()
	{
		if (Input.Pressed("attack1") )
		{
			var fireRay = Scene.Camera.ScreenPixelToRay( Mouse.Position );
			Fire( fireRay );
		}
	}

	public void Fire( Ray aimRay )
	{
		if ( ShotSound is not null )
		{
			Sound.Play( ShotSound, Scene.Camera.Transform.Position );
		}
		var tr = Scene.Trace
			.Ray( aimRay, 5000f )
			.Radius( HitRadius )
			.WithTag( "shootable" )
			.Run();
		if ( tr.Hit )
		{
			var clone = explosion.Clone();
			clone.Transform.Position = blob1.Transform.Position;
			blob1.Destroy();
			playerSpeed.TimetillMove = .02f; 
		}
	}
}
