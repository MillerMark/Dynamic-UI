using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace DynamicWPF
{
	public static class FrameworkElementExtensions
	{

		public static FrameworkElement FindFirstFocusableChild(DependencyObject obj)
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				FrameworkElement child = VisualTreeHelper.GetChild(obj, i) as FrameworkElement;
				if (child != null && child.Focusable)
					return child;
				else
				{
					FrameworkElement childOfChild = FindFirstFocusableChild(child);
					if (childOfChild != null)
						return childOfChild;
				}
			}
			return null;
		}

		public static void FocusFirstElement(this FrameworkElement firstControlToFocus)
		{
			FrameworkElement firstFocusableChild = FindFirstFocusableChild(firstControlToFocus);

			if (firstFocusableChild == null && firstControlToFocus.Focusable)
				firstFocusableChild = firstControlToFocus;

			if (firstFocusableChild != null)
			{
				Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new System.Threading.ThreadStart(() =>
				{
					firstFocusableChild.Focus();         // Set Logical Focus
					Keyboard.Focus(firstFocusableChild); // Set Keyboard Focus
				}));
			}
		}

	}
}
