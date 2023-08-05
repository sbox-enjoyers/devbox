using Sandbox;
using Sandbox.UI;

namespace Devbox.UI;

public abstract class InspectorViewBase : Panel
{
	public TypeDescription ToolTypeDescription { get; set; }
	public ToolAttribute ToolAttribute { get; set; }

	public InspectorViewBase( TypeDescription toolTypeDescription, ToolAttribute toolAttribute ) : base()
	{
		this.ToolTypeDescription = toolTypeDescription;
		this.ToolAttribute = toolAttribute;
	}
}
