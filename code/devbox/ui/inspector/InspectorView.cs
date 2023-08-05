using System;
using Sandbox;
using Sandbox.UI;
using Sandbox.Tools;
using Sandbox.Razor;

namespace Devbox.UI;

[StyleSheet( "/devbox/styles/inspector-view.scss" )]
class InspectorView : InspectorViewBase
{

	public InspectorView( TypeDescription toolTypeDescription, ToolAttribute toolAttribute ) : base( toolTypeDescription, toolAttribute )
	{
		foreach ( var property in this.ToolTypeDescription.Properties )
		{
			if ( !property.HasAttribute<ToolInsptectorFieldAttribute>() )
			{
				continue;
			}

			ToolInsptectorFieldAttribute toolInsptectorFieldAttribute = property.GetCustomAttribute<ToolInsptectorFieldAttribute>();

			object[] args = { toolInsptectorFieldAttribute };
			var panel = (BaseInsptectorField)TypeLibrary.Create( toolInsptectorFieldAttribute.ToolInspectorField, typeof( BaseInsptectorField ), args );

			this.AddChild( panel );
		}
	}

}
