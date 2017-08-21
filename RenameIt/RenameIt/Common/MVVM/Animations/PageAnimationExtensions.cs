using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Common.Animations
{
    /// <summary>
    /// Extension methods for pages to animate them
    /// </summary>
    public static class PageAnimationExtensions
    {
        /// <summary>
        /// Fades a page in
        /// </summary>
        /// <param name="page">The page this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task FadeIn(this Page page, float time)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add fade in animation
            sb.AddFadeIn(time);

            // start animating
            sb.Begin(page);

            // make page visible
            page.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }

        /// <summary>
        /// Fades a page out
        /// </summary>
        /// <param name="page">The page this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task FadeOut(this Page page, float time)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add fade in animation
            sb.AddFadeOut(time);

            // start animating
            sb.Begin(page);

            // make page visible
            page.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }

        /// <summary>
        /// Slides a page in from the right
        /// </summary>
        /// <param name="page">The page this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task SlideInFromRight(this Page page, float time)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add sslide from right animation
            sb.AddSlideFromRight(time, page.WindowWidth);

            // start animating
            sb.Begin(page);

            // make page visible
            page.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }

        /// <summary>
        /// Slides a page out to the left
        /// </summary>
        /// <param name="page">The page this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task SlideOutToLeft(this Page page, float time)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add sslide from right animation
            sb.AddSlideToLeft(time, page.WindowWidth);

            // start animating
            sb.Begin(page);

            // make page visible
            page.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }

        /// <summary>
        /// Slides a page in from right while fading the page in
        /// </summary>
        /// <param name="page">The page this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRight(this Page page, float time)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add sslide from right animation
            sb.AddSlideFromRight(time, page.WindowWidth);

            // add fade in animation
            sb.AddFadeIn(time);

            // start animating
            sb.Begin(page);

            // make page visible
            page.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }

        /// <summary>
        /// Slides a page out to the left while fading the page out
        /// </summary>
        /// <param name="page">The page this function belongs to</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeft(this Page page, float time)
        {
            // create the storyboard
            var sb = new Storyboard();

            // add sslide from right animation
            sb.AddSlideToLeft(time, page.WindowWidth);

            // add fade in animation
            sb.AddFadeOut(time);

            // start animating
            sb.Begin(page);

            // make page visible
            page.Visibility = Visibility.Visible;

            // wait for animation to complete
            await Task.Delay((int)(time * 1000));
        }
    }
}
