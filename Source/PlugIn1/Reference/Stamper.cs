using System;
using System.ComponentModel.Composition;
using Contract;
using System.Windows.Media;
using DynamicUICore;

namespace PlugIn1
{
	[Export(typeof(AssemblyLineControl))]
	public class Stamper : AssemblyLineControl
	{
		[UI("Name:")]
		public string Text { get; set; }

		[UI("Text Color:")]
		public Color TextColor { get; set; }


		[UI("Left Indent:")]
		public double LeftIndent { get; set; }

		[UI("Top Indent:")]
		public double TopIndent { get; set; }

		[UI("Font Size:")]
		public double FontSize { get; set; }


		public override void Process(Product product)
		{
			product.Name = Text;

			product.TextColor = TextColor;
			product.FontSize = FontSize;

			product.LeftIndent = LeftIndent;
			product.TopIndent = TopIndent;

		}
	}
}
