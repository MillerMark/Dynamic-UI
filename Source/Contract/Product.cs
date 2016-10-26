using System;
using System.Linq;
using System.Windows.Media;

namespace Contract
{
	public class Product
	{
		public Product()
		{
			OutlineColor = Colors.LightGray;
		}

		public ProductShape Shape { get; set; }
		public string Name { get; set; }
		public double Height { get; set; }
		public double Width { get; set; }
		public double Weight { get; set; }
		public double CornerRadius { get; set; }
		public double OutlineThickness { get; set; }
		public double FontSize { get; set; }
		public double LeftIndent { get; set; }
		public double TopIndent { get; set; }

		public decimal Price { get; set; }
		public Color FillColor { get; set; }
		public Color OutlineColor { get; set; }
		public Color TextColor { get; set; }
	}
}
