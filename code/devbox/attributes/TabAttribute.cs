using System;

namespace Devbox;

[AttributeUsage( AttributeTargets.Class )]
public sealed class TabAttribute : TagAttribute
{
	public string Name;

	public TabAttribute( string name ) : base( "tab" )
	{
		this.Name = name;
	}
}
