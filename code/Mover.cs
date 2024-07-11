using Sandbox;

public sealed class Mover : Component
{    
	[Property] private GameObject movingblock { get; set; }
	[Property] private GameObject border { get; set; }
    [Property] public float TimetillMove { get; set; }
    [Property] public float TimetillChange { get; set; }
    public bool Reverse = false;
    public bool timerstarted = false;
	public TimeUntil doMove;
	public TimeUntil changeDir;
	public Vector3 direction;
	public bool jumped = false;

    protected override void OnStart()
    {
		doMove = TimetillMove;
		direction = Vector3.Left;
    }

	
    protected override void OnUpdate()
	{
		if (Input.Pressed("jump"))
		{
			Log.Info("bruh");
			jumped = true;
			return;
		}

		if (doMove && !jumped)
		{
    		Move();
			doMove = TimetillMove;
		}

		if (Vector3.DistanceBetween(movingblock.Transform.Position, border.Transform.Position) >= 500)
		{
			if (direction == Vector3.Left)
			{
				direction = Vector3.Right;
				Move();
			}
			else
			{
				direction = Vector3.Left;
				Move();
			}
		}
	}

	public void Move()
	{
		movingblock.Transform.Position += direction * 52;
	}

    // public void Move()
    // {
    //     // if (Reverse) direction = Vector3.Right;
    //     {
    //         // MoveAgain();// Moves again after period of time in seconds //
    //     }
    //     // if (!timerstarted)
    //     // StartReverseTimer();
    // }
	
    // public async void MoveAgain()
    // {
    //     await Task.DelaySeconds(TimetillMove);
    //     Move();// Repeat
    // }

    // public async void StartReverseTimer()
    // {
    //     timerstarted = true;
    //     await Task.DelaySeconds(TimetillChange);
    //     Reverse = !Reverse;
    //     timerstarted = false;
    // }
}