using Sandbox;

public sealed class GayManager : Component
{
	[Property] public GameObject prevob {get; set;}
	[Property] public GameObject SpawnPoint {get; set;}
	public int turn = 1;

	protected override void OnUpdate()
	{
		if (Input.Pressed("jump"))
		{
			Log.Info("log.ligmaballs");
			var cloner = prevob.Clone();
			var direction = Vector3.Up;
			
			cloner.Transform.Position += direction * (52) * turn;
			turn += 1;
		}
	}
}

