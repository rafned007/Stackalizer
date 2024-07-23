using Sandbox;
using System.Threading.Tasks;


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
	[Property] public GameObject confetti {get; set;}
	public bool ignoreinputs = false;
	public bool b1Dead = false;
	public bool b2Dead = false;
	public bool b3Dead = false;
	public int count1 = 0;
	public int count2 = 0;
	public int count3 = 0;
	public float blocksHit = 3;
	public float blocksRemain = 3;
	
	protected override void OnPreRender()
	{
		ignoreinputs = false;
	}

	protected override void OnStart()
	{
		b1Dead = false;
		b2Dead = false;
		b3Dead = false;
	}

	protected override void OnUpdate()
	{
		if (!b1Dead ) B1Destroyer();
		if (!b2Dead) B2Destroyer();
		if (!b3Dead) B3Destroyer();
		
		//No blocks landed, player loses
		if (blocksRemain == 0 && !ignoreinputs)
		{
			_= Restart();
		}
		EffectsController();

		if (Input.Pressed("duck"))
		{
			Log.Info(pBlock1);
		}	
	}

	async Task Restart()
	{
		// wait for this amount of seconds
		await Task.DelaySeconds( 1f );
		Scene.LoadFromFile("scenes/LeaderBoard.scene");
	}

	public void B1Destroyer()
	{
		var startPos1 = Player.Transform.Position + Vector3.Left*52;
		var endPos1 = startPos1 + Vector3.Down *52;
		var tr1 = Scene.Trace.Ray(startPos1, endPos1)
		.WithoutTags("player").Size(5).Run();

		var draw1 = Gizmo.Draw;
		draw1.Color = Color.Green;
		draw1.LineThickness = 2;
		draw1.Line(startPos1, endPos1);

		if(!tr1.Hit  && count1 == 0 && !ignoreinputs && Input.Pressed("jump")) 
		{
			Base1.Destroy();
			Sound.Play("error4");
			var clone = particles.Clone();
			clone.Transform.Position = pBlock1.Transform.Position;
			Log.Info(clone.Transform.Position);
			Log.Info(pBlock1.Transform.Position);
			pBlock1.Destroy();
			b1Dead =true;
			count1++;
			blocksRemain--;
		}
	}

	public void B2Destroyer()
	{
		var startPos2 = Player.Transform.Position;
		var endPos2 = startPos2 + Vector3.Down *52;
		var tr2 = Scene.Trace.Ray(startPos2, endPos2)
		.WithoutTags("player").Size(5).Run();

		var draw2 = Gizmo.Draw;
		draw2.Color = Color.Green;
		draw2.LineThickness = 2;
		draw2.Line(startPos2, endPos2);

		if(!tr2.Hit  && count2 == 0 && !ignoreinputs && Input.Pressed("jump")) 
		{
			Base2.Destroy();
			Sound.Play("error4");
			var clone = particles.Clone();
			clone.Transform.Position = pBlock2.Transform.Position;
			pBlock2.Destroy();
			b2Dead =true;
			count2++;
			blocksRemain--;
		}
	}

	public void B3Destroyer()
	{
		var startPos3 = Player.Transform.Position + Vector3.Right*52;
		var endPos3 = startPos3 + Vector3.Down *52;
		var tr3 = Scene.Trace.Ray(startPos3, endPos3)
		.WithoutTags("player").Size(5).Run();

		var draw3 = Gizmo.Draw;
		draw3.Color = Color.Green;
		draw3.LineThickness = 2;
		draw3.Line(startPos3, endPos3);
		Log.Info ( endPos3);

		if(!tr3.Hit && count3 == 0 && !ignoreinputs && Input.Pressed("jump")) 
		{
			Base3.Destroy();
			Sound.Play("error4");
			var clone = particles.Clone();
			clone.Transform.Position = pBlock3.Transform.Position;
			pBlock3.Destroy();
			b3Dead =true;
			count3++;
			blocksRemain--;
		}
	}
	
	public void EffectsController()
	{
		//All blocks landed
		if (Input.Pressed("jump") && blocksHit / blocksRemain == 1.0 && !ignoreinputs)
		{
			Sound.Play("perfect!");
			var clone = confetti.Clone();
			clone.Transform.Position = Player.Transform.Position + Vector3.Up*80;
		}
		//Any blocks missed
		else if (Input.Pressed("jump") && blocksHit / blocksRemain != 1.0 && !ignoreinputs)
		{
			blocksHit--;
		}
	}
	
}
