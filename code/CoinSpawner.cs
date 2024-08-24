using Sandbox;
using System;

public sealed class CoinSpawner : Component
{
	[Property] public float TimetillClone {get; set;}
	[Property] public GameObject redCoin {get; set;}
	[Property] public GameObject blueCoin {get; set;}
	[Property] public GameObject pinkCoin {get; set;}
	[Property] public GameObject spawnLocation {get; set;}
	[Property] public Mover player;


	protected override void OnStart()
	{
		TimetillClone = 5f;
		coinspawn();
		Log.Info(player.turn);
	}
	
	async void coinspawn()
	{
		Random rnd = new Random();
		var rndCoin = rnd.Next(0, 3);
		var rndTime = rnd.Next(1, 10);
		await Task.DelaySeconds(rndTime);
		
		if (player.turn >= 8)
		{
			if (rndCoin == 0)
			{
				var cloneRC = redCoin.Clone(Transform.Position + (Vector3.Backward * 104));
			}
			else if (rndCoin == 1)
			{
				var cloneBC = blueCoin.Clone(Transform.Position + (Vector3.Backward * 104));
			}
			else
			{
				var clonePC = pinkCoin.Clone(Transform.Position + (Vector3.Backward * 104));
			}
		}
		cloneagain();
	}
	void cloneagain()
	{
			coinspawn();
	}

	// protected override void OnStart()
	// {
	// 	doClone = TimetillClone;
	// 	Log.Info(doClone == true);
	// }

	// protected override void OnUpdate()
	// {
		
	// 	Log.Info(doClone == true);

		
	// 	CoinClone();
	// }

	
	// public void CoinClone()
	// {
	// 	var cloneRC = redCoin.Clone(Transform.Position);
	// 	var cloneBC = blueCoin.Clone(Transform.Position);
	// 	var clonePC = pinkCoin.Clone(Transform.Position);
	// 	if(doClone)
	// 	{
	// 		Log.Info(Transform.Position);
	// 		cloneRC.Transform.Position = spawnLocation.Transform.Position;
	// 		cloneBC.Transform.Position = spawnLocation.Transform.Position;
	// 		clonePC.Transform.Position = spawnLocation.Transform.Position;
	// 		doClone = 5;
	// 	}
	// }
}