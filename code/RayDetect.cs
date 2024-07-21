using Sandbox;

public sealed class RayDetect : Component
{
	[Property] public GameObject Player {get; set;}
	[Property] public GameObject Base1 {get; set;}
	[Property] public GameObject Base2 {get; set;}
	[Property] public GameObject Base3 {get; set;}
	[Property] public GameObject pBlock1 {get; set;}
	[Property] public GameObject pBlock2 {get; set;}
	[Property] public GameObject pBlock3 {get; set;}
	[Property] public GameObject particles {get; set;}
	public bool ignoreinputs = false;
	public int count1 = 0;
	public int count2 = 0;
	public int count3 = 0;
	public float blocksHit = 3;
	public float blocksRemain = 3;
	

	protected override void OnUpdate()
	{
		var startPos1 = Player.Transform.Position + Vector3.Left*52;
		var endPos1 = startPos1 + Vector3.Down *52;
		var tr1 = Scene.Trace.Ray(startPos1, endPos1)
		.WithoutTags("player").Size(5).Run();

		// var draw1 = Gizmo.Draw;

		// draw1.Color = Color.Green;
		// draw1.LineThickness = 2;
		// draw1.Line(startPos1, endPos1);

		if(!tr1.Hit && Input.Pressed("jump") && count1 == 0 && !ignoreinputs) 
		{
			Base1.Destroy();
			var clone = particles.Clone();
			clone.Transform.Position = pBlock1.Transform.Position;
			pBlock1.Destroy();
			count1++;
			blocksRemain--;
		}

		var startPos2 = Player.Transform.Position;
		var endPos2 = startPos2 + Vector3.Down *52;
		var tr2 = Scene.Trace.Ray(startPos2, endPos2)
		.WithoutTags("player").Size(5).Run();

		// var draw2 = Gizmo.Draw;

		// draw2.Color = Color.Green;
		// draw2.LineThickness = 2;
		// draw2.Line(startPos2, endPos2);

		if(!tr2.Hit && Input.Pressed("jump") && count2 == 0 && !ignoreinputs) 
		{
			Base2.Destroy();
			var clone = particles.Clone();
			clone.Transform.Position = pBlock2.Transform.Position;
			pBlock2.Destroy();
			count2++;
			blocksRemain--;
		}

		var startPos3 = Player.Transform.Position + Vector3.Right*52;
		var endPos3 = startPos3 + Vector3.Down *52;
		var tr3 = Scene.Trace.Ray(startPos3, endPos3)
		.WithoutTags("player").Size(5).Run();

		// var draw3 = Gizmo.Draw;

		// draw3.Color = Color.Green;
		// draw3.LineThickness = 2;
		// draw3.Line(startPos3, endPos3);

		if(!tr3.Hit && Input.Pressed("jump") && count3 == 0 && !ignoreinputs) 
		{
			Base3.Destroy();
			var clone = particles.Clone();
			clone.Transform.Position = pBlock3.Transform.Position;
			pBlock3.Destroy();
			count3++;
			blocksRemain--;
		}

		if (Input.Pressed("jump") && blocksHit / blocksRemain == 1.0 && !ignoreinputs)
		{
			Sound.Play("perfect!");
		}
		else if (Input.Pressed("jump") && blocksHit / blocksRemain != 1.0 && !ignoreinputs)
		{
			blocksHit--;
			Sound.Play("error4");
		}

		if (Input.Pressed("jump") && !tr1.Hit && !tr2.Hit && !tr3.Hit && !ignoreinputs)
		{
			restart();
		}
	}
	async void restart()
	{
		ignoreinputs = true;
		
		await Task.DelaySeconds(1);
		Scene.LoadFromFile("scenes/Stack.scene");
		ignoreinputs = false;
	}
}
