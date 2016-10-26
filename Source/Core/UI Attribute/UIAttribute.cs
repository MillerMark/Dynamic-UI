using System;
using System.Linq;

namespace DynamicUICore
{
	[AttributeUsage(AttributeTargets.Property)]
	public class UIAttribute : Attribute
	{
		/// <summary>
		/// Creates a new instance of the UIAttribute
		/// </summary>
		/// <param name="displayText">The text to display for the UI element associated with this property.</param>
		/// <param name="kind">The kind of UI element to create. If not specified, the engine will attempt to determine the kind based on the property's type.</param>
		/// <param name="enumOptions">Options for UIElementType.Enums. Can be checkboxes or radio buttons. Defaults to radio buttons if not specified.</param>
		/// <param name="enumLayout">Enum toggle button layout - horizontal or vertical. Defaults to vertical.</param>
		/// <param name="getVisible">The name of a parameterless boolean method or boolean property that will be called to determine whether this UI element is visible or not.</param>
		public UIAttribute(string displayText, 
											 UIElementType kind = UIElementType.Auto, 
											 UIEnumOptions enumOptions = UIEnumOptions.RadioButtons, 
											 UIEnumLayout enumLayout = UIEnumLayout.Vertical, 
											 string getVisible = null)
		{
			DisplayText = displayText;
			Kind = kind;
			EnumOptions = enumOptions;
			EnumLayout = enumLayout;
			GetVisible = getVisible;
		}

		/// <summary>
		/// The kind of UI element to create. If not specified, the engine will attempt to determine the kind based on the property's type.
		/// </summary>
		public UIElementType Kind { get; set; }

		/// <summary>
		/// The text to display for the UI element associated with this property.
		/// </summary>
		public string DisplayText { get; set; }

		/// <summary>
		/// Options for UIElementType.Enums. Can be checkboxes or radio buttons. Defaults to radio buttons if not specified.
		/// </summary>
		public UIEnumOptions EnumOptions { get; set; }

		/// <summary>
		/// The name of a parameterless boolean method or boolean property that will be called to determine whether this UI element is visible or not.
		/// </summary>
		public string GetVisible { get; set; }

		/// <summary>
		/// Enum toggle button layout - horizontal or vertical. Defaults to vertical.
		/// </summary>
		public UIEnumLayout EnumLayout { get; set; }
	}
}
