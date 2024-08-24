using Sandbox;

public sealed class CoinSpawner : Component
{
	[Property] public float TimetillClone {get; set;}
	[Property] public GameObject redCoin {get; set;}
	[Property] public GameObject blueCoin {get; set;}
	[Property] public GameObject pinkCoin {get; set;}
	[Property] public GameObject spawnLocation {get; set;}


	protected override void OnStart()
	{
		TimetillClone = 5f;
		coinspawn();
	}
	
	
	async void coinspawn()
	{
		await Task.DelaySeconds(TimetillClone);
		var cloneRC = redCoin.Clone(Transform.Position);
		var cloneBC = blueCoin.Clone(Transform.Position);
		var clonePC = pinkCoin.Clone(Transform.Position);
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