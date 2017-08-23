using System.Windows;

namespace Common.AttachedProperties
{
    /// <summary>
    /// Base class to run any animation method when a boolean is set to true
    /// and a reverse animation when set to false
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class BaseAnimateProperty<Parent> : BaseAttachedProperty<Parent, bool>
        where Parent : BaseAttachedProperty<Parent, bool>, new()
    {
        #region public properties
        /// <summary>
        /// Flag indicating if this is the fisrt time this property has been loaded
        /// </summary>
        public bool FirstLoad { get; set; } = true;
        #endregion

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // get the framework element
            if (!(sender is FrameworkElement element))
                return;

            // don't fire if the value does not change
            if (sender.GetValue(ValueProperty) == value && !this.FirstLoad)
                return;

            // on first load...
            if (this.FirstLoad)
            {
                // create single self-unhookable event
                // for the elemtns loaded event
                RoutedEventHandler onLoaded = null;
                onLoaded = (ss, ee) =>
                {
                    // unhook ourselves
                    element.Loaded -= onLoaded;

                    // do desired animation
                    DoAnimation(element, (bool)value);

                    // no longer in first load
                    this.FirstLoad = false;
                };

                // hook into loaded event of the element
                element.Loaded += onLoaded;
            }
            else
            {
                // do desired animation
                DoAnimation(element, (bool)value);
            }
        }

        /// <summary>
        /// The animation method that is fired when the value changes
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="value">The new value</param>
        protected virtual void DoAnimation(FrameworkElement element, bool value) { }
    }
}
