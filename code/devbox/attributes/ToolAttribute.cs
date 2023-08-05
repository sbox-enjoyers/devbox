using System;

namespace Devbox;

[AttributeUsage( AttributeTargets.Class )]
public sealed class ToolAttribute : TagAttribute
{
	public string Tab;
	public string Group;

	public string Inspector { get; set; }

	public ToolAttribute(string tab, string group) : base( "tool" )
	{
		this.Tab = tab;
		this.Group = group;
	}
}
