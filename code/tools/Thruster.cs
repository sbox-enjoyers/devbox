﻿using Devbox;

namespace Sandbox.Tools
{
	[Library( "tool_thruster", Title = "Thruster", Description = "A rocket type thing that can push forwards and backward", Group = "construction" )]
	public partial class ThrusterTool : BaseTool
	{
		PreviewEntity previewModel;
		bool massless = true;

		public override void CreatePreviews()
		{
			if ( TryCreatePreview( ref previewModel, "models/thruster/thrusterprojector.vmdl" ) )
			{
				previewModel.RotationOffset = Rotation.FromAxis( Vector3.Right, -90 );
			}
		}

		protected override bool IsPreviewTraceValid( TraceResult tr )
		{
			if ( !base.IsPreviewTraceValid( tr ) )
				return false;

			if ( tr.Entity is ThrusterEntity )
				return false;

			return true;
		}

		public override void Simulate()
		{
			if ( !Game.IsServer )
				return;

			using ( Prediction.Off() )
			{
				if ( Input.Pressed( "attack2" ) )
				{
					massless = !massless;
				}

				if ( !Input.Pressed( "attack1" ) )
					return;

				var tr = DoTrace();

				if ( !tr.Hit )
					return;

				if ( !tr.Entity.IsValid() )
					return;

				var attached = !tr.Entity.IsWorld && tr.Body.IsValid() && tr.Body.PhysicsGroup != null && tr.Body.GetEntity().IsValid();

				if ( attached && tr.Entity is not Prop )
					return;

				CreateHitEffects( tr.EndPosition );

				if ( tr.Entity is ThrusterEntity )
				{
					// TODO: Set properties

					return;
				}

				var ent = new ThrusterEntity
				{
					Position = tr.EndPosition,
					Rotation = Rotation.LookAt( tr.Normal, Owner.EyeRotation.Forward ) * Rotation.From( new Angles( 90, 0, 0 ) ),
					PhysicsEnabled = !attached,
					EnableSolidCollisions = !attached,
					TargetBody = attached ? tr.Body : null,
					Massless = massless
				};

				if ( attached )
				{
					ent.SetParent( tr.Body.GetEntity(), tr.Body.GroupName );
				}

				ent.SetModel( "models/thruster/thrusterprojector.vmdl" );
			}
		}
	}
}
