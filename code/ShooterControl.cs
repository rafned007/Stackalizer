using Sandbox;

public sealed class ShooterControl : Component
{
	[Property] public SoundEvent ShotSound { get; set; }
	[Property] public GameObject explosion { get; set; }
	[Property] public Mover playerSpeed { get; set; }
	[Property] public Blob1Controller blorble {get; set;}
	[Property] public SoundEvent HitSound { get; set; }
	[Property] public SoundEvent HitSound1 { get; set; }
	[Property] public float HitRadius { get; set; } = 5f;

	protected override void OnStart()
	{
		Mouse.Visible = true;
		Mouse.CursorType = "none";
	}

	protected override void OnUpdate()
	{
		if (Input.Pressed("attack1") )
		{
			var fireRay = Scene.Camera.ScreenPixelToRay( Mouse.Position );
			Fire( fireRay );
		}
	
	}

	public void Fire( Ray fireRay )
	{
		if ( ShotSound is not null )
		{
			Sound.Play( ShotSound, Scene.Camera.Transform.Position );
		}

		var tr = Scene.Trace
			.Ray( fireRay, 5000f )
			.Radius( HitRadius )
			// .WithTag( "shootable" )
			.Run();
		if ( tr.Hit && tr.GameObject.Tags.Has("shootable") )
		{
			var hitpos = tr.HitPosition;
			var clone = explosion.Clone();
			clone.Transform.Position = hitpos;
			tr.GameObject.Destroy();
			Sound.Play( HitSound, tr.HitPosition);
			Sound.Play( HitSound1, tr.HitPosition);
			if (blorble.upgrade && tr.GameObject.Tags.Has("blorble") )
			{
				playerSpeed.TimetillMove *= 2f;
			}
			else if(!blorble.upgrade && tr.GameObject.Tags.Has("blorble"))
			{
				playerSpeed.TimetillMove *= .5f;
			}
			else if (!tr.GameObject.Tags.Has("blorble"))
			{
				playerSpeed.TimetillMove = .02f;	
			} 
		}
	}
}
