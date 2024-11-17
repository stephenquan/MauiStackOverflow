using CommunityToolkit.Maui.Views;

namespace Maui.StackOverflow;

public partial class ExpanderPage : ContentPage
{
    public ExpanderPage()
    {
        InitializeComponent();

        expander.PropertyChanged += (s, e) =>
        {
            switch (e.PropertyName)
            {
                case nameof(Expander.IsExpanded):
                    if (expander.IsExpanded)
                    {
                        caret.RotateTo(180);
                        var animation = new Animation(v => contentOuter.HeightRequest = v, 0, contentInner.Height);
                        animation.Commit(this, "ExpandContent");
                    }
                    else
                    {
                        caret.RotateTo(0);
                        var animation = new Animation(v => contentOuter.HeightRequest = v, contentInner.Height, 0);
                        animation.Commit(this, "CollapseContent");
                    }
                    break;
            }
        };
    }
}
