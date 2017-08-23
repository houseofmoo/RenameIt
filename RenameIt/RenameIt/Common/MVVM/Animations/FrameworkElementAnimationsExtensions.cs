using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Common.Animations
{
    /// <summary>
    /// Helpers to animate framework elements in specific ways
    /// </summary>
    public static class FrameworkElementAnimationsExtensions
    {
        /// <summary>
        /// Fades an element in
        /// </summary>
        /// <param name="element">The element this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task FadeIn(this FrameworkElement element, float time = 0.3f)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add fade in animation
            sb.AddFadeIn(time);

            // start animating
            sb.Begin(element);

            // make page visible
            element.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }

        /// <summary>
        /// Fades an element out
        /// </summary>
        /// <param name="element">The element this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task FadeOut(this FrameworkElement element, float time = 0.3f)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add fade in animation
            sb.AddFadeOut(time);

            // start animating
            sb.Begin(element);

            // make page visible
            element.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }

        /// <summary>
        /// Slides an element in from the right
        /// </summary>
        /// <param name="element">The element this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task SlideInFromRight(this FrameworkElement element, float time = 0.3f)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add sslide from right animation
            sb.AddSlideFromRight(time, element.ActualWidth);

            // start animating
            sb.Begin(element);

            // make page visible
            element.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }

        /// <summary>
        /// Slides an element out to the left
        /// </summary>
        /// <param name="element">The element this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task SlideOutToLeft(this FrameworkElement element, float time = 0.3f)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add sslide from right animation
            sb.AddSlideToLeft(time, element.ActualWidth);

            // start animating
            sb.Begin(element);

            // make page visible
            element.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }

        /// <summary>
        /// Slides an element in from right while fading the page in
        /// </summary>
        /// <param name="element">The element this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRight(this FrameworkElement element, float time = 0.3f)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add sslide from right animation
            sb.AddSlideFromRight(time, element.ActualWidth);

            // add fade in animation
            sb.AddFadeIn(time);

            // start animating
            sb.Begin(element);

            // make page visible
            element.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }

        /// <summary>
        /// Slides an element out to the left while fading the page out
        /// </summary>
        /// <param name="element">The element this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeft(this FrameworkElement element, float time = 0.3f)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add sslide from right animation
            sb.AddSlideToLeft(time, element.ActualWidth);

            // add fade in animation
            sb.AddFadeOut(time);

            // start animating
            sb.Begin(element);

            // make page visible
            element.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }
    }
}
