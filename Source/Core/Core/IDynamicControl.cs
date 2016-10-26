using System;
using System.Reflection;

namespace DynamicUICore
{
    /// <summary>
    /// The IDynamicControl interface. Implement this interface in any controls that need to be dynamically linked
    /// up to live instance properties at runtime.
    /// </summary>
	public interface IDynamicControl
	{
        /// <summary>
        /// Call this method to revert the dynamic control's value back to its initial value (e.g., useful in a cancel operation).
        /// </summary>
		void Revert();

        /// <summary>
        /// Loads the specified value into the dynamic control.
        /// </summary>
        /// <param name="value">The value to load.</param>
		void LoadValue(object value);

        /// <summary>
        /// The initial value of this control. Referenced by the Revert method.
        /// </summary>
		object InitialValue { get; set; }

        /// <summary>
        /// The Blueprint that binds this control to a live instance's property at runtime.
        /// </summary>
		Blueprint Blueprint { get; set; }

        /// <summary>
        /// Trigger this event when your dynamic control's state changes.
        /// </summary>
		event EventHandler StateChanged;
	}
}

