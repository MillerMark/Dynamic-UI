using DynamicUICore;
using System;
using System.Reflection;
using System.Windows.Controls;

namespace DynamicWPF
{
	public class DynamicDoubleTextBox : TextBox, IDynamicControl
	{
		public DynamicDoubleTextBox()
		{
			TextChanged += DynamicDoubleTextBox_TextChanged;
		}

		public DynamicDoubleTextBox(Blueprint blueprint) : this()
		{
			this.Initialize(blueprint);
		}

		private void DynamicDoubleTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			double result;
			if (!double.TryParse(Text, out result))
				result = 0;
			this.SaveValue(result);
			StateChanged?.Invoke(this, EventArgs.Empty);
		}

		public void Revert()
		{
			LoadValue(InitialValue);
		}

		public void LoadValue(object value)
		{
			Text = ((double)value).ToString();
		}

        public Blueprint Blueprint { get; set; }
        public object InitialValue { get; set; }
		public event EventHandler StateChanged;
	}
}
