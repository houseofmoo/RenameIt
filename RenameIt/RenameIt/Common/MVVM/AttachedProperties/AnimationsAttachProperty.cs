using System.Windows;
using Common.Animations; // developer defined

namespace Common.AttachedProperties
{
    public class AnimateFadeInFadeOutProperty : BaseAnimateProperty<AnimateFadeInFadeOutProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Animate in
                await element.FadeIn();
            else
                // animate out
                await element.FadeOut();
        }
    }
}
