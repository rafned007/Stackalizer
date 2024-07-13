using Sandbox;

public sealed class GayManager : Component
{
	[Property] public GameObject prevob {get; set;}
	[Property] public GameObject SpawnPoint {get; set;}
	[Property] public GameObject Border {get; set;}
	[Property] public GameObject Camera {get; set;}
	public int turn = 1;

	protected override void OnUpdate()
	{
		if (Input.Pressed("jump"))
		{
			Log.Info("log.ligmaballs");
			var cloner = prevob.Clone();
			var direction = Vector3.Up;
			
			// cloner.Transform.Position += (direction * (52 * turn)) ;
			cloner.Transform.Position = Vector3.Right * (52*6) + (direction * (52 * turn));
			Border.Transform.Position = direction * (52*turn);
			turn += 1;
			// Border.Transform.Position += Vector3.Up * (52*turn);
			if (turn == 11)
			{ 
				Camera.Transform.Position += direction * (52*turn);
			}
			if (turn == 22)
			{ 
				Camera.Transform.Position += direction * (52*turn)/2;
			}
		}
		
			
	}
}

