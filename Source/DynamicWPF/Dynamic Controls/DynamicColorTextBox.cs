using DynamicUICore;
using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;

namespace DynamicWPF
{
	public class DynamicColorTextBox : TextBox, IDynamicControl
	{
		public DynamicColorTextBox()
		{
			TextChanged += StateChangedHandler;
		}

		public void StateChangedHandler(object o1, object o2)
		{
			// TODO: Template fh (FromHtml).
			System.Drawing.Color drawingColor;
			try
			{
				drawingColor = System.Drawing.ColorTranslator.FromHtml(Text);
			}
			catch
			{
				return;
			}
			var color = Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
			if (swatchShape != null)
				swatchShape.Fill = new SolidColorBrush(color);
			this.SaveValue(color);
			StateChanged?.Invoke(this, EventArgs.Empty);
		}

		public DynamicColorTextBox(Blueprint blueprint) : this()
		{
			this.Initialize(blueprint);
		}

        public void Revert()
		{
			LoadValue(InitialValue);
		}

		public void LoadValue(object value)
		{
			Color color = (Color)value;
			Text = (string)System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(color.R, color.G, color.B));
		}

		System.Windows.Shapes.Shape swatchShape;
		public System.Windows.Shapes.Shape SwatchShape
		{
			get
			{
				return swatchShape;
			}
			set
			{
				if (swatchShape == value)
					return;
				swatchShape = value;
				StateChangedHandler(null, null);
			}
		}

        public Blueprint Blueprint { get; set; }
        public object InitialValue { get; set; }
		public event EventHandler StateChanged;
	}
}
