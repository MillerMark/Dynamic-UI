using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DynamicUICore;

namespace DynamicWPF
{
	public static class DynamicEngine
	{
		public static FrameworkElement BuildUI(Blueprint blueprint, EventHandler StateChangeHandler)
		{
			UIElementType kind;
			if (blueprint.UIAttribute.Kind == UIElementType.Auto)
				kind = GetUIElementType(blueprint.Property.PropertyType);
			else
				kind = blueprint.UIAttribute.Kind;

			switch (kind)
			{
				case UIElementType.Checkbox:
					return ControlFactory.GetCheckbox(blueprint, StateChangeHandler);
				case UIElementType.DoubleTextBox:
					return ControlFactory.GetDoubleTextbox(blueprint, StateChangeHandler);
				case UIElementType.TextBox:
					return ControlFactory.GetTextbox(blueprint, StateChangeHandler);
				case UIElementType.ColorTextBox:
					return ControlFactory.GetColorTextbox(blueprint, StateChangeHandler);
				case UIElementType.Enum:
					return ControlFactory.GetButtonArray(blueprint, StateChangeHandler);
			}
			return null;
		}

		public static UIElementType GetUIElementType(Type propertyType)
		{
			if (propertyType.IsEnum)
				return UIElementType.Enum;

			if (propertyType.FullName == "System.Boolean")
				return UIElementType.Checkbox;

			if (propertyType.FullName == "System.Double")
				return UIElementType.DoubleTextBox;

			if (propertyType.FullName == "System.Windows.Media.Color")
				return UIElementType.ColorTextBox;

			return UIElementType.TextBox;
		}

		public static void ShowDynamicUI(Panel panel, object instance, EventHandler stateChangeHandler)
		{
			panel.Children.Clear();
			PropertyInfo[] properties = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

			FrameworkElement firstControlToFocus = null;
			foreach (var property in properties)
			{
				UIAttribute uiAttribute = property.GetCustomAttribute<UIAttribute>(false);
				if (uiAttribute == null)
					continue;

				Blueprint blueprint = new Blueprint(uiAttribute, instance, property);
				FrameworkElement propEd = BuildUI(blueprint, stateChangeHandler);

				if (firstControlToFocus == null)
					firstControlToFocus = propEd;

				var margin = propEd.Margin;
				margin = new Thickness(margin.Left, margin.Top, margin.Right, 4);
				panel.Children.Add(propEd);
			}

			VisibilityManager.SetParentPanel(panel);
			VisibilityManager.SetChildVisibility(panel);

			if (firstControlToFocus != null)
				firstControlToFocus.FocusFirstElement();
		}
	}
}
