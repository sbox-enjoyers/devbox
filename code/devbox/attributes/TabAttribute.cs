using System;

namespace Devbox;

[AttributeUsage( AttributeTargets.Class )]
public sealed class TabAttribute : TagAttribute
{
	public string Name;
	public short OrderPriority;

	public TabAttribute( string name , short orderPriority = 0 ) : base( "tab" )
	{
		this.Name = name;
		this.OrderPriority = orderPriority;
	}
}
