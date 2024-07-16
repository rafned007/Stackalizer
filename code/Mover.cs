using Sandbox;

public sealed class Mover : Component
{    
	[Property] private GameObject Border { get; set; }
    [Property] public float TimetillMove { get; set; }
    [Property] public float TimeRatio { get; set; }
	[Property] public GameObject Camera {get; set;}
	[Property] public GameObject Base { get; set;}
	public int turn = 1;
    
	public TimeUntil doMove;
	public Vector3 direction;
	public bool jumped = false;

    protected override void OnStart()
    {
		doMove = TimetillMove;
		direction = Vector3.Left;
    }
	
    protected override void OnUpdate()
	{
		controller();
		
		Move();
	}

	public void Move()
	{
		if (doMove && !jumped)
		{
    		Transform.Position += direction * 52;
			doMove = TimetillMove;
		}

		if (Vector3.DistanceBetween(Transform.Position, Border.Transform.Position) >= (12*52))
		{
			if (direction == Vector3.Left)
			{
				direction = Vector3.Right;
				Transform.Position += direction * 52;
			}
			else
			{
				direction = Vector3.Left;
				Transform.Position += direction * 52;
			}
		}
	}

	public void controller()
	{
			
		// cloner.Transform.Position += (direction * (52 * turn));
		if (Input.Pressed("jump"))
		{
			jumped = true;
			TimetillMove *= TimeRatio;
			//clone prefab
			var clone = Base.Clone();
			// timing was off - moved clone to the left
			clone.Transform.Position = Transform.Position + Vector3.Left * 52;

			Transform.Position = Vector3.Right * (52*6) + (Vector3.Up * (52 * turn));
			Border.Transform.Position = Vector3.Up * (52*turn);

			NextTurn();

			Move();
			turn += 1;
			// Border.Transform.Position += Vector3.Up * (52*turn);
			if (turn % 5 == 0)
			{
				Camera.Transform.Position = (Vector3.Up * (52*turn)) + (Vector3.Backward * 104);
			}
		}
	}

	async void NextTurn()
	{
		await Task.DelaySeconds(.5f);
		jumped = false;
		doMove = TimetillMove;
	}
}