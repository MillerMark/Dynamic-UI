using DynamicUICore;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DynamicWPF
{
	public partial class DynamicButtonArray : StackPanel, IDynamicControl
	{

		public DynamicButtonArray()
		{
		}

		public DynamicButtonArray(Blueprint blueprint) : this()
		{
			this.Initialize(blueprint);
			BuildControls();
		}

		public void Revert()
		{
			LoadValue(InitialValue);
		}

		ToggleButton GetToggleButtonByFlagValue(int flagValue)
		{
			foreach (UIElement uIElement in buttonContainer.Children)
			{
				FrameworkElement frameworkElement = uIElement as FrameworkElement;
				if (frameworkElement != null && (int)frameworkElement.Tag == flagValue)
					return frameworkElement as ToggleButton;
			}
			return null;
		}

		void SetChecked(int flagValue, bool value)
		{
			ToggleButton toggleButton = GetToggleButtonByFlagValue(flagValue);
			if (toggleButton != null)
				toggleButton.IsChecked = value;
		}

		public void LoadValue(object value)
		{
			int enumValue = (int)value;
			foreach (var fieldInfo in Blueprint.GetEnumElements())
			{
				int flagValue = (int)fieldInfo.GetRawConstantValue();
				SetChecked(flagValue, (enumValue & flagValue) == flagValue);
			}
		}

		public Blueprint Blueprint { get; set; }
		public object InitialValue { get; set; }

		public event EventHandler StateChanged;


		readonly TextBlock displayText = new TextBlock();
		readonly StackPanel buttonContainer = new StackPanel();
		public UIEnumLayout EnumLayout { get; set; }
		public UIEnumOptions EnumOptions { get; set; }

		public void BuildControls()
		{
			Orientation = Orientation.Horizontal;
			displayText.Margin = new Thickness(4, 0, 0, 0);
			Children.Add(displayText);
			if (EnumLayout == UIEnumLayout.Horizontal)
				buttonContainer.Orientation = Orientation.Horizontal;
			else
				buttonContainer.Orientation = Orientation.Vertical;

			buttonContainer.Margin = new Thickness(6, 0, 0, 0);
			int enumValue = Blueprint.GetIntValue();


			foreach (var fieldInfo in Blueprint.GetEnumElements())
			{
				ToggleButton toggleButton;
				if (EnumOptions == UIEnumOptions.RadioButtons)
					toggleButton = new RadioButton();
				else
					toggleButton = new CheckBox();

				var displayName = fieldInfo.Name.Replace('_', ' ');

				toggleButton.Content = displayName;
				toggleButton.Click += DynamicButtonArray_Clicked;

				int flagValue = (int)fieldInfo.GetRawConstantValue();
				toggleButton.Tag = flagValue;
				toggleButton.IsChecked = (enumValue & flagValue) == flagValue;

				toggleButton.Margin = new Thickness(0, 1, 0, 0);

				buttonContainer.Children.Add(toggleButton);
			}

			Children.Add(buttonContainer);
		}



		/// <summary>
		/// Gets the new enum value based on the specified enumValue and the button that was toggled.
		/// </summary>
		private int GetNewEnumValue(ToggleButton toggleButton, int enumValue)
		{
			return enumValue & (255 - (int)toggleButton.Tag);
		}

		private void SetPropertyValue(int value)
		{
			Blueprint.SetValue(value);
		}

		public void InitializeValue()
		{
			BuildControls();
		}

		void DynamicButtonArray_Clicked(object sender, RoutedEventArgs e)
		{
			ToggleButton toggleButton = sender as ToggleButton;
			if (toggleButton == null)
				return;

			if (Blueprint.IsEmpty || !toggleButton.IsChecked.HasValue)
				return;

			int enumValue = Blueprint.GetIntValue();

			if (toggleButton.IsChecked.Value)
				if (EnumOptions == UIEnumOptions.RadioButtons)
					this.SaveValue((int)toggleButton.Tag);
				else
					this.SaveValue(enumValue | (int)toggleButton.Tag);
			else
				this.SaveValue(GetNewEnumValue(toggleButton, enumValue));

			StateChanged?.Invoke(this, EventArgs.Empty);
		}

		public string DisplayText
		{
			get
			{
				return displayText.Text;
			}
			set
			{
				displayText.Text = value;
			}
		}

	}
}
