using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Devbox.UI;

public class Collapse : Panel
{
	string GroupName;
	public Panel View;
	bool isCollapsed = false;

	public Collapse( string groupname ) {
		this.GroupName = groupname;

		this.StyleSheet.Load("/devbox/styles/collapse.scss");

		this.AddClass( "collapse" );

		var button = this.Add.Button(GroupName);
		button.AddClass( "collapse-button" );

		this.View = this.Add.Panel( "collapse-view" );
		View.AddClass( "active" );

		button.AddEventListener( "onclick", () =>
		{
			this.isCollapsed = !this.isCollapsed;

			View.SetClass( "active", !this.isCollapsed );
		} );

	}
	public void addItem( Panel panel ) 
	{
		panel.AddClass( "collapse-view-item" );
		this.View.AddChild( panel );
	}
}
