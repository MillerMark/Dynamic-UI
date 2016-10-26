using DynamicUICore;
using System;
using System.Reflection;
using System.Windows.Controls;

namespace DynamicWPF
{
	public class DynamicCheckBox : CheckBox, IDynamicControl
	{
		public DynamicCheckBox()
		{
			// Hook into the ancestor control's state change event...
			Checked += StateChangedHandler;
			Unchecked += StateChangedHandler;
		}

		public void StateChangedHandler(object o1, object o2)
		{
			// Save the value to the live instance...
			this.SaveValue(IsChecked);

			// Trigger the StateChanged event...
			StateChanged?.Invoke(this, EventArgs.Empty);
		}

		public DynamicCheckBox(Blueprint blueprint) : this()
		{
			this.Initialize(blueprint);
		}

		public void Revert()
		{
			LoadValue(InitialValue);
		}

		public void LoadValue(object value)
		{
			// Assign the specified value to the dynamic control:
			IsChecked = (bool)value;
		}

		public Blueprint Blueprint { get; set; }
		public object InitialValue { get; set; }

		public event EventHandler StateChanged;
	}
}
