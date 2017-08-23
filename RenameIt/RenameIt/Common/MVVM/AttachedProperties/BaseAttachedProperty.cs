using System;
using System.Windows;

namespace Common.AttachedProperties
{
    /// <summary>
    /// A based attached property to replace the vanilla WPF attached property
    /// </summary>
    /// <typeparam name="Parent">The parent class to be the attached property</typeparam>
    /// <typeparam name="Property">The type of this attached property</typeparam>
    public abstract class BaseAttachedProperty<Parent, Property>
        where Parent : BaseAttachedProperty<Parent, Property>, new()
    {
        #region public events
        /// <summary>
        /// Fired when the value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        /// <summary>
        /// Fired when the value changes, even if the value is the same
        /// </summary>
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };
        #endregion

        #region public properties
        /// <summary>
        /// A singleton instance of our parent class
        /// </summary>
        public static Parent Instance { get; private set; } = new Parent();
        #endregion

        #region attached property definitions
        /// <summary>
        /// The attached property for this class
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached(
                "Value",
                typeof(Property),
                typeof(BaseAttachedProperty<Parent, Property>),
                new PropertyMetadata(
                    default(Property),
                    new PropertyChangedCallback(OnValuePropertyChanged),
                    new CoerceValueCallback(OnValuePropertyUpdated)
                    ));

        /// <summary>
        /// The call back event when the <see cref="ValueProperty"/> has changed
        /// </summary>
        /// <param name="d">The UI element that had its property changed</param>
        /// <param name="e">Arguments for the event</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // call parent function
            Instance.OnValueChanged(d, e);

            // Call event listeners
            Instance.ValueChanged(d, e);
        }

        /// <summary>
        /// The call back event when the <see cref="ValueProperty"/> has changed even if it is the same value
        /// </summary>
        /// <param name="d">The UI element that had its property changed</param>
        /// <param name="e">Arguments for the event</param>
        private static object OnValuePropertyUpdated(DependencyObject d, object value)
        {
            // call parent function
            Instance.OnValueUpdated(d, value);

            // Call event listeners
            Instance.ValueUpdated(d, value);

            return value;
        }

        /// <summary>
        /// Gets the attached property
        /// </summary>
        /// <param name="d">The element to get the property from</param>
        /// <returns></returns>
        public static Property GetValue(DependencyObject d)
        {
            return (Property)d.GetValue(ValueProperty);
        }

        /// <summary>
        /// Set the attached property
        /// </summary>
        /// <param name="d">The element to get the property from</param>
        /// <param name="value">The value to set the property to</param>
        public static void SetValue(DependencyObject d, Property value)
        {
            d.SetValue(ValueProperty, value);
        }
        #endregion

        #region event methods
        /// <summary>
        /// Method that is called when any attached property of this type is changed
        /// </summary>
        /// <param name="sender">The UI element that this property was changed for</param>
        /// <param name="e">The arguments for this event</param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        /// <summary>
        /// Method that is called when any attached property of this type is changed, even if the value is the same
        /// </summary>
        /// <param name="sender">The UI element that this property was changed for</param>
        /// <param name="e">The arguments for this event</param>
        public virtual void OnValueUpdated(DependencyObject sender, object value) { }
        #endregion
    }
}
