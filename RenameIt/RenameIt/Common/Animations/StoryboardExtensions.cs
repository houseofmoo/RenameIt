using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Common.Animations
{
    /// <summary>
    /// Extension methods for storyboards
    /// </summary>
    public static class StoryboardExtensions
    {
        /// <summary>
        /// Adds a fade in animation to the story board
        /// </summary>
        /// <param name="storyboard">The storyboard this function is being called on</param>
        /// <param name="time">The time the animation takes to complete</param>
        public static void AddFadeIn(this Storyboard storyboard, float time)
        {
            // create margin animate from right
            var fadeAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                From = 0,
                To = 1,
            };

            // set the target prop name
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("Opacity"));

            // add this to the storyboard
            storyboard.Children.Add(fadeAnimation);
        }

        /// <summary>
        /// Adds a fade out animation to the story board
        /// </summary>
        /// <param name="storyboard">The storyboard this function is being called on</param>
        /// <param name="time">The time the animation takes to complete</param>
        public static void AddFadeOut(this Storyboard storyboard, float time)
        {
            // create margin animate from right
            var fadeAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                From = 1,
                To = 0,
            };

            // set the target prop name
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("Opacity"));

            // add this to the storyboard
            storyboard.Children.Add(fadeAnimation);
        }

        /// <summary>
        /// Adds a slide from right animation to the story board
        /// </summary>
        /// <param name="storyboard">The storyboard this function is being called on</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <param name="offset">Offset of the page, used to animate from location and to location</param>
        /// <param name="decelerationRatio">Ratio used to slow page to a stop</param>
        public static void AddSlideFromRight(this Storyboard storyboard, float time, double offset, float decelerationRatio = 0.9f)
        {
            // create margin animate from right
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                From = new Thickness(offset, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio,
            };

            // set the target prop name
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));

            // add this to the storyboard
            storyboard.Children.Add(slideAnimation);
        }

        /// <summary>
        /// Adds a slide to left animation to the story board
        /// </summary>
        /// <param name="storyboard">The storyboard this function is being called on</param>
        /// <param name="time">The time the animation takes to complete</param>
        /// <param name="offset">Offset of the page, used to animate from location and to location</param>
        /// <param name="decelerationRatio">Ratio used to slow page to a stop</param>
        public static void AddSlideToLeft(this Storyboard storyboard, float time, double offset, float decelerationRatio = 0.9f)
        {
            // create margin animate from right
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(time)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0, offset, 0),
                DecelerationRatio = decelerationRatio,
            };

            // set the target prop name
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));

            // add this to the storyboard
            storyboard.Children.Add(slideAnimation);
        }
    }
}
