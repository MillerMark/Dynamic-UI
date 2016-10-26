using System;
using System.Reflection;

namespace DynamicUICore
{
	public static class DynamicExtensions
	{
		public static void SaveValue(this IDynamicControl control, object value)
		{
			if (control.Blueprint.IsEmpty)
				return;

			control.Blueprint.SetValue(value);
		}

		/// <summary>
		/// Sets the IDynamicControl's OriginalValue property, and also calls LoadValue
		/// so the object's data is transferred into the control's representation of that data.
		/// If you are deserializing instances of your IDynamicControls, then call this method 
		/// after those instances have been deserialized.
		/// </summary>
		public static void Initialize(this IDynamicControl control)
		{
			var value = control.Blueprint.GetValue();
			control.InitialValue = value;
			control.LoadValue(value);
		}

		public static void Initialize(this IDynamicControl control, Blueprint blueprint)
		{
			control.Blueprint = blueprint;
			control.Initialize();
		}
	}
}
