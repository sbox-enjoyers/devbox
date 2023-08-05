using Sandbox;
using Sandbox.UI;

namespace Devbox.UI;

[Library( "color_inspector_field", Title = "Light inspector field", Description = "Set color value" )]
public class ColorInsptectorField : BaseInsptectorField
{
	public ColorInsptectorField( ToolInsptectorFieldAttribute toolInsptectorFieldAttribute ) : base( toolInsptectorFieldAttribute )
	{
		SetToolFieldValue( Color.Blue );
	}
}
