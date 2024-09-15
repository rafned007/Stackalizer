using Sandbox;
using System;

public sealed class CoinSpawner : Component
{
	[Property] public float TimetillClone {get; set;}
	[Property] public GameObject redCoin {get; set;}
	[Property] public GameObject blueCoin {get; set;}
	[Property] public GameObject pinkCoin {get; set;}
	[Property] public Mover player;


	protected override void OnStart()
	{
		TimetillClone = 5f;
		coinspawn();
	}
	protected override void OnUpdate()
	{
		Random rnd = new Random();	
		var rndTime = rnd.Next(1, 8);
		TimetillClone =  rndTime;
	}
	
	async void coinspawn()
	{
		Random rnd = new Random();
		var rndCoin = rnd.Next(1, 4);
		await Task.DelaySeconds(TimetillClone);
		
		if (player.turn >= 8)
		{
			if (rndCoin == 0)
			{
				Log.Info("wtf");
				cloneagain();
			}
			if (rndCoin == 1)
			{
				var cloneRC = redCoin.Clone(Transform.Position + (Vector3.Forward * -104));
				Log.Info(rndCoin);
			}
			else if (rndCoin == 2)
			{
				Log.Info(rndCoin);
				var cloneBC = blueCoin.Clone(Transform.Position + (Vector3.Forward * -104));
			}
			else
			{
				Log.Info(rndCoin);
				var clonePC = pinkCoin.Clone(Transform.Position + (Vector3.Forward * -104));
			}
		}
		cloneagain();
	}
	void cloneagain()
	{
		coinspawn();
	}
}