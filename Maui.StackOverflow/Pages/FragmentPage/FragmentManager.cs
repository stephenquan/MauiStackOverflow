using CommunityToolkit.Mvvm.Input;

namespace Maui.StackOverflow;

public partial class FragmentManager : Grid
{
    public FragmentManager()
    {
        this.Loaded += (s, e) =>
        {
            if (InitialFragment is not null)
            {
                this.Dispatcher.DispatchAsync(async () => await Push(InitialFragment));
            }
        };
    }

    public int FragmentCount => this.Children.Count;


    [RelayCommand]
    public async Task Push(ControlTemplate fragment)
    {
        ContentView contentView = new ContentView()
        {
            ControlTemplate = fragment
        };
        Grid grid = new Grid()
        {
            Background = Colors.White,
            TranslationX = this.Width
        };
        grid.Add(contentView);
        Add(grid);
        OnPropertyChanged(nameof(FragmentCount));
        await grid.TranslateTo(0, 0, 250);
        PopCommand?.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanPop))]
    public async Task Pop()
    {
        if (this.Children.Count == 0)
        {
            return;
        }
        if (this.Children[Children.Count - 1] is not Grid grid)
        {
            return;
        }
        await grid.TranslateTo(this.Width, 0, 250);
        this.Children.Remove(grid);
        OnPropertyChanged(nameof(FragmentCount));
        PopCommand?.NotifyCanExecuteChanged();
    }

    public bool CanPop => this.Children.Count > 0;

    public ControlTemplate? InitialFragment { get; set; } = null;
}
