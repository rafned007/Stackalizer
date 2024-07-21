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

	protected override void OnUpdate()
	{
		var startPos1 = Player.Transform.Position + Vector3.Left*52;
		var endPos1 = startPos1 + Vector3.Down *52;
		var tr1 = Scene.Trace.Ray(startPos1, endPos1)
		.WithoutTags("player").Size(5).Run();

		var draw1 = Gizmo.Draw;

		draw1.Color = Color.Green;
		draw1.LineThickness = 2;
		draw1.Line(startPos1, endPos1);

		if(!tr1.Hit && Input.Pressed("jump")) 
		{
			Base1.Destroy();
			pBlock1.Destroy();
		}

		var startPos2 = Player.Transform.Position;
		var endPos2 = startPos2 + Vector3.Down *52;
		var tr2 = Scene.Trace.Ray(startPos2, endPos2)
		.WithoutTags("player").Size(5).Run();

		var draw2 = Gizmo.Draw;

		draw2.Color = Color.Green;
		draw2.LineThickness = 2;
		draw2.Line(startPos2, endPos2);

		if(!tr2.Hit && Input.Pressed("jump")) 
		{
			Base2.Destroy();
			pBlock2.Destroy();
		}

		var startPos3 = Player.Transform.Position + Vector3.Right*52;
		var endPos3 = startPos3 + Vector3.Down *52;
		var tr3 = Scene.Trace.Ray(startPos3, endPos3)
		.WithoutTags("player").Size(5).Run();

		var draw3 = Gizmo.Draw;

		draw3.Color = Color.Green;
		draw3.LineThickness = 2;
		draw3.Line(startPos3, endPos3);

		if(!tr3.Hit && Input.Pressed("jump")) 
		{
			Base3.Destroy();
			pBlock3.Destroy();
		}
	}
}
