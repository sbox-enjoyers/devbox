using System;

namespace Devbox;


[AttributeUsage( AttributeTargets.Property )]
public sealed class ToolInsptectorColorFieldAttribute : ToolInsptectorFieldAttribute
{
	public ToolInsptectorColorFieldAttribute( string title ) : base( title, "tool_inspect.color_field" )
	{
		
	}
}
