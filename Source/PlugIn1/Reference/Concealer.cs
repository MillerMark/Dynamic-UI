using Contract;
using DynamicUICore;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Media;

namespace PlugIn1
{
	[Export(typeof(AssemblyLineControl))]
	public class Concealer : AssemblyLineControl
	{
		[UI("Hide: ", enumOptions: UIEnumOptions.Checkboxes)]
		public HideOptions Options { get; set; }

		public override void Process(Product product)
		{
			var transparent = Color.FromArgb(0, 0, 0, 0);
			if (Options.HasFlag(HideOptions.HideFill))
				product.FillColor = transparent;
			if (Options.HasFlag(HideOptions.HideText))
				product.TextColor = transparent;
			if (Options.HasFlag(HideOptions.HideOutline))
				product.OutlineColor = transparent;
		}
	}
}
