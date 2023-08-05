using Sandbox;
using Sandbox.UI;


namespace Devbox;

[Spawnable]
[Library( "example_world_panel", Title = "Test" )]
public class KeypadEntity : ModelEntity
{
	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/sbox_props/road_signs/sign_mid.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );

		Tags.Add( "solid" );
	}

	public override void ClientSpawn()
	{
		var worldPanel = new ExampleWorldPanel(this);
	}
}
