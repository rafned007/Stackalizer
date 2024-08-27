using Sandbox;

public sealed class destroytrigger : Component, Component.ITriggerListener
{

	public void OnTriggerEnter( Collider other )

	{

		if(!other.GameObject.Tags.Has("base"))
		{
			other.GameObject.Destroy();
			Log.Info ( "sprite destroyed" );
		} 
	 	
		

	}
	public void OnTriggerExit ( Collider other )
	{
		
		{

			
			
		}
	}
}