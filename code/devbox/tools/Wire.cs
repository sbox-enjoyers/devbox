using Devbox;
using Sandbox;
using Sandbox.Tools;

namespace Devbox.Tools;

[Library( "tool_wire", Title = "Wire", Description = "Link gates", Group = "general" )]
[Tool( "wireless", "general" )]

public partial class WireTool : BaseTool
{
	public override void Simulate()
	{
		if ( !Game.IsServer )
			return;

		using ( Prediction.Off() )
		{
			if ( !Input.Pressed( "attack1" ) )
				return;

			var tr = DoTrace();

			if ( !tr.Hit || !tr.Entity.IsValid() )
				return;

			if ( tr.Entity is Player )
				return;

			CreateHitEffects( tr.EndPosition );

			if ( tr.Entity.IsWorld )
				return;

			var particle = Particles.Create( "particles/physgun_freeze.vpcf" );
			particle.SetPosition( 0, tr.Entity.Position );
		}
	}
}


