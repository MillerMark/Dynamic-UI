using DynamicUICore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DynamicWPF
{
	public static class ControlFactory
	{
		public static FrameworkElement GetCheckbox(Blueprint blueprint, EventHandler StateChangeHandler)
		{
			DynamicCheckBox checkBox = new DynamicCheckBox(blueprint);
			checkBox.Margin = new Thickness(4);
			checkBox.Content = blueprint.UIAttribute.DisplayText;
			checkBox.HookStateChangedEvents(StateChangeHandler);
			return checkBox;
		}

		public static FrameworkElement GetTextbox(Blueprint blueprint, EventHandler StateChangeHandler)
		{
			StackPanel stackPanel = GetLabeledStackPanel(blueprint.UIAttribute);

			try
			{
				DynamicTextBox textBox = new DynamicTextBox(blueprint);
				textBox.Margin = new Thickness(0, 3, 0, 0);
				textBox.HookStateChangedEvents(StateChangeHandler);
				stackPanel.Children.Add(textBox);
			}
			catch (Exception ex)
			{
				AddExceptionDetail(stackPanel, ex);
			}

			return stackPanel;
		}

		public static FrameworkElement GetDoubleTextbox(Blueprint blueprint, EventHandler StateChangeHandler)
		{
			StackPanel stackPanel = GetLabeledStackPanel(blueprint.UIAttribute);

			try
			{
				DynamicDoubleTextBox textBox = new DynamicDoubleTextBox(blueprint);
				textBox.Margin = new Thickness(0, 3, 0, 0);
				textBox.HookStateChangedEvents(StateChangeHandler);
				stackPanel.Children.Add(textBox);
			}
			catch (Exception ex)
			{
				AddExceptionDetail(stackPanel, ex);
			}

			return stackPanel;
		}

		public static FrameworkElement GetColorTextbox(Blueprint blueprint, EventHandler StateChangeHandler)
		{
			StackPanel stackPanel = GetLabeledStackPanel(blueprint.UIAttribute);

			try
			{
				DynamicColorTextBox textBox = new DynamicColorTextBox(blueprint);
				textBox.Margin = new Thickness(0, 3, 0, 0);

				textBox.HookStateChangedEvents(StateChangeHandler);
				stackPanel.Children.Add(textBox);

				System.Windows.Shapes.Ellipse shape = new System.Windows.Shapes.Ellipse() { Width = 22, Height = 22, Margin = new Thickness(4, 2, 0, 0) };
				textBox.SwatchShape = shape;
				stackPanel.Children.Add(shape);
			}
			catch (Exception ex)
			{
				AddExceptionDetail(stackPanel, ex);
			}

			return stackPanel;
		}

		public static FrameworkElement GetButtonArray(Blueprint blueprint, EventHandler StateChangeHandler)
		{
			var uiAttribute = blueprint.UIAttribute;
			DynamicButtonArray buttons = new DynamicButtonArray()
			{
				DisplayText = uiAttribute.DisplayText,
				EnumOptions = uiAttribute.EnumOptions,
				EnumLayout = uiAttribute.EnumLayout,
				Blueprint = blueprint
			};
			buttons.InitializeValue();
			buttons.HookStateChangedEvents(StateChangeHandler);
			return buttons;
		}

		public static void HookStateChangedEvents(this IDynamicControl iDynamicControl, EventHandler StateChangeHandler)
		{
			iDynamicControl.StateChanged += StateChangeHandler;
			iDynamicControl.StateChanged += VisibilityManager.StateChangeHandler;
		}

		public static StackPanel GetLabeledStackPanel(UIAttribute uiAttribute)
		{
			StackPanel stackPanel = new StackPanel();
			stackPanel.Orientation = Orientation.Horizontal;

			TextBlock label = new TextBlock();
			label.Text = uiAttribute.DisplayText;
			label.Margin = new Thickness(4);
			stackPanel.Children.Add(label);
			return stackPanel;
		}

		private static void AddExceptionDetail(StackPanel stackPanel, Exception ex)
		{
			TextBlock warningBlock = new TextBlock();
			warningBlock.Foreground = new SolidColorBrush(Color.FromRgb(148, 0, 0));
			warningBlock.Background = new SolidColorBrush(Color.FromRgb(255, 247, 247));
			warningBlock.Margin = new Thickness(4);
			warningBlock.Text = "Error getting value: " + ex.Message;
			stackPanel.Children.Add(warningBlock);
		}
	}
}
