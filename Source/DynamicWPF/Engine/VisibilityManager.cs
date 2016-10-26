using DynamicUICore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DynamicWPF
{
	public static class VisibilityManager
	{
		public static void SetParentPanel(Panel panel)
		{
			Panel = panel;
		}
		public static Panel Panel { get; set; }

		static IDynamicControl GetDynamicControl(DependencyObject parent)
		{
			IDynamicControl iDynamicParent = parent as IDynamicControl;
			if (iDynamicParent != null)
				return iDynamicParent;

			var numChildren = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < numChildren; i++)
			{
				DependencyObject childControl = VisualTreeHelper.GetChild(parent, i);
				IDynamicControl iDynamicControl = childControl as IDynamicControl;
				if (iDynamicControl != null)
					return iDynamicControl;

				iDynamicControl = GetDynamicControl(childControl);
				if (iDynamicControl != null)
					return iDynamicControl;
			}
			return null;
		}

		static Visibility GetDynamicVisibility(DependencyObject parent)
		{
			IDynamicControl dynamicControl = GetDynamicControl(parent);
			if (dynamicControl != null)
				if (!dynamicControl.Blueprint.IsVisible())
					return Visibility.Collapsed;

			return Visibility.Visible;
		}

		/// <summary>
		/// Sets the child visibility of dynamic UI elements, based on calling GetVisible method/property specified in UIAttribute.
		/// </summary>
		public static void SetChildVisibility(DependencyObject parent)
		{
			if (parent == null)
				return;

			var numChildren = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < numChildren; i++)
			{
				FrameworkElement frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;

				if (frameworkElement != null)
					frameworkElement.Visibility = GetDynamicVisibility(frameworkElement);
			}
		}

		public static void StateChangeHandler(object sender, EventArgs e)
		{
			SetChildVisibility(Panel);
		}
	}
}
