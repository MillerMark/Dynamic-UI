using Contract;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace DynamicUIDemo
{
	public static class AssemblyLinePreview
	{
		public static void Show(Canvas canvas, ObservableCollection<AssemblyLineControl> assemblyLine)
		{
			canvas.Children.Clear();
			Product newProduct = new Product();
			newProduct.Width = 40;
			newProduct.Height = 40;
			double top = 0d;
			double maxWidth = 0d;
			PlaceLabel(canvas, "Start", ref top, ref maxWidth);
			PlaceProduct(canvas, newProduct, ref top, ref maxWidth);
			foreach (AssemblyLineControl assemblyLineControl in assemblyLine)
			{
				PlaceLabel(canvas, assemblyLineControl.Name, ref top, ref maxWidth);
				assemblyLineControl.Process(newProduct);
				PlaceProduct(canvas, newProduct, ref top, ref maxWidth);
			}
			canvas.Height = top;
			canvas.Width = maxWidth;
	}

		static FrameworkElement GetShapeFrom(Product product)
		{
			double newOutlineThickness = product.OutlineThickness;
			if (newOutlineThickness == 0)
				newOutlineThickness = 1;
			switch (product.Shape)
			{
				case ProductShape.Rectangle:
					return new Rectangle() { Width = product.Width, Height = product.Height, Fill = new SolidColorBrush(product.FillColor), Stroke = new SolidColorBrush(product.OutlineColor), StrokeThickness = newOutlineThickness };
				case ProductShape.Ellipse:
					return new Ellipse() { Width = product.Width, Height = product.Height, Fill = new SolidColorBrush(product.FillColor), Stroke = new SolidColorBrush(product.OutlineColor), StrokeThickness = newOutlineThickness };
				case ProductShape.RoundedRect:
					{
						Border newBorder = new Border();
						newBorder.Width = product.Width;
						newBorder.Height = product.Height;
						newBorder.Background = new SolidColorBrush(product.FillColor);
						newBorder.BorderBrush = new SolidColorBrush(product.OutlineColor);
						newBorder.BorderThickness = new Thickness(newOutlineThickness);
						newBorder.CornerRadius = new CornerRadius(product.CornerRadius);
						return newBorder;
					}
				default:
					return new Rectangle() { Width = product.Width, Height = product.Height, Fill = new SolidColorBrush(product.FillColor), Stroke = new SolidColorBrush(product.OutlineColor), StrokeThickness = newOutlineThickness };
			}
		}

		static void PlaceProduct(Canvas canvas, Product product, ref double top, ref double maxWidth)
		{
			FrameworkElement element = GetShapeFrom(product);
			canvas.Children.Add(element);
			Canvas.SetTop(element, top);
			TextBlock textBlock = new TextBlock();
			textBlock.Text = product.Name;
			textBlock.Margin = new Thickness(4 + product.LeftIndent, 4 + product.TopIndent, 4, 4);
			textBlock.Foreground = new SolidColorBrush(product.TextColor);
			var newFontSize = product.FontSize;
			if (newFontSize != 0)
				textBlock.FontSize = newFontSize;
			canvas.Children.Add(textBlock);
			Canvas.SetTop(textBlock, top);
			if (element.Width > maxWidth)
				maxWidth = element.Width;
			top += element.Height + 10;
		}

		static void PlaceLabel(Canvas canvas, string name, ref double top, ref double maxWidth)
		{
			TextBlock textBlock = new TextBlock();
			textBlock.Text = name;
			textBlock.Margin = new Thickness(4);
			textBlock.Foreground = new SolidColorBrush(Colors.Gray);
			canvas.Children.Add(textBlock);
			Canvas.SetTop(textBlock, top);
			textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
			if (textBlock.DesiredSize.Width > maxWidth)
				maxWidth = textBlock.DesiredSize.Width;
			top += textBlock.DesiredSize.Height;
		}
	}
}
