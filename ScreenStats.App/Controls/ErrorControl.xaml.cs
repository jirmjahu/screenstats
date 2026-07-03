using System.Windows.Controls;

namespace ScreenStats.App.Controls;

public partial class ErrorControl : UserControl
{
    public string Message { get; set; }
    
    public ErrorControl(string message)
    {
        Message = message;
        InitializeComponent();
        DataContext = this;
    }
}