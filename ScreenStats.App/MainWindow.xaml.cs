using System.Windows;
using ScreenStats.App.Helpers;

namespace ScreenStats.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
        WindowHelper.MakeDesktopWidget(this);
    }
}