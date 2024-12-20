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
	}

	public void Move()
	{
		if (doMove && !jumped)
		{
    		WorldPosition += direction * 52;
			doMove = TimetillMove;
		}

		if (Vector3.DistanceBetween(WorldPosition, Border.WorldPosition) >= (10*52))
		{
			if (direction == Vector3.Left)
			{
				direction = Vector3.Right;
				WorldPosition += direction * 52;
			}
			else
			{
				direction = Vector3.Left;
				WorldPosition += direction * 52;
			}
		}
	}

	public void controller()
	{
		if (Input.Pressed("jump"))
		{
			//clone base at players current position
			var clone = Base.Clone(WorldPosition);
			clone.Tags.Remove("base");

			jumped = true;
			TimetillMove *= TimeRatio;
			//move border
			Border.WorldPosition = Vector3.Up * (52*turn);
			//move player
			WorldPosition = Border.WorldPosition + Vector3.Right*(52*9);	

			NextTurn();
		}
	}

	void NextTurn()
	{	
		turn += 1;
		Log.Info($"Level: {turn}");
		jumped = false;
		doMove = .5f;	
		 if (turn % 5 == 0)
		{
			doMove = .7f;
		}
			
	}
	void camControl()
	{	
		var start = Camera.WorldPosition;
		var end = Vector3.Up *(52*turn)+ (Vector3.Backward * 208);
		if (turn % 5 == 0)
		{
			Camera.WorldPosition = start.LerpTo(end, doMove.Fraction/10);	
		}
	}
}