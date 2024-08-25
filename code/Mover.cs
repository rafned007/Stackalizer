using Sandbox;
using System;

public sealed class Mover : Component
{    
	[Property] private GameObject Border { get; set; }
    [Property] public float TimetillMove { get; set; }
    [Property] public float TimeRatio { get; set; }
	[Property] public GameObject Camera {get; set;}
	[Property] public GameObject Base { get; set;}
	[Property] RayDetect player { get; set; }
	public int turn = 1;
	public float speed = 100;
    
	public TimeUntil doMove;
	public Vector3 direction;
	public bool jumped = false;

    protected override void OnStart()
    {
		doMove = .5f;
		direction = Vector3.Left;
		player.ignoreinputs = false;
    }
	
	protected override void OnUpdate()
	{
		if (!player.ignoreinputs)
		{
			Move();
		}
		camControl();
		speed = (float)Math.Round(120 - (TimetillMove * 100));
		
	}

	public void Move()
	{
		if (doMove && !jumped)
		{
    		Transform.Position += direction * 52;
			doMove = TimetillMove;
		}

		if (Vector3.DistanceBetween(Transform.Position, Border.Transform.Position) >= (10*52))
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
		if (Input.Pressed("jump"))
		{
			//clone base at players current position
			var clone = Base.Clone();
			clone.Transform.Position = Transform.Position;

			jumped = true;
			TimetillMove *= TimeRatio;
			//move border
			Border.Transform.Position = Vector3.Up * (52*turn);
			//move player
			Transform.Position = Border.Transform.Position + Vector3.Right*(52*9);	

			NextTurn();
		}
	}

	void NextTurn()
	{	
		turn += 1;
		Log.Info($"Level: {turn - 1}");
		jumped = false;
		doMove = .5f;	
		 if (turn % 5 == 0)
		{
			doMove = .7f;
			// Camera.Transform.Position = (Vector3.Up * (52*turn)) + (Vector3.Backward * 104);	
		}
			
	}
	void camControl()
	{	
		var start = Camera.Transform.Position;
		var end = Vector3.Up *(52*turn)+ (Vector3.Backward * 208);
		if (turn % 5 == 0)
		{
			Camera.Transform.Position = start.LerpTo(end, doMove.Fraction/10);	
		}
	}
}