using Sandbox;

public sealed class ShooterControl : Component
{
	[Property] public SoundEvent ShotSound { get; set; }
	[Property] public GameObject explosion { get; set; }
	[Property] public GameObject coinsplosionp { get; set; }
	[Property] public GameObject coinsplosionb { get; set; }
	[Property] public GameObject coinsplosionr { get; set; }
	[Property] public Mover playerSpeed { get; set; }
	[Property] public Blob1Controller blorble {get; set;}
	[Property] public SoundEvent HitSound { get; set; }
	[Property] public SoundEvent HitSound1 { get; set; }
	[Property] public SoundEvent badcoin { get; set; }
	[Property] public SoundEvent goodcoin { get; set; }
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
			Sound.Play( ShotSound, Scene.Camera.WorldPosition );
		}

		var tr = Scene.Trace
			.Ray( fireRay, 5000f )
			.Radius( HitRadius )
			// .WithTag( "shootable" )
			.Run();
		if ( tr.Hit && tr.GameObject.Tags.Has("shootable") )
		{
			var hitpos = tr.HitPosition;

			if (blorble.upgrade && tr.GameObject.Tags.Has("blorble") )
			{
				var clone = explosion.Clone();
				clone.WorldPosition = hitpos;
				tr.GameObject.Destroy();
				playerSpeed.TimetillMove *= 2f;
				Sound.Play( HitSound, hitpos);
				Sound.Play( HitSound1, hitpos);
			}
			else if(!blorble.upgrade && tr.GameObject.Tags.Has("blorble"))
			{
				var clone = explosion.Clone();
				clone.WorldPosition = hitpos;
				tr.GameObject.Destroy();
				playerSpeed.TimetillMove *= .5f;
				Sound.Play( HitSound, hitpos);
				Sound.Play( HitSound1, hitpos);
			}
			// else if (!tr.GameObject.Tags.Has("blorble"))
			// {
			// 	playerSpeed.TimetillMove = .02f;	
			// }

			if (tr.GameObject.Tags.Has("redcoin"))
			{
				var clone = coinsplosionr.Clone();
				clone.WorldPosition = hitpos;
				tr.GameObject.Destroy();
				playerSpeed.TimetillMove *= .5f;
				Sound.Play( badcoin);
			}
			else if (tr.GameObject.Tags.Has("bluecoin"))
			{
				var clone = coinsplosionb.Clone();
				clone.WorldPosition = hitpos;
				tr.GameObject.Destroy();
				playerSpeed.TimetillMove *= 1.2f;
				Sound.Play( goodcoin);
			}
			else if (tr.GameObject.Tags.Has("pinkcoin"))
			{
				var clone = coinsplosionp.Clone();
				clone.WorldPosition = hitpos;
				tr.GameObject.Destroy();
				playerSpeed.TimetillMove *= 1.5f;
				Sound.Play( goodcoin);
			}
		}
	}
}
