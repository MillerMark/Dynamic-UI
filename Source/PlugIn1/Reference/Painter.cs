using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Contract;
using DynamicUICore;

namespace PlugIn1
{
	
	[Export(typeof(AssemblyLineControl))]
	public class Painter : AssemblyLineControl
	{
		[UI("Change Fill")]
		public bool ChangeFill { get; set; }

		[UI("Fill:", getVisible: nameof(ChangeFill))]
		public Color Color { get; set; }

		[UI("Change Outline")]
		public bool ChangeOutline { get; set; }


		[UI("Outline:", getVisible: nameof(ChangeOutline))]
		public Color OutlineColor { get; set; }

		[UI("Stroke Thickness:", getVisible: nameof(ChangeOutline))]
		public double OutlineThickness { get; set; }

		public Painter()
		{
			OutlineThickness = 1;
			OutlineColor = Colors.Gray;
		}

		public override void Process(Product product)
		{
			if (ChangeFill)
				product.FillColor = Color;

			if (ChangeOutline)
			{
				product.OutlineColor = OutlineColor;
				product.OutlineThickness = OutlineThickness;
			}
		}
	}
}
