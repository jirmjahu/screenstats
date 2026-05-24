using System.Windows.Threading;

namespace ScreenStats.App.Widgets;

public abstract class UpdateableWidget : Widget, IDisposable
{
    private DispatcherTimer? _timer;

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

    protected abstract Task Update();

    protected abstract TimeSpan UpdateInterval();
}