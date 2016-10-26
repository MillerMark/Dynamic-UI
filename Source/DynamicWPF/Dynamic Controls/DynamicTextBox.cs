using System.Xaml;
using System.Reflection;
using System.Windows.Controls;
using System;
using System.Windows.Media;
using DynamicUICore;

namespace DynamicWPF
{
	public class DynamicTextBox : TextBox, IDynamicControl
	{
		public DynamicTextBox()
		{
			TextChanged += DynamicTextBox_TextChanged;
		}

		public DynamicTextBox(Blueprint blueprint) : this()
		{
			this.Initialize(blueprint);
		}

		private void DynamicTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.SaveValue(Text);
			StateChanged?.Invoke(this, EventArgs.Empty);
		}

		public void Revert()
		{
			LoadValue(InitialValue);
		}

		public void LoadValue(object value)
		{
			Text = (string)value;
		}

		public Blueprint Blueprint { get; set; }
		public object InitialValue { get; set; }
		public event EventHandler StateChanged;
	}
}
