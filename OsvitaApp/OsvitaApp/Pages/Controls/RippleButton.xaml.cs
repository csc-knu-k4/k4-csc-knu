using CommunityToolkit.Maui.Behaviors;
using Syncfusion.Maui.Toolkit.Buttons;

namespace OsvitaApp.Pages.Controls;

public partial class RippleButton : SfButton
{
    public RippleButton()
    {
        //InitializeComponent();
        this.Behaviors.Add(new TouchBehavior()
        {
            DefaultAnimationDuration = 250,
            DefaultAnimationEasing = Easing.CubicOut,
            PressedOpacity = 0.6,
            PressedScale = 0.99
        });

    }
}