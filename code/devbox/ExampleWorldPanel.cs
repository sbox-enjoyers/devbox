using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Devdbox;

public class ExampleWorldPanel : WorldPanel
{
	protected ModelEntity parent;

	public ExampleWorldPanel( ModelEntity parent ) : base()
	{
		this.parent = parent;

		StyleSheet.Load( "/devbox/UI/ExampleWorldPanel.scss" );
		Add.Label( "hello world" );

		this.PanelBounds = new Rect( 0, 0, this.parent.CollisionBounds.Size.x * 20, this.parent.CollisionBounds.Size.y * 20 );
	}

	public override void Tick()
	{
		base.Tick();


		this.Position = this.parent.Position + (this.parent.Rotation.Up * (this.parent.CollisionBounds.Size.z)) + (this.parent.Rotation.Forward * (this.parent.CollisionBounds.Size.x / 2)) - (this.parent.Rotation.Right * (this.parent.CollisionBounds.Size.y / 2));

		this.Rotation = Rotation.LookAt( this.parent.Rotation.Up, this.parent.Rotation.Forward );

	}
}
