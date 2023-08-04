using System;

namespace Devbox;


[AttributeUsage( AttributeTargets.Property )]
public class ToolInsptectorFieldAttribute : TagAttribute
{
	public string Title;
	public string InspectFieldClassName;


	public ToolInsptectorFieldAttribute( string title, string inspectFieldClassName ) : base( "tool_insptector_field" )
	{
		this.Title = title;
		this.InspectFieldClassName = inspectFieldClassName;
	}
}
