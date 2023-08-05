using System;

namespace Devbox;


[AttributeUsage( AttributeTargets.Property )]
public abstract class ToolInsptectorFieldAttribute : TagAttribute
{
	public string Title;
	public string ToolInspectorField;
	public string PropertyName;


	public ToolInsptectorFieldAttribute( string title, string toolInspectorField ) : base( "tool_insptector_field" )
	{
		this.Title = title;
		this.ToolInspectorField = toolInspectorField;
	}
}
