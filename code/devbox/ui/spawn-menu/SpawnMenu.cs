using Devbox;
using Sandbox;
using Sandbox.Tools;
using Sandbox.UI;
using System.Collections.Generic;

namespace Devbox.UI;

[Library]
public partial class SpawnMenu : Panel
{
	public static SpawnMenu Instance;
	readonly ToolList toollist;
	readonly Panel inspector;

	protected Dictionary<string, Dictionary<string, HashSet<string>>> toolByGroupByTab = new();
	protected string activeTab = "tools";

	public SpawnMenu()
	{
		this.buildToolTabs();

		Instance = this;

		var view = new ResizableSplitView();

		var left = new Panel();
		left.AddClass( "left" );
		{
			var tabs = left.AddChild<ButtonGroup>();
			tabs.AddClass( "tabs" );

			var body = left.Add.Panel( "body" );
			{
				var props = body.AddChild<SpawnList>();
				tabs.SelectedButton = tabs.AddButtonActive( "#spawnmenu.props", ( b ) => props.SetClass( "active", b ) );

				var models = body.AddChild<ModelList>();
				tabs.AddButtonActive( "#spawnmenu.modellist", ( b ) => models.SetClass( "active", b ) );

				var ents = body.AddChild<EntityList>();
				tabs.AddButtonActive( "#spawnmenu.entities", ( b ) => ents.SetClass( "active", b ) );

				var npclist = body.AddChild<NpcList>();
				tabs.AddButtonActive( "#spawnmenu.npclist", ( b ) => npclist.SetClass( "active", b ) );
			}
		}


		var right = new Panel();
		right.AddClass( "right" );
		{
			var tabs = right.AddChild<ButtonGroup>();
			tabs.AddClass( "tabs" );
			{
				var tabAttributes = new List<(TypeDescription Type, TabAttribute Attribute)>(TypeLibrary.GetTypesWithAttribute<TabAttribute>());

				tabAttributes.Sort( (a, b) => { 
					return a.Attribute.OrderPriority > b.Attribute.OrderPriority 
						? 1 
						: a.Attribute.OrderPriority == b.Attribute.OrderPriority
							? 0
							: -1; 
				} );

				foreach ( var entry in tabAttributes )
				{
					Panel panel = TypeLibrary.Create<Panel>( entry.Type.ClassName );

					tabs.AddButtonActive( $"#tab.{entry.Attribute.Name}", ( b ) =>
					{
						panel.SetClass( "active", b );

						if ( b )
						{
							this.activeTab = entry.Attribute.Name;
							this.RebuildToolList();
						}
					} );
				}

				tabs.SelectedButton = tabs.GetChild( 0 );
			}

			var body = new ResizableSplitView();
			body.AddClass( "body" );
			{
				toollist = new ToolList();
				body.SetLeftPanel( toollist );

				inspector = new Inspector();
				body.SetRightPanel( inspector );

				
			}
			right.AddChild( body );
		}

		view.SetLeftPanel( left );
		view.SetRightPanel( right );

		this.AddChild( view );
	}

	public void RebuildToolList()
	{
		toollist.Clear();

		var groups = this.toolByGroupByTab.GetOrCreate( this.activeTab );

		foreach ( var groupName in groups.Keys )
		{
			var collapse = new Collapse( groupName );
			toollist.AddCollapse( collapse );

			var tools = groups.GetOrCreate( groupName );

			foreach ( var tool in tools )
			{
				if ( tool == "BaseTool" )
					continue;

				var button = new Button( $"#tools.{tool}" );
				collapse.addItem( button );

				button.SetClass( "active", tool == ConsoleSystem.GetValue( "tool_current" ) );

				button.AddEventListener( "onclick", () =>
				{
					SetActiveTool( tool );

					foreach ( var col in toollist.CollapseList )
					{
						foreach ( var child in col.View.Children ) {
							child.SetClass( "active", child == button );
						}
					}
				} );
			}
		}
	}

	void SetActiveTool( string className )
	{
		// setting a cvar
		BaseTool.SetCurrentTool( className );

		// set the active weapon to the toolgun
		if ( Game.LocalPawn is not Player player ) return;
		if ( player.Inventory is null ) return;

		// why isn't inventory just an ienumurable wtf
		for ( int i = 0; i < player.Inventory.Count(); i++ )
		{
			var entity = player.Inventory.GetSlot( i );
			if ( !entity.IsValid() ) continue;
			if ( entity.ClassName != "weapon_tool" ) continue;

			player.ActiveChildInput = entity;
		}
	}

	public override void Tick()
	{
		base.Tick();

		Parent.SetClass( "spawnmenuopen", Input.Down( "menu" ) );

		UpdateActiveTool();
	}

	void UpdateActiveTool()
	{
/*		var toolCurrent = ConsoleSystem.GetValue( "tool_current" );
		var tool = string.IsNullOrWhiteSpace( toolCurrent ) ? null : TypeLibrary.GetType<BaseTool>( toolCurrent );

		foreach ( var child in toollist.Children )
		{
			if ( child is Button button )
			{
				child.SetClass( "active", tool != null && button.Text == tool.Title );
			}
		}*/
	}

	public override void OnHotloaded()
	{
		base.OnHotloaded();
		this.buildToolTabs();
		this.RebuildToolList();
	}


	protected void buildToolTabs()
	{
		toolByGroupByTab.Clear();

		foreach ( var entry in TypeLibrary.GetTypes<BaseTool>() )
		{
			string tabName;
			string groupName;

			if ( TypeLibrary.HasAttribute<ToolAttribute>( entry.TargetType ) )
			{
				var toolAttr = TypeLibrary.GetAttribute<ToolAttribute>( entry.TargetType );

				tabName = toolAttr.Tab;
				groupName = toolAttr.Group;
			}
			else
			{
				tabName = "tools";
				groupName = "others";
			}

			var groups = toolByGroupByTab.GetOrCreate( tabName );
			var tools = groups.GetOrCreate( groupName );

			tools.Add( entry.ClassName );
		}
	}
}
