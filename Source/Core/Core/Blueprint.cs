using System.Reflection;
using System;
using System.Windows;

namespace DynamicUICore
{
	/// <summary>
	/// Contains the elements needed to assemble dynamic UI controls and bind them to properties of 
	/// live instantiated objects.
	/// </summary>
	public class Blueprint
	{
		public UIAttribute UIAttribute { get; set; }
		public object Instance { get; set; }
		public PropertyInfo Property { get; set; }


		// constructors...
		public Blueprint(UIAttribute uIAttribute, object instance, PropertyInfo propertyInfo)
		{
			UIAttribute = uIAttribute;
			Instance = instance;
			Property = propertyInfo;
		}

		/// <summary>
		/// Returns true if this Blueprint object has not been properly initialized.
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				return Property == null || Instance == null;
			}
		}

		/// <summary>
		/// Gets the current connected instance property's value.
		/// </summary>
		public object GetValue()
		{
			return Property.GetValue(Instance);
		}

		/// <summary>
		/// Gets the current connected instance property's value as an Int.
		/// </summary>
		public int GetIntValue()
		{
			return (int)Property.GetValue(Instance);
		}

		/// <summary>
		/// Sets the current connected instance property to the specified value.
		/// </summary>
		public void SetValue(object value)
		{
			Property.SetValue(Instance, value);
		}

		/// <summary>
		/// Determines whether this instance property's dynamic control should be visible or not, based on 
		/// the UIAttribute's GetVisible property (which references a boolean method or a boolean property 
		/// that is queried from this method - if that boolean member returns true this dynamic control will
		/// be visible; otherwise it will be collapsed).
		/// </summary>
		public bool IsVisible()
		{
			if (string.IsNullOrEmpty(UIAttribute.GetVisible))
				return true;

			try
			{
				Type thisType = Instance.GetType();

				const BindingFlags AnyMember = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;

				MethodInfo checkVisibilityMethod = thisType.GetMethod(UIAttribute.GetVisible, AnyMember);

				object result = null;

				if (checkVisibilityMethod != null)
					result = checkVisibilityMethod.Invoke(Instance, null);
				else
				{
					PropertyInfo checkVisibilityProperty = thisType.GetProperty(UIAttribute.GetVisible, AnyMember);
					if (checkVisibilityProperty != null)
						result = checkVisibilityProperty.GetValue(Instance);
				}

				if (result == null)
					return true;

				return (bool)result;
			}
			catch
			{
				return true;
			}
		}

		/// <summary>
		/// Returns an array of FieldInfo objects representing the enum elements of the 
		/// instance property's type (which needs to be an enum type).
		/// </summary>
		public FieldInfo[] GetEnumElements()
		{
			return Property.PropertyType.GetFields(BindingFlags.Public | BindingFlags.Static);
		}


	}
}
