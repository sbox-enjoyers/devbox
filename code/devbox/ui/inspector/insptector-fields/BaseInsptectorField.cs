using Sandbox;
using Sandbox.UI;

namespace Devbox.UI;

public abstract class BaseInsptectorField : Panel
{
	public ToolInsptectorFieldAttribute ToolInsptectorFieldAttribute { get; set; }

	public BaseInsptectorField( ToolInsptectorFieldAttribute toolInsptectorFieldAttribute ) : base()
	{
		this.ToolInsptectorFieldAttribute = toolInsptectorFieldAttribute;

		var label = new Label();
		label.Text = this.ToolInsptectorFieldAttribute.Title;
		this.AddChild( label );
	}

	protected void SetToolFieldValue(object value)
	{
		Log.Info( $"{this.ToolInsptectorFieldAttribute.PropertyName}" );
		Event.Run( "tool.update", this.ToolInsptectorFieldAttribute.PropertyName, value );
	}
}
