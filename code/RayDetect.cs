using Sandbox;

public sealed class RayDetect : Component
{
	[Property] public GameObject Base1 {get; set;}
	[Property] public GameObject Base2 {get; set;}
	[Property] public GameObject Base3 {get; set;}
	[Property] public GameObject pBlock1 {get; set;}
	[Property] public GameObject pBlock2 {get; set;}
	[Property] public GameObject pBlock3 {get; set;}
	[Property] public GameObject particles {get; set;}
	[Property] public GameObject confetti {get; set;}
	[Property] public GameObject pauseMenu {get; set;}
	[Property] public GameObject leaderBoardMenu {get; set;}
	[Property] Mover player {get; set;}
	[Property] Camshake shake {get; set;}
	
	public bool ignoreinputs = false;
	public bool b1Dead = false;
	public bool b2Dead = false;
	public bool b3Dead = false;
	public int count1 = 0;
	public int count2 = 0;
	public int count3 = 0;
	public float blocksHit = 3;
	public float blocksRemain = 3;

	protected override void OnUpdate()
	{
		if (blocksRemain == 0 && !ignoreinputs)
		{
			ignoreinputs = true;
			shake.ViewBob();
			OpenLeaderBoard();
		}

		if (!b1Dead)B1Destroyer();
		if (!b2Dead)B2Destroyer();
		if (!b3Dead)B3Destroyer();
		
		EffectsController();

		if(!ignoreinputs)
		{
			player.controller();
		}
	}

	void B1Destroyer()
	{
		var startPos1 = WorldPosition + Vector3.Left*52;
		var endPos1 = startPos1 + Vector3.Down *52;
		var tr1 = Scene.Trace.Ray(startPos1, endPos1)
		.WithoutTags("player").Size(5).Run();

		// var draw1 = Gizmo.Draw;
		// draw1.Color = Color.Green;
		// draw1.LineThickness = 2;
		// draw1.Line(startPos1, endPos1);

		if(Input.Pressed("jump") && !tr1.Hit && count1 == 0 && !ignoreinputs) 
		{
			Base1.Destroy();
			var clone = particles.Clone();
			clone.WorldPosition = pBlock1.WorldPosition;
			pBlock1.Destroy();
			b1Dead =true;
			count1++;
			blocksRemain--;
			Log.Info(b1Dead);
		}
	}
	void B2Destroyer()
	{
		var startPos2 = WorldPosition;
		var endPos2 = startPos2 + Vector3.Down *52;
		var tr2 = Scene.Trace.Ray(startPos2, endPos2)
		.WithoutTags("player").Size(5).Run();

		// var draw2 = Gizmo.Draw;
		// draw2.Color = Color.Green;
		// draw2.LineThickness = 2;
		// draw2.Line(startPos2, endPos2);

		if(Input.Pressed("jump") && !tr2.Hit &&  count2 == 0 && !ignoreinputs) 
		{
			Base2.Destroy();
			var clone = particles.Clone();
			clone.WorldPosition = pBlock2.WorldPosition;
			pBlock2.Destroy();
			b2Dead =true;
			count2++;
			blocksRemain--;
			Log.Info(b2Dead);
		}
	}
	void B3Destroyer()
	{
		var startPos3 = WorldPosition + Vector3.Right*52;
		var endPos3 = startPos3 + Vector3.Down *52;
		var tr3 = Scene.Trace.Ray(startPos3, endPos3)
		.WithoutTags("player").Size(5).Run();

		// var draw3 = Gizmo.Draw;
		// draw3.Color = Color.Green;
		// draw3.LineThickness = 2;
		// draw3.Line(startPos3, endPos3);

		if(Input.Pressed("jump") && !tr3.Hit && count3 == 0 && !ignoreinputs) 
		{
			Base3.Destroy();
			var clone = particles.Clone();
			clone.WorldPosition = pBlock3.WorldPosition;
			pBlock3.Destroy();
			b3Dead =true;
			count3++;
			blocksRemain--;
			Log.Info(b3Dead);
		}
		
	}
	void EffectsController()
	{
		if (Input.Pressed("jump") && blocksRemain == 0 && !ignoreinputs)
		{
			Sound.Play("Fail");
		}
		else if (Input.Pressed("jump") && blocksHit / blocksRemain == 1.0 && !ignoreinputs)
		{
			Sound.Play("AmongUs");
			ConfettiLocationFinder();
		}
		else if (Input.Pressed("jump") && blocksHit / blocksRemain != 1.0 && !ignoreinputs)
		{
			blocksHit--;
			Sound.Play("error4");
		}
	}
	public async void OpenLeaderBoard()
	{
		if (!Application.IsEditor) 
			{
				Sandbox.Services.Stats.SetValue( "score", player.turn - 1 );
				// Log.Info(player.turn - 1);
			}
			
		await Task.DelayRealtimeSeconds(1f);
		pauseMenu.Enabled = false;
		leaderBoardMenu.Enabled = true;
	}
	public void ConfettiLocationFinder()
	{
		var clone = confetti.Clone();
		
		if (!b1Dead && !b2Dead && !b3Dead)
		{
			clone.WorldPosition = Vector3.Up*52*(player.turn) + Vector3.Up*52;
		}
		if (b1Dead)
		{
			clone.WorldPosition = Vector3.Up*52*(player.turn) + Vector3.Up*52 + Vector3.Left*26;
		}
		if (b3Dead)
		{
			clone.WorldPosition = Vector3.Up*52*(player.turn) + Vector3.Up*52 + Vector3.Left*-26;
		}
		if (b1Dead && b3Dead)
		{
			clone.WorldPosition = Vector3.Up*52*(player.turn) + Vector3.Up*52;
		}
		if (b1Dead && b2Dead)
		{
			clone.WorldPosition = Vector3.Up*52*(player.turn) + Vector3.Up*52 + Vector3.Left*52;
		}
		if (b2Dead && b3Dead)
		{
			clone.WorldPosition = Vector3.Up*52*(player.turn) + Vector3.Up*52 + Vector3.Left*-52;
		}
	}

}
