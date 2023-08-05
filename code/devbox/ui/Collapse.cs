using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Devbox.UI;

public class Collapse : Panel
{
	string GroupName;
	public Panel View;
	bool isCollapsed = false;

	public Collapse( string groupname )
	{
		GroupName = groupname;

		StyleSheet.Load( "/devbox/styles/collapse.scss" );

		AddClass( "collapse" );

		var button = Add.Button( GroupName );
		button.AddClass( "collapse-button" );

		View = Add.Panel( "collapse-view" );
		View.AddClass( "active" );

		button.AddEventListener( "onclick", () =>
		{
			isCollapsed = !isCollapsed;

			View.SetClass( "active", !isCollapsed );
		} );

	}
	public void addItem( Panel panel )
	{
		panel.AddClass( "collapse-view-item" );
		View.AddChild( panel );
	}
}
