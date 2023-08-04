using Devbox.UI;
using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;

namespace Devbox;
public class ToolList : Panel
{
	public readonly List<Collapse> CollapseList = new();

	public ToolList() {
		this.StyleSheet.Load( "/devbox/styles/toollist.scss" );
	}

	public void AddCollapse( Collapse collapse ) { 
		collapse.Parent = this;
		this.CollapseList.Add( collapse );
	}

	public void Clear() { 
		this.CollapseList.Clear();
		this.DeleteChildren(true);
	}
}
