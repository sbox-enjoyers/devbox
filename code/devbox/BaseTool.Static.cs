using Devbox;
using Devbox.UI;
using Sandbox;
using System.Linq;

namespace Sandbox.Tools;

public partial class BaseTool
{
	protected TypeDescription toolType;

	static BaseTool()
	{
		var types = TypeLibrary.GetTypes<BaseTool>( );

		foreach (var type in types )
		{
			foreach ( var property in type.Properties )
			{
				if ( !property.HasAttribute<ToolInsptectorFieldAttribute>() )
				{
					continue;
				}

				ToolInsptectorFieldAttribute toolInsptectorFieldAttribute = property.GetCustomAttribute<ToolInsptectorFieldAttribute>();
				toolInsptectorFieldAttribute.PropertyName = property.Name;
			}
		}
	}

	public static void SetCurrentTool(string toolName)
	{
		Event.Run( "tool.changed", toolName );
		ConsoleSystem.Run( "tool_current", toolName );
	}

	public static string GetCurrentTool( IClient owner )
	{
		var currentTool = owner.GetClientData<string>( "tool_current" );

		if (currentTool == null)
		{
			SetCurrentTool( "tool_balloon" );
		}

		return owner.GetClientData<string>( "tool_current", "tool_balloon" );
	}

	public BaseTool() : base()
	{
		Event.Register( this );

		this.toolType = TypeLibrary.GetType(this.ClassName);
	}

	[Event("tool.update")]
	protected void onValueChange(string propertyName, object value)
	{
		Log.Info( "tool.update" );
		TypeLibrary.SetProperty(this, propertyName, value);
	}

	~BaseTool()
	{
		Event.Unregister( this );
	}
}
