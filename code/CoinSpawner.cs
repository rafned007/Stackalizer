using Sandbox;
using System;

public sealed class CoinSpawner : Component
{
	[Property] public float TimetillClone {get; set;}
	[Property] public GameObject redCoin {get; set;}
	[Property] public GameObject blueCoin {get; set;}
	[Property] public GameObject pinkCoin {get; set;}
	[Property] public Mover player;
	[Property] public PauseMenu pauseMenu;


	protected override void OnStart()
	{
		TimetillClone = 5f;
		coinspawn();
	}
	protected override void OnUpdate()
	{
		
	}
	
	async void coinspawn()
	{
		Random rnd = new Random();
		var rndCoin = rnd.Next(1, 4);
		TimetillClone = rnd.Next(1, 8);
		await Task.DelayRealtimeSeconds(TimetillClone);
		
		if (player.turn >= 8 && !pauseMenu.IsPaused)
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