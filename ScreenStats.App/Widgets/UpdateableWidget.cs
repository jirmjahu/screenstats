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

        if (_timer != null)
        {
            return;
        }

        _timer = new DispatcherTimer
        {
            Interval = UpdateInterval()
        };
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }

    public virtual void Dispose()
    {
        if (_timer == null)
        {
            return;
        }

        _timer.Tick -= OnTimerTick;
        _timer.Stop();
        _timer = null;
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void OnTimerTick(object? sender, EventArgs e)
    {
        await Update();
    }

    protected abstract Task Update();

    protected abstract TimeSpan UpdateInterval();
}