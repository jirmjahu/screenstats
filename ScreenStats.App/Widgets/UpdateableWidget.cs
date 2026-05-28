using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace ScreenStats.App.Widgets;

public abstract class UpdateableWidget : Widget, INotifyPropertyChanged, IDisposable
{
    private DispatcherTimer? _timer;

    public event PropertyChangedEventHandler? PropertyChanged;

    public void Start()
    {
        _ = Update();

        _timer = new DispatcherTimer
        {
            Interval = UpdateInterval()
        };
        _timer.Tick += async (_, _) => await Update();
        _timer.Start();
    }

    public virtual void Dispose()
    {
        _timer?.Stop();
        _timer = null;
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected abstract Task Update();

    protected abstract TimeSpan UpdateInterval();
}